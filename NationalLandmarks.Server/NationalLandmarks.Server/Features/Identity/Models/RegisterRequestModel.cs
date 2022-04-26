namespace NationalLandmarks.Server.Features.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    using static NationalLandmarks.Server.Data.Validation.User;

    public class RegisterRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(MaxFirstNameLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(MaxLastNameLength)]
        public string LastName { get; set; }
    }
}
