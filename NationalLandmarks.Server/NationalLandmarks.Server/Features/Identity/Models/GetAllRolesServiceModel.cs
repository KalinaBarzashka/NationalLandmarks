namespace NationalLandmarks.Server.Features.Identity.Models
{
    public class GetAllRolesServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; } = true;

        public string? Description { get; set; }
    }
}
