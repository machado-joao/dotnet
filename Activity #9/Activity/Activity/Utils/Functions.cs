using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Activity.Utils
{
    public class Functions
    {
        public static string GeneratePieChart(string title, string data)
        {
            string chart = @"<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>
                <script type='text/javascript'>
                google.charts.load('current', {packages:['corechart']});
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {
                var data = google.visualization.arrayToDataTable([
                ['', ''],
                " + data + @"
                ]);
                var options = {
                title: '" + title + @"',
                is3D: true, };
                var chart = new google.visualization.PieChart(document.getElementById('piechart_" +
                 title.Replace(" ", "") + @"'));
                chart.draw(data, options);
                }
                </script>
                <div id='piechart_" + title.Replace(" ", "") + @"' style='min-height: 500px;'></div>";

            return chart;
        }

        public static string GenerateColumnOrBarChart(string title, string subtitle, string data, bool isBarChart)
        {
            /* Example:
                ['Year', 'Sales', 'Expenses', 'Profit'],
                ['2014', 1000, 400, 200],
                ['2015', 1170, 460, 250],
                ['2016', 660, 1120, 300],
                ['2017', 1030, 540, 350] */

            string chartType = null;
            if (isBarChart)
            {
                chartType = "bars: 'horizontal'";
            }

            string chart = @"<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>
                <script type='text/javascript'>
                google.charts.load('current', {'packages':['bar']});
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {
                var data = google.visualization.arrayToDataTable([
                " + data + @" ]);
                var options = {
                chart: {
                title: '" + title + @"',
                subtitle: '" + subtitle + @"', },
                " + chartType + @"
                };
                var chart = new google.charts.Bar(document.getElementById('barchart_" + title.Replace(" ", "") + @"'));
                chart.draw(data, google.charts.Bar.convertOptions(options));
                }
                </script>
                <div id='barchart_" + title.Replace(" ", "") + @"' style='min-height: 500px;'></div>";

            return chart;
        }
    }
}
