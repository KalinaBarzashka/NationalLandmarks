namespace NationalLandmarks.Server.Features.Landmark
{
    public interface ILandmarkService
    {
        public Task<int> CreateLandmark(CreateLandmarkRequestModel model, string userId);
    }
}
