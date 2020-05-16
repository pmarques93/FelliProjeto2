using System;
namespace Felli
{
    /// <summary>
    /// Class responsible for all renders in the console
    /// </summary>
    public class Renderer
    {
        private Board[,] board;

        private byte boardSize;
        
        public Renderer(Board[,] board, byte boardSize)
        {
            this.board = board;
            this.boardSize = boardSize;
        }

        /// <summary>
        /// Empty Constructor used to create instaces of the class
        /// </summary>
        public Renderer(){}


        /// <summary>
        /// Method that prints the board with all pieces, rows and columns
        /// </summary>
        /// <param name="playerOne">Variable from the Player class that 
        /// represents player One</param>
        /// <param name="playerTwo">Variable from the Player class that 
        /// represents player Two</param>
        /// <param name="playerName">Name of each player's pieces.</param>
        public void RenderBoard(Player[] playerOne, Player[] playerTwo,
                            string playerName)
        {
            Console.Write("\n .----0  1  2  3  4---.");
            Console.Write($"                  Selectable Pieces\n");
            Console.Write(" |                    |                 ");
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
                        Console.Write($".. ");  
                }
                Console.Write($"  {i}");
                
                // Prints playable numbers on middle row
                if (i == 1)
                    PossiblePlays();
                if (i == 2)
                    EatenPieces(playerOne, playerTwo, 1);
                if (i == 3)
                    EatenPieces(playerOne, playerTwo, 2);

                Console.WriteLine("");
            }
            Console.Write(" |                    |");
            Console.Write("    'back' to select another piece");
            Console.Write("\n '----0  1  2  3  4---'");
            Console.Write("          'exit' to leave the game\n");
        }

        /// <summary>
        /// Method responsible for printing all information related to the game 
        /// to the players 
        /// /// </summary>
        /// <param name="message"></param>
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
                        +"                ** PLAYER 1 **");
                    break;
                case "Player2Round":
                    Console.WriteLine("\n --------------------------------" +
                    "-------------------------");
                    Console.Write("                            "
                        +"                ** PLAYER 2 **");
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
        /// Method responsible for printing the name of the selected player
        /// </summary>
        /// <param name="playerName">Name of each player's pieces</param>
        public void RenderPlayer(string playerName)
        {
            Console.WriteLine($" {playerName} selected.\n");
        }

        /// <summary>
        /// Method responsible for printing the possible plays in the current
        /// turn to the active player
        /// </summary>
        private void PossiblePlays()
        {
            Console.Write($"              Playable Numbers: ");
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
        /// Method responsible for printing the removed pieces for each player
        /// </summary>
        /// <param name="playerOne">Variable from the Player class that 
        /// represents player One</param>
        /// <param name="playerTwo">Variable from the Player class that 
        /// represents player Two</param>
        /// <param name="x">Variable that holds the number of either player One
        /// or player Two</param>
        private void EatenPieces(Player[] playerOne, Player[] playerTwo, byte x)
        {
            if (x == 1)
            {
                //Checks all pieces in the playerOne array 
                Console.Write($"       Eaten Player One Pieces: ");
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
                Console.Write($"       Eaten Player Two Pieces: ");
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
        /// Method that prints the rules
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
        /// Method that prints current playable pieces for the active player in 
        /// the current turn
        /// </summary>
        /// <param name="playerOne">Variable from the Player class that 
        /// represents player One</param>
        /// <param name="playerTwo">Variable from the Player class that 
        /// represents player Two</param>
        /// <param name="playerName">Name of each player's pieces.</param>
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
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <param name="player"></param>
        /// <returns></returns>
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