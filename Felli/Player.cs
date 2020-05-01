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
        private Renderer print = new Renderer();
        public bool IsAlive { get; private set; }

        public Player(string name, Position position, bool isAlive)
        {
            Name = name;
            Position = position;
            IsAlive = isAlive;
        }

        public Position GetPosition()
        {
            byte aux1, aux2;
            Position pos;
            print.RenderMessage("InsertRow");
            aux1 = Convert.ToByte(Console.ReadLine());
            print.RenderMessage("InsertColumn");
            aux2 = Convert.ToByte(Console.ReadLine());
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