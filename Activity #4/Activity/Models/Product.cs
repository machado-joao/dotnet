using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [MaxLength(100)]
        public string Brand { get; set; }

        public int ProductTypeId { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}
