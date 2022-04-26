namespace NationalLandmarks.Server.Features.Visit.Models
{
    public class VisitedLandmarkServiceModel
    {
        public string Name { get; set; }

        public bool IsNationalLandmark { get; set; }

        public string TownName { get; set; }

        public string ImageUrl { get; set; }
    }
}
