using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Activity.Models
{
    public class Book
    {
        [Required(ErrorMessage = "The title is a required field!")]
        [Display(Name = "Book's name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The minimum length is 2!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The publisher is a required field!")]
        [Display(Name = "Book's publisher")]
        [MaxLength(50, ErrorMessage = "The maximum length is 50!")]
        [RegularExpression("^[a-zA-z ]+$", ErrorMessage = "Only letters are allowed!")]
        public string Publisher { get; set; }
    }
}
