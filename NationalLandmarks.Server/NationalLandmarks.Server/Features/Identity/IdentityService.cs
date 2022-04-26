namespace NationalLandmarks.Server.Features.Identity
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using NationalLandmarks.Server.Data;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Features.Identity.Models;
    using NationalLandmarks.Server.Infrastructure.Services;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class IdentityService : IIdentityService
    {
        private readonly NationalLandmarksDbContext dbContext;

        public IdentityService(NationalLandmarksDbContext dbContext)
        {
            this.dbContext = dbContext;
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

                user.Email = modelEmail;
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

                user.UserName = modelUserName;
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
    }
}
