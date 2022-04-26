namespace NationalLandmarks.Server.Features.Town.Models
{
    using System.ComponentModel.DataAnnotations;

    using static NationalLandmarks.Server.Data.Validation.Town;

    public class UpdateTownRequestModel
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public int AreaId { get; set; }
    }
}
