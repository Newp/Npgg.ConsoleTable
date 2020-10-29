using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Npgg
{
    public static class ConsoleTable
    {
        public static void Write<T>(IEnumerable<T> list) => Console.WriteLine(Create(list));

        public static string Create<T>(IEnumerable<T> list)
        {
            var ps = typeof(T).GetProperties();

            var assigners = MemberGetter.GetAssigners<T>();

            var readyList = new List<(int index, string name, int length, string[] values)>();

            int idx = 0;
            foreach (var assigner in assigners.Values)
            {
                string key = assigner.MemberName;
                var values = list.Select(item => assigner.GetValue(item).ToString());
                int maxLength = Math.Max(key.Length, values.Max(item => item.Length));

                readyList.Add((idx++, key, maxLength, values.ToArray()));
            }

            StringBuilder sb = new StringBuilder();
            var cc = " | ";
            var lineFormat = cc + string.Join(cc, readyList.Select(item => $"{{{item.index},{item.length}}}")) + cc;

            var drawHorizontalLine = new Action(() => sb.AppendFormat(lineFormat, readyList.Select(item => "".PadLeft(item.length, '=')).ToArray()).AppendLine());

            drawHorizontalLine();//==============================================

            sb.AppendFormat(lineFormat, readyList.Select(item => item.name).ToArray()).AppendLine();

            drawHorizontalLine();//==============================================

            for (int i = 0; i < list.Count(); i++)
            {
                var rowValues = readyList.Select(item => item.values[i].ToString());
                sb.AppendFormat(lineFormat, rowValues.ToArray()).AppendLine();
            }

            drawHorizontalLine();//==============================================
            sb.Length--;
            return sb.ToString();
        }
    }
}
