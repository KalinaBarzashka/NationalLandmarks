namespace NationalLandmarks.Server.Features.Landmark.Models
{
    public class GetAllLandmarksServiceModel
    {
        public int Id { get; set; }

        public string? RegistrationNumber { get; set; }

        public string Name { get; set; }

        public bool IsNationalLandmark { get; set; }

        public int PlaceId { get; set; }

        public string PlaceName { get; set; }

        public string ImagePath { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
