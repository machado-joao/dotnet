using Activity.Models;
using Activity.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Activity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Example: ['Work', 11], ['Eat', 2], ['Commute', 2], ['Watch TV', 2], ['Sleep', 7]

            // Pie Chart Generation
            var resultPieChart = Sales.GenerateSalesList().GroupBy(x => x.Product).Select(g =>
                new { g.Key, TotalSales = g.Sum(x => x.Price) });

            StringBuilder pieChartData = new StringBuilder();
            foreach (var item in resultPieChart)
            {
                pieChartData.Append("['" + item.Key + "'," + item.TotalSales.ToString().Replace(",", ".") + "],");
            }
            string dataWithCommaAtTheEnd = pieChartData.ToString();
            string dataWithoutCommaAtTheEnd = dataWithCommaAtTheEnd.Substring(0, dataWithCommaAtTheEnd.Length - 1);

            ViewBag.PieChart = Functions.GeneratePieChart("Total of sales by Product", dataWithoutCommaAtTheEnd);

            // Column Chart Generation
            var resultColumnChart = Sales.GenerateSalesList().GroupBy(x => x.Category).Select(g =>
                new { g.Key, TotalSales = g.Sum(x => x.Price) });

            StringBuilder topColumnChartData = new StringBuilder("[''");
            StringBuilder bottomColumnChartData = new StringBuilder("['Categories'");
            foreach (var item in resultColumnChart)
            {
                topColumnChartData.Append(",'" + item.Key + "'");
                bottomColumnChartData.Append("," + item.TotalSales.ToString().Replace(",", "."));
            }
            topColumnChartData.Append("],");
            bottomColumnChartData.Append("]");

            ViewBag.ColumnChart = Functions.GenerateColumnOrBarChart("Total of sales by Category", "", topColumnChartData.ToString() + bottomColumnChartData.ToString(), false);

            // Bar Chart Generation
            var resultBarChart = Sales.GenerateSalesList().GroupBy(x => new { x.Date.Month, x.Category }).Select(g =>
                new { g.Key.Month, g.Key.Category, TotalSales = g.Sum(x => x.Price) }).OrderBy(x => x.Month).ThenBy(x => x.Category);
            var categories = resultBarChart.OrderBy(x => x.Category).Select(x => x.Category).Distinct().ToList();
            var months = resultBarChart.OrderBy(x => x.Month).Select(x => x.Month).Distinct().ToList();

            string topBarChartData = "['Month','" + string.Join("','", categories.ToArray()) + "'],";
            string bottomBarChartData = "";
            foreach (var month in months)
            {
                bottomBarChartData += "['" + DateTimeFormatInfo.CurrentInfo.GetMonthName(month) + "',";
                foreach (var category in categories)
                {
                    if (resultBarChart.Where(x => x.Month == month && x.Category == category).ToList().Count > 0)
                    {
                        bottomBarChartData += resultBarChart.Where(x => x.Month == month && x.Category == category).ToList().FirstOrDefault().TotalSales.ToString().Replace(",", ".") + ",";
                    }
                    else
                    {
                        bottomBarChartData += "0,";
                    }
                }
                bottomBarChartData = bottomBarChartData.Substring(0, bottomBarChartData.Length - 1);
                bottomBarChartData += "],";
            }
            bottomBarChartData = bottomBarChartData.Substring(0, bottomBarChartData.Length - 1);

            ViewBag.BarChart = Functions.GenerateColumnOrBarChart("Total of Sales", "Sales by Period", topBarChartData + bottomBarChartData, true);

            return View();
        }
    }
}
