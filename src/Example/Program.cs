using System;
using Npgg;

namespace Example
{
    class Program
    {

        class People
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Job { get; set; }
        }

        static void Main(string[] args)
        {
            var peoples = new[]{ 
                new People() { Name = "에스텔 브라이트", Age = 16, Job = "유격사" }


            };

            ConsoleTable.Write(peoples);
        }
    }
}
