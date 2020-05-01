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
            Console.WriteLine("");
            for (byte i = 0; i < boardSize; i++)
            {
                Console.Write($" ");
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
                        Console.Write($"   ");  
                }
                // Prints playable numbers on middle row
                if (i == 2)
                    PossiblePlays();

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
                default:
                    Console.WriteLine("No message defined");
                    break;
            }
        }

        public void RenderPlayer(string playerName)
        {
            Console.WriteLine($"{playerName} selected.\n");
        // Prints possible plays
        }
        public void PossiblePlays()
        {
            Console.Write($"              Playable Numbers: ");
            foreach (Board position in board)
            {
                if (position.Position.IsPlayable)
                {
                    Console.Write($"{position.Position.Row}");
                    Console.Write($"{position.Position.Column}");
                }
            }
        }

        // Returns true for equal positions
        public bool ComparePosition(Board board, Player player) 
        {
            bool x = false;
            if (board.Position.Row == player.Position.Row &&
                board.Position.Column == player.Position.Column)
                x = true;

            return x;
        }
    }
}