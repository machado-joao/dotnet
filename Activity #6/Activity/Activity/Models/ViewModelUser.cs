using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class Registration
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,12})", ErrorMessage = "The password must contain: 1 upper letter, 1 lower letter, 1 number, and at least six chars length.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Retype the password")]
        public string PasswordConfirmation { get; set; }
    }

    public class Access
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,12})", ErrorMessage = "The password must contain: 1 upper letter, 1 lower letter, 1 number, and at least six chars length.")]
        public string Password { get; set; }
    }

    public class Message
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        public string Body { get; set; }
    }

    public class ForgotPassword
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }

    public class ChangePassword
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,12})",
        ErrorMessage = "The password must contain: 1 upper letter, 1 lower letter, 1 number, and at least six chars length.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Hash { get; set; }
    }
}
