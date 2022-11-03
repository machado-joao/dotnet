using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        public int PersonId { get; set; }

        public virtual Person Person { get; set; }

        [MaxLength(100)]
        public string Photo { get; set; }
    }
}
