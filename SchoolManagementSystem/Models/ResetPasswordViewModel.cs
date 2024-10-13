﻿using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.Models
{
    public class ResetPasswordViewModel
    {
        // Required field for the username
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        // Required field for the password
        // DataType sets the field to be treated as a password input
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Required field for password confirmation
        // Ensures the confirmation matches the original password
        [Required(ErrorMessage = "Password confirmation is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        // Required token for password reset validation
        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; }
    }
}
