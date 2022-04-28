namespace NationalLandmarks.Server.Features.Landmark.Models
{
    public class LandmarkDetailsServiceModel: GetAllLandmarksServiceModel
    {
        public string Description { get; set; }

        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string? Opens { get; set; }

        public string? Closes { get; set; }

        public bool? WorksOnWeekends { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Website { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public int VisitsCount { get; set; }

        public int TotalVisits { get; set; }

        public IEnumerable<int?> Grades { get; set; }
    }
}
