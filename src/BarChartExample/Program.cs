using System;
using System.Collections.Generic;
using Npgg;
namespace BarChartExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var dic = new List<ConsoleBarChartOption>()
            {
                new ConsoleBarChartOption(){
                    BarColor = ConsoleColor.Red,
                    Name = "Newtonsoft.Json",
                    Value = 2000,
                },
                new ConsoleBarChartOption(){
                    BarColor = ConsoleColor.Yellow,
                    Name = "Utf8Json",
                    Value = 800,
                },
                new ConsoleBarChartOption(){
                    BarColor = ConsoleColor.Blue,
                    Name = "System.Text.Json",
                    Value = 1500,
                }
            };

            ConsoleBarChart.Write(dic);
        }
    }
}
