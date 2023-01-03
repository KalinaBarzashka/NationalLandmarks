namespace NationalLandmarks.Server.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Visit
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int LandmarkId { get; set; }

        public Landmark Landmark { get; set; }

        public DateTime VisitedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public Grade? Grade { get; set; }

        public string? Comment { get; set; }
    }
}
