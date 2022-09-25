using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Description { get; set; }

        [EnumDataType(typeof(Type))]
        [Required]
        public Type Status { get; set; }

        public enum Type
        {
            ACTIVE = 1,
            INACTIVE = 0
        }
    }
}
