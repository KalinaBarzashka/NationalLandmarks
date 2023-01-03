namespace NationalLandmarks.Server.Features.Visit.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CreateVisitRequestModel
    {
        [Required]
        public int LandmarkId { get; set; }

        public int? Grade { get; set; }

        public string? Comment { get; set; }
    }
}
