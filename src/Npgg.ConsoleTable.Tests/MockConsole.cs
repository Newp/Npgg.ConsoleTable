using System.IO;
using System.Text;

namespace Npgg.ConsoleTableTests
{
    public class MockConsole : TextWriter
    {
        public override Encoding Encoding => new UTF8Encoding(false);

        StringBuilder sb = new StringBuilder();
        public override void Write(string value)
        {
            sb.Append(value);
            base.Write(value);
        }

        public override void WriteLine()
        {
            sb.AppendLine();
            base.WriteLine();
        }
        public override void WriteLine( string value )
        {
            sb.AppendLine(value);
            base.WriteLine(value);
        }

        public string Read() => sb.ToString();
    }
}
