using System;
using System.Collections.Generic;
using System.Linq;

namespace Npgg
{
    class ConsoleColumn
    {
        public int Index;
        public string Name;
        public int Width;
        public string[] Values;

        public ConsoleColumn(int index, string name, IEnumerable<string> values)
        {
            this.Index = index;
            this.Name = name;
            this.Values = values.ToArray();


            bool emptyValues = values == null || values.Count() == 0;
            
            this.Width = Math.Max(
                name.Length
                , emptyValues ? 0 : values.Max(text => ConsoleTable.GetTextWidth(text)));
            
        }

        public int GetWidth(string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;

            var len1 = ConsoleTable.GetTextWidth(value);
            var len2 = value.Length;

            var diff = len1 - len2;

            return this.Width - diff;
        }
    }
}
