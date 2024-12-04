using System.ComponentModel.DataAnnotations;

namespace ConstructionManagement_Backend.Services
{
    public class UserRegisterRequest
    {
        public string Username { get; set; }

        [Required]
        public string Password { get; set; } // Hashed password

        [Required]
        public string Role { get; set; } // Admin, ProjectManager, Architect, etc.

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
