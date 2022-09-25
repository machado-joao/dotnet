using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Edition { get; set; }

        [Display(Name = "Number of pages")]
        public int Pages { get; set; }

        public int Year { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<BookAuthor> BookAuthor { get; set; }
    }
}
