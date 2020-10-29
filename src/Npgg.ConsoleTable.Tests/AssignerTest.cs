using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Npgg.ConsoleTable.Tests
{
    public class AssignerTest
    {
        readonly Dictionary<string, MemberGetter> assingers = MemberGetter.GetAssigners<Sample>();

        class Sample
        {
            public string Name { get; set; }


            public int Age { get; set; }

            public string Value { get; set; }

        }

        [Fact]
        public void CountTest()
        {
            Assert.Equal(3, assingers.Count);
        }


        [Fact]
        public void OrderTest()
        {
            Assert.Equal(nameof(Sample.Name), assingers.First().Key);
            Assert.Equal(nameof(Sample.Value), assingers.Last().Key);
        }



        [Fact]
        public void GetValueTest()
        {
            var sample = new Sample() { Name = "name", Value = "valuevalue", Age = 200 };

            Assert.Equal(assingers[nameof(Sample.Name)].GetValue(sample), sample.Name);
            Assert.Equal(assingers[nameof(Sample.Value)].GetValue(sample), sample.Value);
        }


    }
}
