using System;
namespace Felli
{
    public class Rules
    {
        public void PrintRules()
        {
            Console.WriteLine("--------------------- FELLI GAME --------------");
            Console.WriteLine("----------------------- Rules ------------------");
            Console.WriteLine("- 6 White pieces are positioned on the bottom.");
            Console.WriteLine("- 6 Black pieces are positioned on the top.");
            Console.WriteLine("- Each player gets a turn.");
            Console.WriteLine("- On its turn, the player may move one piece," +
            " a house per turn.");
            Console.WriteLine("- Pieces may be moved in every direction if theres" +  
            "an empty space next to it.");
            Console.WriteLine("- Pieces may also move, jumping over the opponent's" +
            ", eliminating it \n  from the game and landing on the house adjacent" +  
            " in that direction");
            Console.WriteLine("- Only a single piece may be eliminated each turn.");
            Console.WriteLine("- The game ends when a player eliminates all" + 
            " the opponents pieces.");
            Console.WriteLine("- White pieces go first.");
            Console.WriteLine("----------------------------------------------");
        }
    }
}