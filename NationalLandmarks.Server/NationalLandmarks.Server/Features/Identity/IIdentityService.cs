using NationalLandmarks.Server.Features.Identity.Models;
using NationalLandmarks.Server.Infrastructure.Services;

namespace NationalLandmarks.Server.Features.Identity
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string username, string secret);

        Task<ProfileServiceModel> GetUserProfile(string userId);

        Task<Result> UpdateProfile(string userId, UpdateProfileRequestModel model);
    }
}
