namespace NationalLandmarks.Server.Features.Visit.Models
{
    public class VisitedLandmarkServiceModel
    {
        public string Name { get; set; }

        public bool IsNationalLandmark { get; set; }

        public string PlaceName { get; set; }

        public string ImagePath { get; set; }
    }
}
