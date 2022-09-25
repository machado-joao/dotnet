using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The field title is required!")]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "The field publisher is required!")]
        [MaxLength(200)]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "The field author is required!")]
        [MaxLength(200)]
        public string Author { get; set; }
    }
}
