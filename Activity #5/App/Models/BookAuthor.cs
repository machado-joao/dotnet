using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class BookAuthor
    {
        [Key]
        public int Id { get; set; }

        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public virtual Book Book { get; set; }

        public virtual Author Author { get; set; }
    }
}
