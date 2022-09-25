using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class Product
    {
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "The min. length is 5 and max. length is 200!")]
        [Display(Name = "Product's description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [Display(Name = "Product's price")]
        public decimal Price { get; set; }

        [Display(Name = "Product's brand")]
        public string Brand { get; set; }
    }
}
