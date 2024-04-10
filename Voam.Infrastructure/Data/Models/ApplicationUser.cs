using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Voam.Infrastructure.Data.Constants;

namespace Voam.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(ApplicationUserConstants.FullNameMax)]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(ApplicationUserConstants.FullNameMax)]
        public required string LastName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
