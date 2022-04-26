namespace NationalLandmarks.Server.Features.Visit.Models
{
    public class VisitedLandmarkServiceModel
    {
        public string Name { get; set; }

        public bool IsNationalLandmark { get; set; }

        public int TownId { get; set; }

        public string ImageUrl { get; set; }
    }
}
