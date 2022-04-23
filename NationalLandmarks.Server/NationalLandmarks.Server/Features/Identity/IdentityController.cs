namespace NationalLandmarks.Server.Features.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using NationalLandmarks.Server.Data.Models;
    using NationalLandmarks.Server.Features.Identity.Models;

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityService identityService;
        private readonly AppSettings appSettings;

        public IdentityController(UserManager<User> userManager,
            IIdentityService identityService,
            IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }

        [Route(nameof(Register))]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [Route(nameof(Login))]
        [HttpPost]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null) return Unauthorized();

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid) return Unauthorized();

            var token = this.identityService.GenerateJwtToken(
                user.Id,
                user.UserName,
                this.appSettings.Secret);

            return new LoginResponseModel
            {
                Token = token
            };
        }

    }
}
