using System;
namespace Felli
{
    /// <summary>
    /// Class for rendering
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
        public Renderer(){}


        // Renders the board
        public void RenderBoard(Player[] playerOne, Player[] playerTwo)
        {
            Console.Write("\n     0  1  2  3  4\n\n");

            for (byte i = 0; i < boardSize; i++)
            {
                Console.Write($" {i}  ");
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
                // Prints playable numbers on middle row
                if (i == 1)
                    PossiblePlays();
                if (i == 2)
                    EatenPieces(playerOne, playerTwo);

                Console.WriteLine("");
            }
        }

        public void RenderMessage(string message)
        {
            
            switch (message)
            {
                case "InvalidMove":
                    Console.WriteLine("That is not a valid move");
                    break;
                case "InsertRow":
                    Console.Write("Insert a row number: ");
                    break;
                case "InsertColumn":
                    Console.Write("Insert a column number: ");
                    break;
                case "SelectPiece":
                    Console.Write("\nSelect a piece to play with: ");
                    break;
                case "FirstRound":
                    Console.Write("Who will play first?\n"+
                    "\n{'1' for Player 1 | '2' for Player 2}."+
                    "\n[1 / 2]: ");
                    break;
                case "Player1Round":
                    Console.WriteLine("\n-----------------------"
                    +"-------------- PLAYER 1 --------");
                    break;
                case "Player2Round":
                    Console.WriteLine("\n--------- PLAYER 2 --------------" +
                    "-----------------------");
                    break;
                case "InvalidPiece":
                    Console.WriteLine("\n-------------------- INVALID PIECE" +
                    " ---------------------\n"+
                    "Please, insert a valid piece name.\n");
                    break;
                default:
                    Console.WriteLine("No message defined");
                    break;
            }
        }

        public void RenderPlayer(string playerName)
        {
            Console.WriteLine($"{playerName} selected.\n");
        }

        // Prints possible plays
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

        private void EatenPieces(Player[] playerOne, Player[] playerTwo)
        {
            Console.Write($"          Eaten Player Pieaces: ");
            foreach (Player player in playerOne)
            {
                if (!(player.IsAlive))
                {
                    Console.Write($"{player.Name} ");
                }
            }
            foreach (Player player in playerTwo)
            {
                if (!(player.IsAlive))
                {
                    Console.Write($"{player.Name} ");
                }
            }
        }

        public void PrintRules()
        {
            Console.WriteLine("--------------------- FELLI GAME --------------");
            Console.WriteLine("----------------------- Rules ------------------");
            Console.WriteLine("- 6 White pieces are positioned on the bottom.");
            Console.WriteLine("- 6 Black pieces are positioned on the top.");
            Console.WriteLine("- Each player gets a turn.");
            Console.WriteLine("- On its turn, the player may move one piece," +
            " a house per turn.");
            Console.WriteLine("- Pieces may be moved in every direction if theres" +  
            "an empty space next to it.");
            Console.WriteLine("- Pieces may also move, jumping over the opponent's" +
            ", eliminating it \n  from the game and landing on the house adjacent" +  
            " in that direction");
            Console.WriteLine("- Only a single piece may be eliminated each turn.");
            Console.WriteLine("- The game ends when a player eliminates all" + 
            " the opponents pieces.");
            Console.WriteLine("- White pieces go first.");
         
        }
        // Returns true for equal positions
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