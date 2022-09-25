using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Activity.Models
{
    public class Student
    {
        [Required]
        [Display(Name = "Enrollment number")]
        [Range(1, 100)]
        public int Enrollment { get; set; }

        [Required]
        [Display(Name = "Student's name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Student's course")]
        public string Course { get; set; }

        [Required]
        [Display(Name = "Is active?")]
        public bool Active { get; set; }
    }
}
