using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Windows.Markup;

namespace Npgg
{

    class Program
    {

        static void Main(string[] args)
        {
            List<Sample> list = new List<Sample>()
            {
                new Sample(){ Name="john", Value= "11230981fj191912", Age = 25},
                new Sample(){ Name="custom long name", Value= "11230981fjfdslkjdfsljfdsfldfdfafa", Age = 2500},
                new Sample(){ Name="miku", Value= "3939", Age = 10}      
            };

            Console.WriteLine(ConsoleTable.Create(list.Select( item => new { item.Name, item.Age })));
            Console.WriteLine(ConsoleTable.Create(list));

        }
        
    }

    class Sample
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int Age { get; set; }
    }
}
