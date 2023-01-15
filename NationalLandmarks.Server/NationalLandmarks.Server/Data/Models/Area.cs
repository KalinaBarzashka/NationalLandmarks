namespace NationalLandmarks.Server.Data.Models
{
    using NationalLandmarks.Server.Data.Models.Base;
    using System.ComponentModel.DataAnnotations;

    using static NationalLandmarks.Server.Data.Validation.Area;

    public class Area: IDeletableEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        public string? Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? CreatedByUsername { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedByUsername { get; set; }

        public virtual IEnumerable<Place> Places { get; set; } = new HashSet<Place>();
    }
}
