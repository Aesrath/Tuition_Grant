using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01.Models
{
   public class Pokedex
   {
      public int ID { get; private set; }
      public string Name { get; private set; }
      public string Type1 { get; private set; }
      public string Type2 { get; private set; }
      public int Attack { get; private set; }
      public int Defence { get; private set; }
      public int Stamina { get; private set; }

      public Pokedex(int id, string name, 
                      string type1, string type2, 
                      int attack, int defence, int stamina)
      {
         ID = id;
         Name = name;
         Type1 = type1;
         Type2 = type2;
         Attack = attack;
         Defence = defence;
         Stamina = stamina;
      }

   }

}
