using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
