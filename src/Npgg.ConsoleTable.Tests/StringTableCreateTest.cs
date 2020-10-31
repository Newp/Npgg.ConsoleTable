using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Npgg.ConsoleTableTests
{
    public class StringTableCreateTest
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

        //[Fact]
        //public void LineCountTest()
        //{
        //    var table = ConsoleTable.Create(list);

        //    //4 = top+column+mid+bottom line
        //    Assert.Equal(list.Count + 4, table.Split('\n').Length);
        //}

    }
}
