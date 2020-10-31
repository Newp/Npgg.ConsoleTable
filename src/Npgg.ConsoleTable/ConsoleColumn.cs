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
            this.Width = Math.Max(name.Length, values.Max(text => ConsoleTable.GetTextWidth(text)));
        }
    }
}
