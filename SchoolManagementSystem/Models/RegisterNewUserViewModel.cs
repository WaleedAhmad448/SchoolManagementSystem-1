﻿using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class RegisterNewUserViewModel
    {
        // FirstName is required
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        // LastName is required
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Username (email) is required
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        // Address with a max length of 100 characters
        [MaxLength(100, ErrorMessage = "The field {0} can only contain {1} characters.")]
        public string Address { get; set; }

        // PhoneNumber with a max length of 20 characters and allows only digits, spaces, and special characters like "+" and "-"
        [MaxLength(20, ErrorMessage = "The field {0} can only contain {1} characters.")]
        [RegularExpression(@"^\+?[0-9\s\-()]*$", ErrorMessage = "The field {0} must contain only numbers and valid phone characters.")]
        public string PhoneNumber { get; set; }

        // Password is required with a minimum length of 6
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least {1} characters long.")]
        public string Password { get; set; }

        // Confirm password, must match the Password field
        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string Confirm { get; set; }

        // TemporaryPassword to hold the generated password
        public string? TemporaryPassword { get; set; }
    }
}
