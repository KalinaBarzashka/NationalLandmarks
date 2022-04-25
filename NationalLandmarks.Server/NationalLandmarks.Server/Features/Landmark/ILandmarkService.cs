namespace NationalLandmarks.Server.Features.Landmark
{
    using NationalLandmarks.Server.Features.Landmark.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    public interface ILandmarkService
    {
        public Task<int> CreateLandmark(CreateLandmarkRequestModel model, string userId);

        public Task<Result> UpdateLandmark(int id, UpdateLandmarkRequestModel model, string userId);

        public Task<Result> DeleteLandmark(int id, string userId);

        public Task<IEnumerable<GetAllLandmarksServiceModel>> GetAllLandmarks();

        public Task<LandmarkDetailsServiceModel> GetLandmarkDetailsById(int id);

    }
}
