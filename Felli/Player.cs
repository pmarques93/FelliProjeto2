namespace Felli
{
    /// <summary>
    /// Class for player character
    /// </summary>
    public class Player
    {
        public string Name { get; }
        public Position Position { get; set; }

        public bool Selected { get; set; }
        
        public bool IsAlive { get; private set; }

        public byte Index { get; private set;}

        /// <summary>
        /// Constructor for the Player Class
        /// </summary>
        /// <param name="name">Variable that holds the player's name</param>
        /// <param name="position">Variable from the class Position
        /// that holds each player's piece position</param>
        /// <param name="isAlive">Variable that checks if each player's piece
        /// is still in game or dead. </param>
        public Player(string name, Position position, bool isAlive)
        {
            Name = name;
            Position = position;
            IsAlive = isAlive;
            Index = GetIndex();
        }

        /// <summary>
        /// Method used to check which player is currently playing
        /// </summary>
        public void SelectPlayer()
        {
            Selected = true;
        }

        /// <summary>
        /// Method used to change the playing player.
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
        /// Method that calls the chosen piece to be played
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