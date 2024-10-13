using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(50, ErrorMessage = "O campo {0} só pode conter {1} caracteres")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "O campo {0} só pode conter {1} caracteres")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "O campo {0} só pode conter {1} caracteres")]
        public string? Address { get; set; }

        [Display(Name = "Profile Picture")]
        public Guid? ProfilePictureId { get; set; }

        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Last Login")]
        public DateTime? LastLogin { get; set; }
    }
}
