namespace Felli
{
    /// <summary>
    /// Class for board position
    /// </summary>
    public class Position
    {
        public byte Row { get; private set; }

        public byte Column { get; private set; }
    
        public bool IsPlayable { get; set; }

        public bool Occupied { get; private set; }
        

        public Position(byte row, byte column, bool isPlayable)
        {
            Row = row;
            Column = column;
            IsPlayable = isPlayable;
        }        

        public Position(byte row, byte column)
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