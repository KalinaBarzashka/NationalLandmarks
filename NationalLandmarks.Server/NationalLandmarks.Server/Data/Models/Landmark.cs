namespace NationalLandmarks.Server.Data.Models
{
    using NationalLandmarks.Server.Data.Models.Base;
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Validation.Landmark;

    public class Landmark: IDeletableEntity
    {
        public int Id { get; set; }

        public string? RegistrationNumber { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public bool IsNationalLandmark { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public int PlaceId { get; set; }

        public Place Place { get; set; }

        [MaxLength(MaxAddressLength)]
        public string Address { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string? WorkingTime { get; set; }

        //public string? Opens { get; set; }

        //public string? Closes { get; set; }

        public bool? WorksOnWeekends { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Website { get; set; }

        //[Required]
        //public bool HasSeal { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public virtual IEnumerable<Visit> Visits { get; set; } = new HashSet<Visit>();

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? CreatedByUsername { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedByUsername { get; set; }
    }
}
