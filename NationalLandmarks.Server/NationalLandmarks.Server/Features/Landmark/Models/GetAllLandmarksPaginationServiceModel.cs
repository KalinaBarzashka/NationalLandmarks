namespace NationalLandmarks.Server.Features.Landmark.Models
{
    public class GetAllLandmarksPaginationServiceModel
    {
        public IEnumerable<GetAllLandmarksServiceModel> Landmarks { get; set; }

        public int PageNumber { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalItemsCount { get; set; }
    }
}
