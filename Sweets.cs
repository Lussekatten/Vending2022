using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending2022
{
    internal class Sweets : Article
    {
        public Sweets(string name, string type, int price) : base(name, type, price)
        {
        }

        public override void Examine()
        {
            Console.WriteLine($"Article name: {Name}, Price: {Price} kr");
        }
        public override void Use()
        {
            Console.WriteLine("Enjoy eating your sweet tasting " + Name);
        }
    }
}
