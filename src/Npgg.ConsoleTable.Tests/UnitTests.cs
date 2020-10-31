using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Npgg.ConsoleTableTests
{
    public class UnitTests
    {
        class Sample
        {
            public Sample(int id, string name, string message, DateTime registerTime)
            {
                Id = id;
                Name = name;
                Message = message;
                RegisterTime = registerTime;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Message { get; set; }
            public DateTime RegisterTime { get; set; }
        }

        List<Sample> list = new List<Sample>(new[]{
                new Sample(1, "재훈", "첫빠", new DateTime(2020,10,29)),
                new Sample(2, "초코파이 찰떡", "맛있음", new DateTime(2020,10,30)),
                new Sample(3, "renoir", "출시하긴 하냐", new DateTime(2020,11,5)),
                new Sample(4, "모니터", "너무비쌈", new DateTime(2020,11,5)),
                new Sample(5, "3080", "팔지도 않는다..", new DateTime(2020,11,5)),
                new Sample(6, "사무실 이사", "안갔으면", new DateTime(2020,11,5)),

            });

        public string Write<T>(IEnumerable<T> list) => Write<T>(list, item => ConsoleColor.White);

        public string Write<T>(IEnumerable<T> list, ColorSelector<T> colorSelector)
        {
            var stdout = Console.Out;

            MockConsole mockConsole = new MockConsole();

            Console.SetOut(mockConsole);
            ConsoleTable.Write(list, colorSelector);
            Console.SetOut(stdout);

            return mockConsole.Read();
        }

        [Fact]
        public void LineCountTest()
        {
            var result = this.Write(list);

            //line 3+ column 1 + newline 1
            Assert.Equal(list.Count + 5, result.Split('\n').Length);
        }


        [Fact]
        public void ColumnTest()
        {
            var result = this.Write(list);

            //line 3+ column 1 + newline 1
            StringReader reader = new StringReader(result);
            reader.ReadLine();//빈줄
            var raw = reader.ReadLine();
            var column = splite(raw);

            var members = typeof(Sample).GetMembers()
                .Where(mem => mem.MemberType == System.Reflection.MemberTypes.Property
                    || mem.MemberType == System.Reflection.MemberTypes.Field).ToArray();


            Assert.Equal(members.Length, column.Length);

            for (int i = 0; i < column.Length; i++)
            {
                Assert.Equal(members[i].Name, column[i]);
            }
        }

        string[] splite(string line) => line.Split("|").Select(text => text.Trim()).Where(text => string.IsNullOrEmpty(text) == false).ToArray();

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
