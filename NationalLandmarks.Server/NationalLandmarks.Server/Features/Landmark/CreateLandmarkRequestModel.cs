namespace NationalLandmarks.Server.Features.Landmark
{
    using System.ComponentModel.DataAnnotations;

    using static Data.Validation.Landmark;

    public class CreateLandmarkRequestModel
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public string IsNationalLandmark { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(MaxAddressLength)]
        public string Address { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }

        public string? Opens { get; set; }

        public string? Closes { get; set; }

        public string? WorksOnWeekends { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Website { get; set; }

        [Required]
        public string HasSeal { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
