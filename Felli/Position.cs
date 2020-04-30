namespace Felli
{
    /// <summary>
    /// Class for board position
    /// </summary>
    public class Position
    {
        public uint Row { get; private set; }

        public uint Column { get; private set; }

        public bool IsPlayable { get; private set; }

        public bool Occupied { get; private set; }
        

        public Position(uint row, uint column, bool isPlayable)
        {
            Row = row;
            Column = column;
            IsPlayable = isPlayable;
        }        

        public Position(uint row, uint column)
        {
            Row = row;
            Column = column;
        }

        public void OccupySpace()
        {
            IsPlayable = false;
            Occupied = true;
        }

        public void FreeSpace()
        {
            IsPlayable = true;
            Occupied = false;
        }
    }
}