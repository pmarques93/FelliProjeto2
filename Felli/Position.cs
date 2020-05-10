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
        /// <summary>
        /// Constructor used when using the Class Position with the Class
        /// Board
        /// </summary>
        /// <param name="row">Variable that holds the row value
        ///  of the board </param>
        /// <param name="column">Variable that holds the column value
        ///  of the board</param>
        /// <param name="isPlayable">Variable that sets the position has playable
        ///(True) or unplayable (False)</param>
        public Position(byte row, byte column, bool isPlayable)
        {
            Row = row;
            Column = column;
            IsPlayable = isPlayable;
        }        
        /// <summary>
        /// Constructor used when using the Class Position with the Class
        /// Player
        /// </summary>
        /// <param name="row">Variable that holds the row value
        ///  of the player </param>
        /// <param name="column">Variable that holds the column value
        ///  of the player</param>
        public Position(byte row, byte column)
        {
            Row = row;
            Column = column;
        }
        /// <summary>
        /// Method used to change a position from free to occupied
        /// </summary>  
        public void OccupySpace()
        {
            IsPlayable = false;
            Occupied = true;
        }
        /// <summary>
        /// Method used to change a position from occupied to free
        /// </summary>
        public void FreeSpace()
        {
            IsPlayable = true;
            Occupied = false;
        }
    }
}