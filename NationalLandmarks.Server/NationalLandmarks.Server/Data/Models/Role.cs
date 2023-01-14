namespace NationalLandmarks.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class Role : IdentityRole
    {
        public bool? IsActive { get; set; } = true;

        public string? Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    }
}
