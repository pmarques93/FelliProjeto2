namespace Felli
{
    /// <summary>
    /// Class for player character
    /// </summary>
    public class Player
    {
        public string Name { get; }

        public Position Position { get; private set; }

        public Player(string name, Position position)
        {
            Name = name;
            Position = position;
        }
    }
}