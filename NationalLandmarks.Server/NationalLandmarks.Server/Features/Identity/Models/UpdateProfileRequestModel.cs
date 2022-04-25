namespace NationalLandmarks.Server.Features.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    using static NationalLandmarks.Server.Data.Validation.User;

    public class UpdateProfileRequestModel
    {
        [MaxLength(MaxFirstNameLength)]
        public string FirstName { get; set; }

        [MaxLength(MaxLastNameLength)]
        public string LastName { get; set; }

        public string UserName { get; set; }

        public string? ProfileImageUrl { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
