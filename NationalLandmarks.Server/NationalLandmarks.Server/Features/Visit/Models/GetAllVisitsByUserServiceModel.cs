namespace NationalLandmarks.Server.Features.Visit.Models
{
    using NationalLandmarks.Server.Data.Models;

    public class GetAllVisitsByUserServiceModel
    {
        public int Id { get; set; }

        public int LandmarkId { get; set; }

        public VisitedLandmarkServiceModel Landmark { get; set; }

        public DateTime VisitedOn { get; set; }

        public Grade? Grade { get; set; }

        public string? Comment { get; set; }
    }
}
