namespace Felli
{
    /// <summary>
    /// Class for gameplay board
    /// </summary>
    public class Board
    {
    
        /// <summary>Gets and sets a Position value.</summary>
        public Position Position { get; private set; }
        
        /// <summary>
        /// Constructor used to create instace of the class
        /// </summary>
        /// <param name="position"> Instace of the class possition</param>
        public Board(Position position)
        {
            Position = position;
        }
    }
}
