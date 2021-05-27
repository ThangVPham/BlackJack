using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Suit { get; set; }

        public Card(string name, int value, string suit)
        {
            if(Int32.Parse(name) == 11)
            {
                Name = "J";
            }
            else if (Int32.Parse(name) == 12)
            {
                Name = "Q";
            }
            else if (Int32.Parse(name) == 13)
            {
                Name = "K";
            }
            else if(Int32.Parse(name) == 1)
            {
                Name = "Ace";
            }
            else
            {
                Name = name;
            }
            
            if (value > 10)
            {
                Value = 10;
            }
            else
            {
                Value = value;
            }
                                  
            Suit = suit;
        }

        public override string ToString()
        {
            return ($"{Name} of {Suit}");
        }

        public static Card Parse(string objectData)
        {
            string[] parts = objectData.Split('\t');

            Card a;

            if (parts.Length != 3)
            {
                throw new Exception("Only Accept Value and Suit");
            }
            else
            {            
                a = new Card(parts[0], Int32.Parse(parts[1]), parts[2]);
            }
          
            return a;
        }
    }
}
