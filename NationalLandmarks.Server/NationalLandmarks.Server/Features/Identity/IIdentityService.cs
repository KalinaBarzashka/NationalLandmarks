namespace NationalLandmarks.Server.Features.Identity
{
    using NationalLandmarks.Server.Features.Identity.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string username, IList<string> roles, string secret);

        int? ValidateJwtToken(string token, string secret);

        string generateVerificationToken();

        bool VerifyEmail(string token);

        Task<ProfileServiceModel> GetUserProfile(string userId);

        Task<Result> UpdateProfile(string userId, UpdateProfileRequestModel model);

        bool IsEmailAlreadyUsed(string email);

        string HashPassword(string password, string salt);

        Task<IEnumerable<GetAllRolesServiceModel>> GetAllRoles();

        Task<string> CreateRole(CreateRoleRequestModel model);

        Task<bool> DoesRoleExists(string id);

        Task<Result> UpdateRole(string id, UpdateRoleRequestModel model);

        Task<Result> DeleteRole(string id);
    }
}
