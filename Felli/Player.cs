using System;
namespace Felli
{
    /// <summary>
    /// Class for player character
    /// </summary>
    public class Player
    {
        public string Name { get; }

        public Position Position { get; set; }

        public bool Selected { get; set; }

        public bool IsAlive { get; private set; }

        public Player(string name, Position position, bool isAlive)
        {
            Name = name;
            Position = position;
            IsAlive = isAlive;
        }

        public Position GetPosition()
        {
        
            uint aux1, aux2;
            Position pos;
            Console.Write("Insert a row number: ");
            aux1 = Convert.ToUInt32(Console.ReadLine());

            Console.Write("Insert a row number: ");
            aux2 = Convert.ToUInt32(Console.ReadLine());
            pos = new Position(aux1,aux2);
            return pos;
        }

        public void SelectPlayer()
        {
            Selected = true;
        }

        public void DeselectPlayer()
        {
            Selected = false;
        }

        // Stops playing from being printed and chooseable
        public void Die()
        {
            IsAlive = false;
        }
    }
}