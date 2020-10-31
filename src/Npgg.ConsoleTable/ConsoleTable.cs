using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Npgg
{
    public static class ConsoleTable
    {

        class Column
        {
            public int index;
            public string name;
            public int width;
            public string[] values;
        }

        public static void Write<T>(IEnumerable<T> list, Func<T, ConsoleColor> rowColorSelector)
        {
            var ps = typeof(T).GetProperties();

            var assigners = MemberGetter.GetAssigners<T>();

            var columns = new List<Column>();

            int idx = 0;
            foreach (var assigner in assigners.Values)
            {
                string key = assigner.MemberName;
                var values = list.Select(item => assigner.GetValue(item).ToString());

                int maxLength = Math.Max(key.Length, values.Max(item => item.Length));

                columns.Add(new Column()
                {
                    index = idx++,
                    name = key,
                    width = maxLength,
                    values = values.ToArray()
                }) ;
            }



            var totalWidth = columns.Sum(column => column.width);

            WriteLine(columns);

            WriteColumn(columns, ConsoleColor.Yellow, col => col.name);

            WriteLine(columns);

            for (int i = 0; i < list.Count(); i++)
            {
                var color = rowColorSelector(list.ElementAt(i));
                WriteRow(columns, color, col => col.values[i]);
            }


            WriteLine(columns);
        }


        static readonly string cc = " | ";

        static readonly ConsoleColor tableColor = ConsoleColor.Cyan;
        static void WriteColumn(List<Column> columns, ConsoleColor color, Func<Column, string> selector)
        {
            foreach (var column in columns)
            {
                Write(tableColor, 3, cc);
                var value = selector(column);
                Write(color, column.width, value);
            }

            Write(tableColor, 3, cc);
            Console.WriteLine();
        }

        static void WriteLine(List<Column> columns)
        {
            var spliter = " +-";
            foreach (var column in columns)
            {
                Write(tableColor, 3, spliter);
                var value = string.Empty.PadLeft(column.width, '-');
                Write(tableColor, column.width, value);
                spliter = "-+-";
            }

            Write(tableColor, 3, "-+ ");
            Console.WriteLine();
        }

        static void WriteRow(List<Column> columns, ConsoleColor color, Func<Column, string> selector)
        {
            foreach (var column in columns)
            {
                Write(tableColor, 3, cc);
                var value = selector(column);
                Write(color, column.width, value);
            }
            Write(tableColor, 3, cc);
            Console.WriteLine();
        }


        static void Write(ConsoleColor consoleColor, int width, string value)
        {
            Console.ForegroundColor = consoleColor;
            
            string format = $"{{0, {width}}}";

            Console.Write(format, value);

            Console.ResetColor();
        }
        //public static void Write<T>(IEnumerable<T> list) => Console.WriteLine(Create(list));

        //public static string Create<T>(IEnumerable<T> list)
        //{
        //    var ps = typeof(T).GetProperties();

        //    var assigners = MemberGetter.GetAssigners<T>();

        //    var readyList = new List<(int index, string name, int length, string[] values)>();

        //    int idx = 0;
        //    foreach (var assigner in assigners.Values)
        //    {
        //        string key = assigner.MemberName;
        //        var values = list.Select(item => assigner.GetValue(item).ToString());
        //        int maxLength = Math.Max(key.Length, values.Max(item => item.Length));

        //        readyList.Add((idx++, key, maxLength, values.ToArray()));
        //    }

        //    StringBuilder sb = new StringBuilder();
        //    var cc = " | ";
        //    var lineFormat = cc + string.Join(cc, readyList.Select(item => $"{{{item.index},{item.length}}}")) + cc;

        //    var drawHorizontalLine = new Action(() => sb.AppendFormat(lineFormat, readyList.Select(item => "".PadLeft(item.length, '=')).ToArray()).AppendLine());

        //    drawHorizontalLine();//==============================================

        //    sb.AppendFormat(lineFormat, readyList.Select(item => item.name).ToArray()).AppendLine();

        //    drawHorizontalLine();//==============================================

        //    for (int i = 0; i < list.Count(); i++)
        //    {
        //        var rowValues = readyList.Select(item => item.values[i].ToString());
        //        sb.AppendFormat(lineFormat, rowValues.ToArray()).AppendLine();
        //    }

        //    drawHorizontalLine();//==============================================
        //    sb.Length--;
        //    return sb.ToString();
        //}

        class Builder
        {
            StringBuilder sb = new StringBuilder();
            public string format { get; set; }
            

        }
    }
}
