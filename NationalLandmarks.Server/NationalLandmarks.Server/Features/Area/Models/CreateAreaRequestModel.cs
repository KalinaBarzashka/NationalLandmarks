namespace NationalLandmarks.Server.Features.Area.Models
{
    using System.ComponentModel.DataAnnotations;

    using static NationalLandmarks.Server.Data.Validation.Area;

    public class CreateAreaRequestModel
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }
    }
}
