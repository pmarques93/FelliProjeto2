namespace Felli
{
    /// <summary>
    /// Class for player character
    /// </summary>
    public class Player
    {
        private string name;

        public Position Position { get; private set; }

        public Player(string name, Position position)
        {
            this.name = name;
            Position = position;
        }

    }
}