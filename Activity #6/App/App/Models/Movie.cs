using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(20)]
        public string Category { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [MaxLength(200)]
        public string Synopsis { get; set; }
    }
}
