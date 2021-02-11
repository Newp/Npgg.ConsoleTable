using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Npgg
{
    public class ConsoleBarChartOption
    {
        public ConsoleColor BarColor { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
    public static class ConsoleBarChart
    {
        public static void Write(List<ConsoleBarChartOption> list)
        {
            var columnLength = list.Max(text => text.Name.Length);
            var maxValue = list.Max(item=>item.Value);

            foreach(var item in list)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;


                string format = $" {{0, {columnLength}}} ";

                Console.Write(format, item.Name);

                Console.BackgroundColor = item.BarColor;

                var length = (item.Value / maxValue ) * 50;
                Console.Write(string.Empty.PadLeft((int)length));

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = item.BarColor;
                Console.Write(" " + item.Value);

                Console.Write('\n');


            }
        }
    }
}
