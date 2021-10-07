using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Channels;

namespace TextAdventure
{
   public class Hero
   {
       public string Name = "";
       public int Health = 300;
       public List<string> Items = new List<string>();
       public string Location = "newgame";

       public void Attack(Minotaur minotaur)
       {
           Random random = new Random();
           int roll = random.Next(1, 4);

           if (roll > 2)
           {
               if (this.Items.Contains("shiny sword"))
               {
                    minotaur.hp -= 100;
                    Console.WriteLine("You attacked with the shiny sword. ");
                    Console.WriteLine($"The minotaur has {minotaur.hp} left.");
               } else if (this.Items.Contains("wooden sword"))
               {
                   minotaur.hp -= 70;
                   Console.WriteLine($"You dealt 70 damage to the minotaur with your wooden sword, minotaur's health is {minotaur.hp}. ");
               }

               if (Items.Contains("blessed amulet"))
               {
                   this.Health += 10;
                   if (this.Health > 300)
                   {
                       this.Health = 300;
                   }
               }
               else if (Items.Contains("cursed amulet"))
               {
                   this.Health -= 10;
               }
           }
           else
           {
              
               minotaur.hp -= 50;
               Console.WriteLine(
                   $"You dealt 50 damage to the minotaur by punching it in the baaallllllzz. The minotaur has {minotaur.hp} left.");
           }
          
       }
   }
}

