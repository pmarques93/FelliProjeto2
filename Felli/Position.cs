namespace Felli
{
    /// <summary>
    /// Class for board position
    /// </summary>
    public class Position
    {
        public uint Row { get; private set; }
        public uint Column { get; private set; }

        public Position(uint row, uint column)
        {
            Row = row;
            Column = column;
        }

    }
}