namespace NationalLandmarks.Server.Features.Landmark
{
    using NationalLandmarks.Server.Features.Landmark.Models;
    using NationalLandmarks.Server.Infrastructure.Services;

    public interface ILandmarkService
    {
        public Task<int> Create(CreateLandmarkRequestModel model, string userId);

        public Task<Result> Update(int id, UpdateLandmarkRequestModel model, string userId);

        public Task<Result> Delete(int id, string userId);

        public Task<IEnumerable<GetAllLandmarksServiceModel>> GetAll();

        public Task<LandmarkDetailsServiceModel> GetDetailsById(int id);

    }
}
