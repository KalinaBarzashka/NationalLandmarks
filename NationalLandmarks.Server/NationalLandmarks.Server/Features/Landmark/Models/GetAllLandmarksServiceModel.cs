namespace NationalLandmarks.Server.Features.Landmark.Models
{
    public class GetAllLandmarksServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsNationalLandmark { get; set; }

        public string Town { get; set; }

        public string ImageUrl { get; set; }
    }
}
