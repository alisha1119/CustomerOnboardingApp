﻿using System.ComponentModel.DataAnnotations;

namespace CustomerOnboardingApp.Presentation.ViewModels.Login
{
    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
