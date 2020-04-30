namespace Felli
{
    /// <summary>
    /// Class for board position
    /// </summary>
    public class Position
    {
        public uint Row { get; private set; }
        public uint Column { get; private set; }
        public State IsPlayable { get; private set; }

        public Position(uint row, uint column, State isPlayable)
        {
            Row = row;
            Column = column;
            IsPlayable = IsPlayable;
        }

    }
}