namespace NationalLandmarks.Server.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        string? GetUserName();

        string? GetId();
    }
}
