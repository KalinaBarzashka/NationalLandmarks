namespace NationalLandmarks.Server.Infrastructure.Services
{
    using NationalLandmarks.Server.Infrastructure.Extensions;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        //cache the user - minier performance fix!
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            this.user = contextAccessor.HttpContext?.User;
        }

        public string? GetUserName()
        {
            return this.user?.Identity?.Name;
        }

        public string? GetId()
        {
            return this.user?.GetId();
        }
    }
}
