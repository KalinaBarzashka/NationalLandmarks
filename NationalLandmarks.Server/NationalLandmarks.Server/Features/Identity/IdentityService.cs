namespace NationalLandmarks.Server.Features.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Features.Identity.Models;
    using NationalLandmarks.Server.Infrastructure.Services;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentityService : IIdentityService
    {
        private readonly NationalLandmarksDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public IdentityService(NationalLandmarksDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public string GenerateJwtToken(string userId, string userName, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        public int? ValidateJwtToken(string token, string secret)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);

                // return user id from JWT token if validation successful
                return userId;
            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
        public string generateVerificationToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // ensure token is unique by checking against db
            var tokenIsUnique = !this.dbContext.Users.Any(x => x.VerificationToken == token);
            if (!tokenIsUnique)
                return generateVerificationToken();

            return token;
        }

        public bool VerifyEmail(string token)
        {
            var user = this.dbContext.Users.SingleOrDefault(x => x.VerificationToken == token);

            if (user == null)
                return false;

            user.Verified = DateTime.UtcNow;
            user.VerificationToken = null;

            this.dbContext.Users.Update(user);
            this.dbContext.SaveChanges();
            return true;
        }

        public async Task<ProfileServiceModel> GetUserProfile(string userId)
        {
            return await this.dbContext
                .Users
                .Where(u => u.Id == userId)
                .Select(u => new ProfileServiceModel
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    ProfileImageUrl = u.ProfileImageUrl
                }).FirstOrDefaultAsync();
        }

        public async Task<Result> UpdateProfile(string userId, UpdateProfileRequestModel model)
        {
            var user = await this.dbContext.Users.FindAsync(userId);

            if (user == null)
            {
                return "User does not exists!";
            }

            var result = await this.ChangeUserEmail(user, model.Email, userId);
            if(result.Failure)
            {
                return result;
            }

            result = await this.ChangeUserName(user, model.UserName, userId);
            if (result.Failure)
            {
                return result;
            }

            this.ChangeUserProfile(user, model.FirstName, model.LastName, model.ProfileImageUrl);

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<Result> ChangeUserEmail(User user, string modelEmail, string userId)
        {
            if (!string.IsNullOrWhiteSpace(modelEmail) && user.Email != modelEmail)
            {
                var emailExists = await this.dbContext
                    .Users
                    .AnyAsync(u => u.Id != userId && u.Email == modelEmail);

                if (emailExists)
                {
                    return "The provided e-mail is already taken!";
                }

                var userDb = await this.userManager.FindByIdAsync(userId);
                if (userDb == null)
                {
                    return "User not found!";
                }
                await this.userManager.SetEmailAsync(userDb, modelEmail);
            }

            return true;
        }

        private async Task<Result> ChangeUserName(User user, string modelUserName, string userId)
        {
            if (!string.IsNullOrWhiteSpace(modelUserName) && user.UserName != modelUserName)
            {
                var userNameExists = await this.dbContext
                    .Users
                    .AnyAsync(u => u.Id != userId && u.UserName == modelUserName);

                if (userNameExists)
                {
                    return "The provided username is already taken!";
                }

                var userDb = await this.userManager.FindByIdAsync(userId);
                if(userDb == null)
                {
                    return "User not found!";
                }
                await this.userManager.SetUserNameAsync(userDb, modelUserName);                
            }

            return true;
        }

        private void ChangeUserProfile(User user, string modelFirstName, string modelLastName, string modelProfileImageUrl)
        {
            if (user.FirstName != modelFirstName)
            {
                user.FirstName = modelFirstName;
            }

            if (user.LastName != modelLastName)
            {
                user.LastName = modelLastName;
            }

            if (user.ProfileImageUrl != modelProfileImageUrl)
            {
                user.ProfileImageUrl = modelProfileImageUrl;
            }
        }

        public bool IsEmailAlreadyUsed(string email)
        {
            // validate - already registered email
            return dbContext.Users.Any(x => x.Email == email);
        }

        public string HashPassword(string password, string salt)
        {
            var passwordAsByteArr = Encoding.ASCII.GetBytes(password);
            var saltAsByteArr = Encoding.ASCII.GetBytes(salt);
            return new Rfc2898DeriveBytes(passwordAsByteArr, saltAsByteArr, 10, HashAlgorithmName.SHA256).ToString();
        }

        public async Task<IEnumerable<GetAllRolesServiceModel>> GetAllRoles()
        {
            return await this.dbContext
                .Roles
                .Select(r => new GetAllRolesServiceModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    IsActive = r.IsActive
                })
                .ToListAsync();
        }

        public async Task<string> CreateRole(CreateRoleRequestModel model)
        {
            var role = new Role{
                Name = model.Name,
                Description = model.Description
            };

            await this.roleManager.CreateAsync(role);
            return role.Id;
        }

        public async Task<bool> DoesRoleExists(string id)
        {
            var role = await this.dbContext
                .Roles
                .Where(a => a.Id == id)
                .CountAsync();

            return role != 0;
        }

        public async Task<Result> UpdateRole(string id, UpdateRoleRequestModel model)
        {
            var role = await this.dbContext
                .Roles
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (role == null)
            {
                return "Role does not exists!";
            }

            if (role.Name != model.Name && !string.IsNullOrWhiteSpace(model.Name))
            {
                role.Name = model.Name;
            }

            if (role.Description != model.Description && !string.IsNullOrWhiteSpace(model.Description))
            {
                role.Description = model.Description;
            }

            if (model.IsActive.HasValue)
            {
                role.IsActive = model.IsActive;
            }

            await this.roleManager.UpdateAsync(role);
            return true;
        }

        public async Task<Result> DeleteRole(string id)
        {
            var role = await this.dbContext
                .Roles
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (role == null)
            {
                return "Role does not exists!";
            }

            role.IsDeleted = true;
            role.IsActive = false;
            await this.roleManager.UpdateAsync(role);
            return true;
        }
    }
}
