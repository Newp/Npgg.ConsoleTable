using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Npgg.ConsoleTableTests
{
    public class UnitTests
    {
        [Theory]
        [InlineData("한글123", 7)]
        [InlineData("!@#$%^&*()1234567890", 20)]
        [InlineData("`~", 2)]
        [InlineData("\\|", 2)]
        [InlineData(";:'\"", 4)]
        [InlineData(",<.>/?", 6)]

        public void GetWidthTest(string value, int expectLength)
        {
            var actualLength = ConsoleTable.GetTextWidth(value);

            Assert.Equal(expectLength, actualLength);
        }
    }
}
