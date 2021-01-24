using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Transactions;
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

        readonly MemberInfo[] memberInfos;
        public UnitTests()
        {
            this.memberInfos = typeof(Sample).GetMembers()
                .Where(mem => mem.MemberType == System.Reflection.MemberTypes.Property
                    || mem.MemberType == System.Reflection.MemberTypes.Field).ToArray();
        }

        public string Write(Action action)
        {
            var stdout = Console.Out;

            MockConsole mockConsole = new MockConsole();

            Console.SetOut(mockConsole);
            action();
            Console.SetOut(stdout);

            return mockConsole.Read();
        }

        [Fact]
        public void LineCountTest()
        {
            var result = Write(() => ConsoleTable.Write(list, item => ConsoleColor.White));

            //line 3+ column 1 + newline 1
            Assert.Equal(list.Count + 5, result.Split('\n').Length);
        }


        [Fact]
        public void ColumnTest()
        {
            var result = Write(() => ConsoleTable.Write(list, item => ConsoleColor.White));

            //line 3+ column 1 + newline 1
            StringReader reader = new StringReader(result);
            reader.ReadLine();//빈줄
            var raw = reader.ReadLine();
            var column = splite(raw);


            Assert.Equal(this.memberInfos.Length, column.Length);

            for (int i = 0; i < column.Length; i++)
            {
                Assert.Equal(this.memberInfos[i].Name, column[i]);
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


        [Fact]
        public void SingleWriteTest()
        {
            var sample = new Sample(10, "name_value", "message_value", new DateTime(2020, 10, 30));

            var result = Write(() => ConsoleTable.WriteSingle(sample));

            using var reader = new StringReader(result);

            reader.ReadLine();


            foreach (var assginer in MemberGetter.GetAssigners(typeof(Sample)).Values)
            {
                var text = reader.ReadLine();

                var values = text.Split('|').Select(value => value.Trim()).ToArray();

                Assert.Equal(4, values.Length); // 왼쪽 공백+오른쪽공백

                Assert.Equal(0, values[0].Length);

                Assert.Equal(assginer.MemberName, values[1]);
                Assert.Equal(assginer.GetValue(sample).ToString(), values[2]);

                Assert.Equal(0, values[3].Length);


                reader.ReadLine();
            }
        }


        [Fact]
        public void SingleWritePrimitiveTest()
        {
            var result = Write(() => ConsoleTable.Write(new[] { 1, 2, 3, 4 }));

            using var reader = new StringReader(result);

            var read = reader.ReadToEnd();

            var expect =
@" +-------+ 
 | value | 
 +-------+ 
 |     1 | 
 |     2 | 
 |     3 | 
 |     4 | 
 +-------+ 
";
            Assert.Equal(expect, read);

        }
    }
}
