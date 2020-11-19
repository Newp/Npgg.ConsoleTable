using System;
using System.Collections.Generic;
using System.Linq;
using Npgg;

namespace Example
{
    class Program
    {

        class Item
        {
            public string Name { get; set; }
            public Rarity Rarity { get; set; }
            public string Slot { get; set; }
        }

        enum Rarity
        {
            Normal,
            Magic,
            Unique,
            Legendary,
        }

        static void Main(string[] args)
        {
            var items = new[]
            {
                new Item(){ Name= "Leoric's Crown", Rarity = Rarity.Normal, Slot ="Helm"},
                new Item(){ Name= "Thunderfury", Rarity = Rarity.Unique, Slot ="One Handed Weapon"},
                new Item(){ Name= "할배검 the grandfather", Rarity = Rarity.Legendary, Slot ="Two Handed Weapon"},
                new Item(){ Name= "WINDFORCE", Rarity = Rarity.Magic, Slot ="양손무기"},
            };

            ConsoleTable.Write(items.Select(item => (item.Name, item.Rarity)));
            //or
            ConsoleTable.Write(items.Select(item => new { item.Name, item.Rarity }));

            #region ColorByRarity
            ConsoleTable.Write(items, item => item.Rarity switch
            {
                Rarity.Magic => ConsoleColor.DarkCyan,
                Rarity.Unique => ConsoleColor.DarkMagenta,
                Rarity.Legendary => ConsoleColor.DarkYellow,
                _ => ConsoleColor.White
            });
            #endregion

            ConsoleTable.TableColor = ConsoleColor.Red;
            ConsoleTable.ColumnColor = ConsoleColor.Cyan;
            ConsoleTable.RowColor = ConsoleColor.White;


            var obj = new Item() { Name = "Leoric's Crown", Rarity = Rarity.Normal, Slot = "Helm" };
            ConsoleTable.WriteSingle(obj);


            var dictionary = new Dictionary<string, object>()
            {
                { "dictionary",  "convert"},
                { "to",  "key/value pair"},
                { "length",  3},
            };

            ConsoleTable.Write(dictionary);

            ConsoleTable.Write(dictionary, (kvp)=>
            {
                return ConsoleColor.Red;
            });
        }
    }
}
