using System;

namespace TextAdventure
{
   public class Minotaur
   {
       public int hp = 700;

       public void Attack(Hero hero)
       {
           Random random = new Random();
           int rng = random.Next(1, 4);
           if (rng == 1)
           {
               hero.Health -= 40;
               Console.WriteLine("The Minotaur kicks you in the ballz...");
               Console.WriteLine($"you took 40 damage, you have {hero.Health} left");
              
           }
           else
           {
               hero.Health -= 25;
               Console.WriteLine("The Minotaur spits on you");
               Console.WriteLine($"You took 25 damage, you have {hero.Health} left");
           }
       }
          
   }
}
