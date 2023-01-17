using Microsoft.AspNetCore.Http;

namespace NationalLandmarks.Server.Features.Identity
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Features.Identity.Models;
    using NationalLandmarks.Server.Features.Notification;
    using NationalLandmarks.Server.Infrastructure.Services;

    using static Infrastructure.WebConstants;

    /// <summary>
    /// CRUD operations for Identity objects
    /// </summary>
    [Produces("application/json")]
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        private readonly AppSettings appSettings;
        private readonly ICurrentUserService currentUserService;
        private readonly IEmailService emailService;
        private readonly RoleManager<Role> roleManager;

        public IdentityController(UserManager<User> userManager,
            IIdentityService identityService,
            IOptions<AppSettings> appSettings,
            ICurrentUserService currentUserService,
            IEmailService emailService,
            RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
            this.currentUserService = currentUserService;
            this.emailService = emailService;
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Register user in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if user is created successfully.</response>
        /// <response code="400">Returns bad request if creating user fails.</response>
        /// <response code="409">Returns conflict is email is already in use.</response>
        [HttpPost]
        [Route(nameof(Register))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            if (identityService.IsEmailAlreadyUsed(model.Email))
            {
                return Conflict("Email is already in use!");
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                VerificationToken = this.identityService.generateVerificationToken()
            };

            var hashedPassword = this.identityService.HashPassword(model.Password, appSettings.Salt);

            var result = await this.userManager.CreateAsync(user, hashedPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // send email
            this.emailService.sendVerificationEmail(user, Request.Headers["origin"]);
            // add to role User
            await this.userManager.AddToRoleAsync(user, "User");

            return Ok(new { message = "Registration successful, please check your email for verification instructions.!" });
        }

        /// <summary>
        /// Verify users' e-mail address
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if e-mail is verified successfully.</response>
        /// <response code="400">Returns bad request if e-mail verification fails.</response>
        [HttpPost("verify-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult VerifyEmail([FromQuery]string token)
        {
            bool verified = this.identityService.VerifyEmail(token);

            if (!verified)
            {
                return BadRequest("Verification failed!");
            }

            return Ok(new { message = "Verification successful, you can now login!" });
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Token if user was successfully identified.</returns>
        /// <response code="200">Returns ok and token if user is identified successfully.</response>
        /// <response code="403">Returns forbidden if wrong username or password is provided.</response>
        [HttpPost]
        [Route(nameof(Login))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null || !user.IsVerified) return StatusCode(StatusCodes.Status403Forbidden); // Status401Unauthorized

            var hashedPassword = this.identityService.HashPassword(model.Password, appSettings.Salt);

            var passwordValid = await this.userManager.CheckPasswordAsync(user, hashedPassword);
            if (!passwordValid) return StatusCode(StatusCodes.Status403Forbidden); // Status401Unauthorized

            var userRoles = await this.userManager.GetRolesAsync(user);

            var token = this.identityService.GenerateJwtToken(
                user.Id,
                user.UserName,
                userRoles,
                this.appSettings.Secret);

            return new LoginResponseModel
            {
                Token = token
            };
        }

        /// <summary>
        /// Get user's profile information.
        /// </summary>
        /// <returns>Profile information of the current user.</returns>
        /// <response code="200">Returns ok and profile model object with user details.</response>
        [HttpGet]
        [Authorize]
        [Route(nameof(Profile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileServiceModel>> Profile()
        {
            return await this.identityService.GetUserProfile(this.currentUserService.GetId());
        }

        /// <summary>
        /// Update user's profile information.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the update was successfull.</response>
        /// <response code="400">Returns bad request if update fails.</response>
        [HttpPut]
        [Authorize]
        [Route(nameof(Profile))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProfile(UpdateProfileRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.identityService.UpdateProfile(userId, model);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Get all roles from the database.
        /// </summary>
        /// <returns>IEnumerable object models with id, name, description and is deleted params.</returns>
        /// <response code="200">Returns all roles as objects.</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route(nameof(Roles))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<GetAllRolesServiceModel>> Roles()
        {
            return await this.identityService.GetAllRoles();
        }

        /// <summary>
        /// Create new Role object.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action result and the id of the newly created Role.</returns>
        /// <response code="201">Returns the newly created item.</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route(nameof(Roles))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(CreateRoleRequestModel model)
        {
            string roleId = await this.identityService.CreateRole(model);

            return Created(nameof(this.Create), roleId);
        }

        /// <summary>
        /// Update specific Role object.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the update was successfull.</response>
        /// <response code="400">Returns bad request if update fails.</response>
        /// <response code="404">Returns not found if role with the specified id does not exists.</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route(nameof(Roles) + "/" + RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(string id, UpdateRoleRequestModel model)
        {
            var exists = await this.identityService.DoesRoleExists(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = await this.identityService.UpdateRole(id, model);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Delete specific 
        /// object.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if the delete was successfull.</response>
        /// <response code="400">Returns bad request if delete fails.</response>
        /// <response code="404">Returns not found if role with the specified id does not exists.</response>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route(nameof(Roles) + "/" + RouteId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string id)
        {
            var exists = await this.identityService.DoesRoleExists(id);

            if (!exists)
            {
                return NotFound();
            }

            var result = await this.identityService.DeleteRole(id);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        /// <summary>
        /// Assign user to specific role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <response code="200">Returns ok if the assign was successfull.</response>
        /// <response code="400">Returns bad request if assign fails.</response>
        [HttpPatch]
        [Authorize(Roles = "Admin")]
        [Route(nameof(Roles))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AssignUserToRole([FromQuery]string userId, [FromQuery]string roleId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var role = await this.roleManager.FindByIdAsync(roleId);

            var result = await this.userManager.AddToRoleAsync(user, role.Name);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        /// <summary>
        /// Unassign user from specific role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        /// <response code="200">Returns ok if the assign was successfull.</response>
        /// <response code="400">Returns bad request if assign fails.</response>
        [HttpPatch]
        [Authorize(Roles = "Admin")]
        [Route(nameof(Roles) + "/Remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UnassignUserFromRole([FromQuery] string userId, [FromQuery] string roleId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var role = await this.roleManager.FindByIdAsync(roleId);

            var result = await this.userManager.RemoveFromRoleAsync(user, role.Name);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }
    }
}
