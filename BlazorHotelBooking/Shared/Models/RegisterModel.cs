﻿using System.ComponentModel.DataAnnotations;

namespace BlazorHotelBooking.Shared.Models
{
    public class RegisterModel
    {
        [Required, EmailAddress, Display(Name = "Email")]
        public string? Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Password"),
            StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string? Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Confirm Password"),
            Compare("Password", ErrorMessage="Passwords Do Not Match")]
        public string? ConfirmPassword { get; set; }

        [Required, Display(Name = "Passport Number")]
        public string? PassportNumber { get; set; }
    }
}
