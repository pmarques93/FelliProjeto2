namespace Felli
{
    /// <summary>
    /// Class for gameplay board
    /// </summary>
    public class Board
    {
        public Position Position { get; private set; }
        public Board(Position position)
        {
            Position = position;
        }
    }
}