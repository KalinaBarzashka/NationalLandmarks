namespace NationalLandmarks.Server.Features.Landmark
{
    using NationalLandmarks.Server.Features.Landmark.Models;

    public interface ILandmarkService
    {
        public Task<int> CreateLandmark(CreateLandmarkRequestModel model, string userId);

        public Task<bool> UpdateLandmark(UpdateLandmarkRequestModel model, string userId);

        public Task<bool> DeleteLandmark(int id, string userId);

        public Task<IEnumerable<GetAllLandmarksServiceModel>> GetAllLandmarks();

        public Task<LandmarkDetailsServiceModel> GetLandmarkDetailsById(int id);

    }
}
