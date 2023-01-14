namespace NationalLandmarks.Server.Features.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateRoleRequestModel
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
