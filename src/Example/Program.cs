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
                new People() { Name = "John", Age = 16, Job = "Sailer"},
                new People() { Name = "Newp Lee", Age = 36, Job = "baeksoo" },
                new People() { Name = "Hatsune Miku", Age = 10, Job = "Singer" },
            };

            //var peoples = new[]{
            //    new People() { Name = "에스텔 브라이트", Age = 16, Job = "유격사"},
            //    new People() { Name = "브라이트 노아", Age = 45, Job = "함장"},
            //    new People() { Name = "Newp Lee", Age = 36, Job = "무직" },
            //    new People() { Name = "", Age = 36, Job = "무직" },
            //};

            ConsoleTable.Write(peoples, people=> {
                if (people.Age > 30)
                    return ConsoleColor.Red;
                else
                    return ConsoleColor.Green;
            });
        }
    }
}
