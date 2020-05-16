namespace Felli
{
    /// <summary>
    /// Class for player character.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Gets and sets a Position value.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Gets and sets a value indicating if the player is selected.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets and sets a value indicating if the player's piece is alive.
        /// </summary>        
        public bool IsAlive { get; private set; }

        /// <summary>
        /// Gets and sets selected piece's index.
        /// </summary>
        public byte Index { get; private set;}

        /// <summary>
        /// Constructor for the Player Class
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="position">Player's piece position</param>
        /// <param name="isAlive">Specifies whether the piece is still alive
        /// or dead. </param>
        public Player(string name, Position position, bool isAlive)
        {
            Name = name;
            Position = position;
            IsAlive = isAlive;
            Index = GetIndex();
        }

        /// <summary>
        /// Checks which player is currently playing.
        /// </summary>
        public void SelectPlayer()
        {
            Selected = true;
        }

        /// <summary>
        /// Changes the player.
        /// </summary>
        public void DeselectPlayer()
        {
            Selected = false;
        }

        ///<summary>
        /// Removes a piece from the Player's Total
        ///<summary>          
        public void Die()
        {
            IsAlive = false;
        }

        /// <summary>
        /// Calls the chosen piece to be played.
        /// </summary>
        /// <returns>Returns the index of the chosen piece </returns>
        private byte GetIndex()
        {
            //Creation of arrays with each players pieces
            string [] name1 = new string [6]{"W0", "W1","W2", "W3", "W4" ,"W5"};
            string [] name2 = new string [6]{"B0", "B1","B2", "B3", "B4" ,"B5"};
            byte index = 0;

            
            for (byte i = 0; i < 6; i++)
            {
                if (Name == name1[i] || Name == name2[i])
                {
                    index = i;
                    break;
                }
            }

            return index;
        }       
    }
}