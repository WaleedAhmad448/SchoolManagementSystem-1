using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class LoginViewModel
    {
        // Username (email) is required
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        // Password is required and must be at least 6 characters long
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        // Option to remember the user, so they don't have to log in again
        public bool RememberMe { get; set; }
    }
}
