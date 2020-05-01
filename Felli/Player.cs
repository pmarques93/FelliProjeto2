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


        public byte Index { get; private set;}
        public Player(string name, Position position, bool isAlive)
        {
            Name = name;
            Position = position;
            IsAlive = isAlive;
            Index = GetIndex();
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

        private byte GetIndex()
        {

            string [] name1 = new string [6]{"W0", "W1","W2", "W3", "W4" ,"W5"};
            string [] name2 = new string [6]{"B0", "B1","B2", "B3", "B4" ,"B5"};
            byte index = 0;

            for (byte i = 0; i < 6; i++)
            {
                if (Name == name1[i] || Name == name2[i])
                {
                    // Console.WriteLine($"Name: {Name}");
                    // Console.WriteLine($"i: {i}");
                    index = i;
                    break;
                }
            }

            return index;
        }       
    }
}