using System;
namespace Felli
{
    /// <summary>
    /// Class responsible for all renders in the console.
    /// </summary>
    public class Renderer
    {
        /// <summary>
        /// Gets or sets a Board value.
        /// </summary>
        private Board[,] board;

        /// <summary>
        ///Sets the boardsize value.
        /// </summary>
        private byte boardSize;
        
        /// <summary>
        /// Constructor for the Renderer class.
        /// </summary>
        /// <param name="board">Array with all board positions.</param>
        /// <param name="boardSize">Size of the board.</param>
        public Renderer(Board[,] board, byte boardSize)
        {
            this.board = board;
            this.boardSize = boardSize;
        }

        /// <summary>
        /// Empty Constructor used to create instaces of the class.
        /// </summary>
        public Renderer(){}


        /// <summary>
        /// Prints the board with all pieces, rows and columns.
        /// </summary>
        /// <param name="playerOne">Player one piece's position and name.</param>
        /// <param name="playerTwo">Player two piece's position and name.</param>
        /// <param name="playerName">Name of each player's pieces.</param>
        public void RenderBoard(Player[] playerOne, Player[] playerTwo,
                            string playerName)
        {
            Console.Write("\n   _ 0_ 1_ 2_ 3_ 4_ _  ");
            Console.Write($"                   Selectable Pieces\n");
            Console.Write(" |                    |                  ");
            PossiblePick(playerOne, playerTwo, playerName);
            Console.WriteLine("");
            
            //Prints the board with all pieces, and playable houses
            //according to the boardSize value
            for (byte i = 0; i < boardSize; i++)
            {
                Console.Write($" {i}   ");
                for (byte j = 0; j < boardSize; j++)
                {
                    // Prints playable positions
                    if (board[i,j].Position.IsPlayable)
                    {
                        Console.Write($"{board[i,j].Position.Row}");
                        Console.Write($"{board[i,j].Position.Column} ");
                    }

                    // Prints occupied cells with player 1 / 2
                    else if (board[i,j].Position.Occupied)
                    {
                        foreach (Player player in playerOne)
                        {
                            if (player.IsAlive)
                                if (ComparePosition(board[i,j], player))
                                    Console.Write($"{player.Name} ");
                        }
                        foreach (Player player in playerTwo)    
                        {
                            if (player.IsAlive)
                                if (ComparePosition(board[i,j], player))           
                                    Console.Write($"{player.Name} ");
                        }
                    } 
                    else
                    {
                        Console.Write($"   ");
                    }
                }
                Console.Write($"  {i}");
                if(i == 0)
                    Console.Write("\n"+@" |     \   |    /     |");
                else if (i == 1)
                {
                    PossiblePlays();
                    Console.Write("\n"+@" |       \ |  /       |");
                }
                else if (i == 2)
                {
                    EatenPieces(playerOne, playerTwo, 2);
                    Console.Write("\n"+@" |       / |  \       |");
                }
                else if (i == 3)
                    Console.Write("\n"+@" |     /   |    \     |");
                
                // Prints playable numbers on middle row
                // if (i == 0)
                //     PossiblePlays();
                if (i == 1)
                    EatenPieces(playerOne, playerTwo, 1);                    

                Console.WriteLine("");
            }
            Console.Write(" | _  _  _  _  _  _ _ |");
            Console.WriteLine("     'back' to select another piece");
            Console.Write("     0  1  2  3  4     ");
            Console.WriteLine("           'exit' to leave the game");
        }

        /// <summary>
        /// Prints all information related to the game to the players.
        /// </summary>
        /// <param name="message">Player's Input.</param>
        public void RenderMessage(string message)
        {
            
            switch (message)
            {
                case "InvalidMove":
                    Console.WriteLine("\n -------------------- INVALID MOVE" +
                    " ---------------------\n"+
                    " That is not a valid move!\n Please, try again.");
                    break;
                case "InsertRow":
                    Console.Write(" Insert a row number: ");
                    break;
                case "InsertColumn":
                    Console.Write(" Insert a column number: ");
                    break;
                case "SelectPiece":
                    Console.Write("\n Select a piece to play with: ");
                    break;
                case "FirstRound":
                    Console.Write("\n Player 1 plays with White Pieces ||" +
                    " Player 2 plays with Black Pieces. \n Which player" +
                    " will play first?" +
                    "\n '1' for White Pieces || '2' Black Pieces\n" + 
                    "\n Pick first player: ");
                    break;
                case "Player1Round":
                    Console.WriteLine("\n -----------------------------" +
                    "----------------------------");
                    Console.Write("                            "
                        +"                 ** PLAYER 1 **\n");
                    break;
                case "Player2Round":
                    Console.WriteLine("\n --------------------------------" +
                    "-------------------------");
                    Console.Write("                            "
                        +"                 ** PLAYER 2 **\n");
                    break;
                case "InvalidPiece":
                    Console.WriteLine("\n -------------------- INVALID PIECE" +
                    " ---------------------\n"+
                    " Please, insert a valid piece name.\n");
                    break;
                case "MovementString":
                    Console.WriteLine("\n -------------------- INVALID INPUT" +
                    " ---------------------\n"+
                    " Please, insert a number, not a string.\n");
                    break;
                case "MovementTooBig":
                    Console.WriteLine("\n -------------------- INVALID INPUT" +
                    " ---------------------\n"+
                    " Please, insert a valid number.\n");
                    break;
                case "NoPlaysP1":
                    Console.WriteLine("\n -----------------------------" +
                    "-------------------------");
                    Console.WriteLine("\n"+
                    " P2 is the Winner !! Goodbye!\n");
                    break;
                case "NoPlaysP2":
                    Console.WriteLine("\n -----------------------------" +
                    "-------------------------");
                    Console.WriteLine("\n"+
                    " P1 is the Winner !! Goodbye!\n");
                    break;
                default:
                    Console.WriteLine(" No message defined");
                    break;
            }
        }

        /// <summary>
        /// Prints the name of the selected player.
        /// </summary>
        /// <param name="playerName">Name of selected player.</param>
        public void RenderPlayer(string playerName)
        {
            Console.WriteLine($" {playerName} selected.\n");
        }

        /// <summary>
        /// Prints the possible plays in the current turn to the active player.
        /// </summary>
        private void PossiblePlays()
        {
            Console.Write($"                   Playable Numbers: ");
            foreach (Board position in board)
            {
                if (position.Position.IsPlayable)
                {
                    Console.Write($"{position.Position.Row}");
                    Console.Write($"{position.Position.Column} ");
                }
            }
        }

        /// <summary>
        /// Prints the removed pieces for each player.
        /// </summary>
        /// <param name="playerOne">Player one piece's position and name.</param>
        /// <param name="playerTwo">Player two piece's position and name.</param>
        /// <param name="x">Player's number.</param>
        private void EatenPieces(Player[] playerOne, Player[] playerTwo, byte x)
        {
            if (x == 1)
            {
                //Checks all pieces in the playerOne array 
                Console.Write($"            Eaten Player One Pieces: ");
                foreach (Player player in playerOne)
                {
                    //If the piece is eliminated prints it on the board
                    if (!(player.IsAlive))
                    {
                        Console.Write($"{player.Name} ");
                    }
                }
            }
            else
            {
                //Checks all pieces in the playerTwo array 
                Console.Write($"            Eaten Player Two Pieces: ");
                foreach (Player player in playerTwo)
                {
                    //If the piece is eliminated prints it on the board
                    if (!(player.IsAlive))  
                    {
                        Console.Write($"{player.Name} ");
                    }
                }
            }
        }

        /// <summary>
        /// Method that prints the rules.
        /// </summary>
        public void PrintRules()
        {
            Console.WriteLine(" --------------------- FELLI GAME -----------"+
            "---");
            Console.WriteLine(" ----------------------- Rules -------------"+
            "-----");
            Console.WriteLine(" - 6 White pieces are positioned at the" + 
            " bottom.");
            Console.WriteLine(" - 6 Black pieces are positioned at the top.");
            Console.WriteLine(" - Each player gets a turn.");
            Console.WriteLine(" - On each player's turn, only one piece" +
            " must be moved at a time, by \n   inserting its name and then" +
            " the desired row and column.");
            Console.WriteLine(" - Pieces may be moved in every direction if " +  
            "there's an empty/playable\n   space next to it.");
            Console.WriteLine(@" - Pieces can also 'eat' other enemy pieces, " +
            " and, therefore, eliminate\n   them, jumping over them and " +
            "landing on the house on the back of that\n   same piece.");
            Console.WriteLine(" - Only a single piece may be eliminated " +
            "each turn.");
            Console.WriteLine(" - The game ends when a player eliminates" + 
            " all the opponent pieces, or\n   the opponent has no possible" + 
            " plays.");
        }   

        /// <summary>
        /// Prints current playable pieces for the active player in the current 
        /// turn.</summary>
        /// <param name="playerOne">Player one piece's position and name.</param>
        /// <param name="playerTwo">Player two piece's position and name.</param>
        /// <param name="playerName">Player's name.</param>
        private void PossiblePick(Player[] playerOne, Player[] playerTwo, 
                                string playerName)
        {
            
            if (playerName == "p1")
            {   
                //Checks all pieces in the playerOne array 
                foreach (Player player in playerOne)
                {
                    //If the piece is in-game prints it on the board
                    if (player.IsAlive)
                    {
                        Console.Write($"{player.Name}|");
                    }
                }
            }
            else
            {
                //Checks all pieces in the playerTwo array 
                foreach (Player player in playerTwo)
                {
                    //If the piece is in-game prints it on the board
                    if (player.IsAlive)
                    {
                        Console.Write($"{player.Name}|");
                    }
                }
            }
        }


        /// <summary>
        /// Compares a board position with a player's piece position.
        /// </summary>
        /// <param name="board">One single board position.</param>
        /// <param name="player">Selected player's piece's position.</param>
        /// <returns>returns true if the positions are equal, otherwise
        /// false.</returns>
        private bool ComparePosition(Board board, Player player) 
        {
            bool x = false;
            if (board.Position.Row == player.Position.Row &&
                board.Position.Column == player.Position.Column)
                x = true;

            return x;
        }
    }
}