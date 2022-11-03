using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Photo { get; set; }

        public ICollection<Album> Albums { get; set; }
    }
}
