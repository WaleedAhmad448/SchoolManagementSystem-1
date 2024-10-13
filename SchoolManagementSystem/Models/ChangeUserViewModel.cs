using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class ChangeUserViewModel
    {
        // FirstName is required
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        // LastName is required
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Address with a max length of 100 characters
        [MaxLength(100, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string Address { get; set; }

        // PhoneNumber with a max length of 20 characters
        [MaxLength(20, ErrorMessage = "The field {0} can only contain {1} characters")]
        public string PhoneNumber { get; set; }
    }
}
