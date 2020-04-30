namespace Felli
{
    /// <summary>
    /// Class for gameplay board
    /// </summary>
    public class Board
    {
        public Position Position { get; private set; }
        public State IsPlayable { get; private set; }

        public Board(Position position, State isPlayable)
        {
            Position = position;
            IsPlayable = isPlayable;
        }
    }
}