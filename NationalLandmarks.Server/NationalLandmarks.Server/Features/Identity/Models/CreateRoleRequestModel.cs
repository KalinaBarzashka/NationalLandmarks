using System.ComponentModel.DataAnnotations;

namespace NationalLandmarks.Server.Features.Identity.Models
{
    public class CreateRoleRequestModel
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
