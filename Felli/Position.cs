namespace Felli
{
    /// <summary>
    /// Class for board position
    /// </summary>
    public class Position
    {
        public uint Row { get; private set; }
        public uint Column { get; private set; }
        public bool IsPlayable { get; set; }

        public Position(uint row, uint column, bool isPlayable)
        {
            Row = row;
            Column = column;
            IsPlayable = isPlayable;
        }
    }
}