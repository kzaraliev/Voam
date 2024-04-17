using System.ComponentModel.DataAnnotations;
using static Voam.Core.Constants.MessageConstants;

namespace Voam.Core.Models.Identity
{
    public class LoginUser
    {
        [Required(ErrorMessage = RequiredMessage)]
        public required string Email { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public required string Password { get; set; }
    }
}
