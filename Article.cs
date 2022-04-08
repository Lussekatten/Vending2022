using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending2022
{
    internal abstract class Article
    {
        public Article(string name, string type, int price)
        {
            Name = name;
            Type = type;
            Price = price;
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }

        //We could introduce an Id so we can uniquely identify the purchased items
        public int Id { get; set; }

        public abstract void Examine();
        public abstract void Use();

    }
}
