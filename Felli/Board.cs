namespace Felli
{
    /// <summary>
    /// Class for gameplay board
    /// </summary>
    public class Board
    {
        private readonly int row;
        private readonly int column;
        private readonly bool isPlayable;

        public Board(int row, int column, bool isPlayable)
        {
            this.row = row;
            this.column = column;
            this.isPlayable = isPlayable;
        }

        public int PrintRow() => row;

        public int PrintColumn() => column;

        public bool IsPlayable() => isPlayable;
    }
}