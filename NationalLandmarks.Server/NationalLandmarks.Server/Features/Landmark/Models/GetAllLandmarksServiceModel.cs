namespace NationalLandmarks.Server.Features.Landmark.Models
{
    public class GetAllLandmarksServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsNationalLandmark { get; set; }

        public int TownId { get; set; }

        public string TownName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
