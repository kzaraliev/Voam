using System.ComponentModel.DataAnnotations;
using static Voam.Core.Constants.MessageConstants;

namespace Voam.Core.Models.Identity
{
    public class RegisterUser
    {
        [Required(ErrorMessage = RequiredMessage)]
        public required string Email { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public required string Password { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public required string PhoneNumber { get; set; }
    }
}
