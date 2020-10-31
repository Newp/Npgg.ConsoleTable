using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Npgg
{
    public delegate ConsoleColor ColorSelector<T>(T item);

    public static class ConsoleTable
    {

        public static int GetTextWidth(string value)
        {
            var length = value.ToCharArray().Sum(c => c > 127 ? 2 : 1);
            return length;
        }

        public static void Write<T>(IEnumerable<T> list, ColorSelector<T> colorSelector)
        {
            var properties = typeof(T).GetProperties();

            var assigners = MemberGetter.GetAssigners<T>();

            var columns = assigners.Values.Select((assigner, i) => 
                new ConsoleColumn(i, assigner.MemberName, list.Select(item => assigner.GetValue(item).ToString()))
            );

            var totalWidth = columns.Sum(column => column.Width);

            WriteLine(columns);

            WriteRow(columns, ConsoleColor.Yellow, col => col.Name);

            WriteLine(columns);

            for (int i = 0; i < list.Count(); i++)
            {
                var color = colorSelector(list.ElementAt(i));
                WriteRow(columns, color, col => col.Values[i]);
            }


            WriteLine(columns);
        }


        static readonly string cc = " | ";

        static readonly ConsoleColor tableColor = ConsoleColor.Cyan;

        static void WriteLine(IEnumerable<ConsoleColumn> columns)
        {
            var spliter = " +-";
            foreach (var column in columns)
            {
                WriteWord(tableColor, 3, spliter);
                var value = string.Empty.PadLeft(column.Width, '-');
                WriteWord(tableColor, column.Width, value);
                spliter = "-+-";
            }

            WriteWord(tableColor, 3, "-+ ");
            Console.WriteLine();
        }

        static void WriteRow(IEnumerable<ConsoleColumn> columns, ConsoleColor color, Func<ConsoleColumn, string> textSelector)
        {
            foreach (var column in columns)
            {
                WriteWord(tableColor, 3, cc);
                var value = textSelector(column);

                var len1 = GetTextWidth(value);
                var len2 = value.Length;

                var diff = len1 - len2;

                WriteWord(color, column.Width - diff, value);
            }
            WriteWord(tableColor, 3, cc);
            Console.WriteLine();
        }


        static void WriteWord(ConsoleColor consoleColor, int width, string value)
        {
            Console.ForegroundColor = consoleColor;
            
            string format = $"{{0, {width}}}";

            Console.Write(format, value);

            Console.ResetColor();
        }
    }
}
