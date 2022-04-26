namespace NationalLandmarks.Server.Data.Models
{
    using NationalLandmarks.Server.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;

    using static Validation.Landmark;

    public class Landmark: DeletableEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public bool IsNationalLandmark { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public int TownId { get; set; }

        public Town Town { get; set; }

        [Required]
        [MaxLength(MaxAddressLength)]
        public string Address { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }

        public string? Opens { get; set; }

        public string? Closes { get; set; }

        public bool? WorksOnWeekends { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Website { get; set; }

        //[Required]
        //public bool HasSeal { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public virtual IEnumerable<Visit> Visits { get; set; }
    }
}
