namespace NationalLandmarks.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using NationalLandmarks.Server.Data.Models.Base;
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Validation.User;

    public class User: IdentityUser, IDeletableEntity
    {
        [Required]
        [MaxLength(MaxFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(MaxLastNameLength)]
        public string LastName { get; set; }

        public string? ProfileImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    
        public string? DeletedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? CreatedByUsername { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedByUsername { get; set; }

        public virtual IEnumerable<Landmark> Landmarks { get; } = new HashSet<Landmark>();

        public virtual IEnumerable<Visit> Visits { get; set; }
    }
}
