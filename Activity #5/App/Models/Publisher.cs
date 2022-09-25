using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Document { get; set; }
    }
}
