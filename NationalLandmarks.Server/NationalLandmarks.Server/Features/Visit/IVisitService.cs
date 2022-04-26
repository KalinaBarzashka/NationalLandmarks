namespace NationalLandmarks.Server.Features.Visit
{
    using NationalLandmarks.Server.Features.Visit.Models;

    public interface IVisitService
    {
        Task<int> AddVisitedLandmarkForUser(int landmarkId, int? grade, string? userId);

        Task<IEnumerable<GetAllVisitsByUserServiceModel>> GetAllByUserId(string? userId);

    }
}
