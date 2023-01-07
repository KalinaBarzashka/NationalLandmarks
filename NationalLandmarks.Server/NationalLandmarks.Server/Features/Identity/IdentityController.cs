using Microsoft.AspNetCore.Http;

namespace NationalLandmarks.Server.Features.Identity
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Features.Identity.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

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

        public IdentityController(UserManager<User> userManager,
            IIdentityService identityService,
            IOptions<AppSettings> appSettings,
            ICurrentUserService currentUserService)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
            this.currentUserService = currentUserService;
        }

        /// <summary>
        /// Register user in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Action result.</returns>
        /// <response code="200">Returns ok if user is created successfully.</response>
        /// <response code="400">Returns bad request if creating user fails.</response>
        [HttpPost]
        [Route(nameof(Register))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<IdentityError>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
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
            if (user == null) return StatusCode(StatusCodes.Status403Forbidden); // Status401Unauthorized

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid) return StatusCode(StatusCodes.Status403Forbidden); // Status401Unauthorized

            var token = this.identityService.GenerateJwtToken(
                user.Id,
                user.UserName,
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
    }
}
