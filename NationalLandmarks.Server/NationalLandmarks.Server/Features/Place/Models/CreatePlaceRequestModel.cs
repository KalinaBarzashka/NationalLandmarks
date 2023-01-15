namespace NationalLandmarks.Server.Features.Place.Models
{
    using System.ComponentModel.DataAnnotations;

    using static NationalLandmarks.Server.Data.Validation.Place;

    public class CreatePlaceRequestModel
    {
        [Required]
        [MaxLength(MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public int AreaId { get; set; }
    }
}
