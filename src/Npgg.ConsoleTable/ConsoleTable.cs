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

        public static void WriteSingle<T>(T obj)
        {
            var properties = typeof(T).GetProperties();

            var members = MemberGetter.GetAssigners<T>()
                .Values
                .ToDictionary
                    (
                        assigner => assigner.MemberName,
                        assigner => assigner.GetValue(obj).ToString()
                    );

            Write(members);
        }

        public static void Write<K, V>(IDictionary<K, V> dictionary)
        {
            var members = dictionary.ToDictionary(kvp => kvp.Key.ToString(), kvp => kvp.Value.ToString());

            var columns = new ConsoleColumn[]
            {
                new ConsoleColumn(0, "Key", members.Keys.ToArray()),
                new ConsoleColumn(1, "Values", members.Values.ToArray())
            };

            WriteLine(columns);

            foreach (var member in members)
            {
                WriteWord(ConsoleTable.TableColor, 3, cc);
                WriteWord(ConsoleTable.ColumnColor, columns[0].GetWidth(member.Key), member.Key);
                WriteWord(ConsoleTable.TableColor, 3, cc);
                WriteWord(ConsoleTable.RowColor, columns[1].GetWidth(member.Value), member.Value);
                WriteWord(ConsoleTable.TableColor, 3, cc);

                Console.WriteLine();
                WriteLine(columns);
            }
        }


        public static int GetTextWidth(string value)
        {
            if (value == null)
                return 0;

            var length = value.ToCharArray().Sum(c => c > 127 ? 2 : 1);
            return length;
        }

        public static void Write<T>(IEnumerable<T> list)
        {

            var type = typeof(T);
            if (type.IsPrimitive || type == typeof(string))
            {
                var converted = list.Select(item => new { value = item });
                Write(converted, item => RowColor);
            }
            else
            {
                Write(list, item => RowColor);
            }
            
        }
        
        public static void Write<T>(IEnumerable<T> list, ColorSelector<T> colorSelector)
        {
            var properties = typeof(T).GetProperties();

            var assigners = MemberGetter.GetAssigners<T>();

            var columns = assigners.Values.Select((assigner, i) => 
                new ConsoleColumn(i, assigner.MemberName, list.Select(item => assigner.GetValue(item)?.ToString()))
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

        /// <summary>
        /// 테이블 색깔을 지정합니다.
        /// </summary>
        public static ConsoleColor TableColor = ConsoleColor.Cyan;

        /// <summary>
        /// 단, 대리자를 이용한 색상 지정일 경우에는 동작하지 않습니다.
        /// </summary>
        public static ConsoleColor RowColor = ConsoleColor.Gray;

        /// <summary>
        /// Column에 들어가는 Text 색상을 지정합니다.
        /// </summary>
        public static ConsoleColor ColumnColor = ConsoleColor.Yellow;

        static void WriteLine(IEnumerable<ConsoleColumn> columns)
        {
            var spliter = " +-";
            foreach (var column in columns)
            {
                WriteWord(TableColor, 3, spliter);
                var value = string.Empty.PadLeft(column.Width, '-');
                WriteWord(TableColor, column.Width, value);
                spliter = "-+-";
            }

            WriteWord(TableColor, 3, "-+ ");
            Console.WriteLine();
        }

        static void WriteRow(IEnumerable<ConsoleColumn> columns, ConsoleColor color, Func<ConsoleColumn, string> textSelector)
        {
            foreach (var column in columns)
            {
                WriteWord(TableColor, 3, cc);
                var value = textSelector(column);

                var width = column.GetWidth(value);

                WriteWord(color, width, value);
            }
            WriteWord(TableColor, 3, cc);
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
