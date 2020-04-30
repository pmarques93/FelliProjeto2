namespace Felli
{
    /// <summary>
    /// Class for gameplay board
    /// </summary>
    public class Board
    {
        public Position position { get; private set; }
        public State IsPlayable { get; private set; }

        public Board(Position position, State isPlayable)
        {
            this.position = position;
            IsPlayable = isPlayable;
        }
    }
}