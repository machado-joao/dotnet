using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Profile description")]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
