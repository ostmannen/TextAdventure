using System;
using System.Data;
using System.Threading.Channels;


namespace TextAdventure
{
   class Program
   {
       static void Main(string[] args)
       {
          
           Console.WriteLine("Welcome to Text Adventure! ");
           Hero hero = new Hero();
           Minotaur minotaur = new Minotaur();
          
          
           while (hero.Location != "quit")
           {
               Console.WriteLine(hero.Health);
               if (hero.Location == "newgame")
               {
                   NewGame(hero);
               }
               else if (hero.Location == "tableroom")
               {
                   tableroom(hero);
               }
               else if (hero.Location == "corridor")
               {
                    corridor(hero);
               }
               else if (hero.Location == "lockedroom")
               {
                   lockedroom(hero);
               }
               else if (hero.Location == "thirdroom")
               {
                   thirdroom(hero);
               }
               else if (hero.Location == "backoutside")
               {
                   backoutside(hero, minotaur);
               } else if (hero.Location == "gameOver")
               {
                   gameOver(ref hero, ref minotaur);
               }
               else if (hero.Location == "hallway")
               {
                   hallway(hero);
               }
               else if (hero.Location == "Nature")
               {
                   Nature(hero);
               }
               else
               {
                   Console.Error.WriteLine($"you forgot to implement {hero.Location}! ");
               }
           }

           Console.WriteLine("you break ");
       }
       static string Ask(string question)
       {
           string response;
           response = "";
           while (response == "")
           {
               Console.WriteLine(question);
               response = Console.ReadLine().Trim();
              
           } return response;
       }

       static bool AskYesorNo(string question)
       {
           while (true)
           {
               string response = Ask(question).ToLower();
               switch (response)
               {
                   case "yes":
                       case "ok":
                       return true;
                       case "no":
                           return false;
               }
           }
       }

       static void NewGame(Hero hero)
       {
           Console.Clear();
           string name = "";
           do
           {
               name = Ask("what is your name? ");
           } while (!AskYesorNo($"So, {name} is it? "));

           hero.Name = name;
           hero.Location = "tableroom";

       }

       static void tableroom(Hero hero)
       {
           bool pickingUp = true;
           Console.Clear();
           hero.Items.Add("wooden sword");
           Console.WriteLine("You are equipped with one wooden sword, and your task "
                             +"is to slay the monster at the end of the adventure. " + ""
                             +"In front of you is a stone table with two items on it, "
                             +"a knife and a key." +
                             "" +
                             "You can only pick up one of these items.");
           while (pickingUp)
           {
               string input = Ask("Do you want the knife or the key");
               switch (input)
               {
                   case "knife":
                       case "the knife":
                       hero.Items.Add("knife");
                       Console.WriteLine("You pick up the knife");
                       pickingUp = false;
                       break;
                       case "key":
                           case "the key":
                           hero.Items.Add("key");
                           Console.WriteLine("You pick up the key");
                           pickingUp = false;
                           break;
                       case "none":
                           case "nothing":
                           Console.WriteLine("You pick up some air");
                           pickingUp = false;
                           break;
               }

               Console.ReadLine();
               hero.Location = "corridor";
           }
       }
       static void corridor(Hero hero)
       { 
           Console.Clear();
          
           Console.WriteLine("You exit the room and find your self standing in a dark "
                            +"hallway. You can either enter another room on your right "
                            +"side, or continue down the hall way on your left.");
              
           string response = Ask("Do you want to go left or right? ");
           if (response == "left")
           {
               hero.Location = "thirdroom";
           }
           else if (response == "right")
           {
               if (hero.Items.Contains("key"))
               {
                   hero.Location = "lockedroom";
                   hero.Items.Remove("key");
               }
               else
               {
                   Console.WriteLine("Door is locked");
               }
           }

           Console.ReadLine();
       }

       static void lockedroom(Hero hero)
       {
           Console.Clear();
           Console.WriteLine("Inside the lockedroom " + "you find a shiny sword! ");

           if (AskYesorNo("Do you want to swap your sword? "))
           {
               hero.Items.Add("Shiny sword");
               hero.Items.Remove("wooden sword");
           }

           hero.Location = "thirdroom";
       }

       static int RollD6()
       {
           Random random = new Random();
           int roll = random.Next(1, 7);
           return roll;
       }

       static void thirdroom(Hero hero)
       {
           Console.Clear();
           if (AskYesorNo("Do you want to loot the corpse? "))
           {
               if (RollD6() > 4)
               {
                   Console.WriteLine("You have found a blessed amulet. " +
                                     "Everytime you deal damage, you heal. " +
                                     "Good for you. :) ");
                   hero.Items.Add("blessed amulet");
               }
               else
               {
                   Console.WriteLine("You were unlucky. You found the cursed amulet. " +
                                     "Everytime you deal damage, you take damage..." +
                                     "Moo hahahahah");
                   hero.Items.Add("cursed amulet");
               }
           } Console.WriteLine("You leave the corpse and continue into the \n " + "next room.");
           Console.ReadLine();
           hero.Location = "backoutside";
       }

       static void backoutside(Hero hero, Minotaur minotaur)
       {
           Console.Clear();
           Console.WriteLine("A minotaur is standing in front of you");
           Console.ReadLine();
           Console.WriteLine("Minotaur: This is the end of your adventure!!!!!!!! " +
                             "I received a message from the gods to whoop your ass!!!! " +
                             "ARE YOUUU READY, BOIII!!!!!!");
           bool fight = true;
           while (fight)
           {
              
               Attack(hero, minotaur, ref fight);
               Console.ReadLine();
               Console.Clear();
           }
       }

       static void Attack(Hero hero, Minotaur minotaur, ref bool fight)
       {
           if (RollD6() > 4)
           {
               Console.WriteLine("Minotaur attacks");
               minotaur.Attack(hero);
           }
           else
           {
               Console.WriteLine("You attacks");
               hero.Attack(minotaur);
           }
           if (minotaur.hp < 1)
           {
               hero.Location = "hallway";
               Console.WriteLine("Minotaur: ARggghhh!!! You might have defeated me this time." +
                                 "But i will return!!!");
               Console.WriteLine("You entered the main hallway");
               Console.ReadLine();
               fight = false;
           }
           else if (hero.Health < 1)
           {
               hero.Location = "gameOver";
               Console.WriteLine("Minotaur: The gods warned you, you should have gone back to where you came from booooiiii, you time has come to an end.");
               Console.ReadLine();
               Console.WriteLine($"{hero.Name}: I had a mission to save the world from the fake gods that you worship, but I put my people down. Soon I will return for revenge. ");
               fight = false;
           }
       }

       static void gameOver(ref Hero hero, ref Minotaur minotaur)
       {
           hero = new Hero();
           minotaur = new Minotaur();
          
       }

       static void hallway(Hero hero)
       {
           Console.Clear();
           Console.WriteLine("You have now defeated the minotaur and may now be free, enjoy your freedom son!");
           Console.ReadLine();
           Console.WriteLine($"{hero.Name}: haha, I can now go back home to my my family and play some text adventure games." +
                             $" Hmmmmm I like the smell of freedom. ");

           Console.ReadLine();
           hero.Location = "Nature";
       }

       static void Nature(Hero hero)
       {
           Console.Clear();
           Console.WriteLine("Wooow look at the trees, aren't they beautiful??? " +
                             "Hmmm let me think, which way is my home? " +
                             "I think I'm lost, but it's okay, I will now rest under this tree right here and continue my journey tomorrow. ");
           Console.ReadLine();
       }
   }
}
