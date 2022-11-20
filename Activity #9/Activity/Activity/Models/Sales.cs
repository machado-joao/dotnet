using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Activity.Models
{
    public class Sales
    {
        public string Product { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }

        public Double Price { get; set; }

        public static List<Sales> GenerateSalesList()
        {
            List<Sales> sales = new List<Sales>();

            sales.Add(new Sales
            {
                Category = "Shoes",
                Date = DateTime.Parse("2020-05-21"),
                Product = "Product 1",
                Price = 100.00
            });
            sales.Add(new Sales
            {
                Category = "Clothes",
                Date = DateTime.Parse("2020-05-21"),
                Product = "Product 2",
                Price = 125.36
            });
            sales.Add(new Sales
            {
                Category = "Clothes",
                Date = DateTime.Parse("2020-04-21"),
                Product = "Product 3",
                Price = 130.45
            });
            sales.Add(new Sales
            {
                Category = "Shoes",
                Date = DateTime.Parse("2020-04-21"),
                Product = "Product 4",
                Price = 110.77
            });
            sales.Add(new Sales
            {
                Category = "Shoes",
                Date = DateTime.Parse("2020-04-21"),
                Product = "Product 5",
                Price = 100.21
            });
            sales.Add(new Sales
            {
                Category = "Clothes",
                Date = DateTime.Parse("2020-03-21"),
                Product = "Product 6",
                Price = 140.33
            });
            sales.Add(new Sales
            {
                Category = "Shoes",
                Date = DateTime.Parse("2020-03-21"),
                Product = "Product 7",
                Price = 90.55
            });;

            return sales;
        }
    }
}
