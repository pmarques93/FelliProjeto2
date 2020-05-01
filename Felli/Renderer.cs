using System;
namespace Felli
{
    /// <summary>
    /// Class for rendering
    /// </summary>
    public class Renderer
    {
        private Board[,] board;

        private uint boardSize;
        
        public Renderer(Board[,] board, uint boardSize)
        {
            this.board = board;
            this.boardSize = boardSize;
        }
        public Renderer(){}


        // Renders the board
        public void Render(Player[] playerOne, Player[] playerTwo)
        {
            Console.WriteLine("");
            for (uint i = 0; i < boardSize; i++)
            {
                Console.Write($" ");
                for (uint j = 0; j < boardSize; j++)
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
                            if (player.Position.Row == board[i,j].Position.Row
                            &&  player.Position.Column == 
                                board[i,j].Position.Column)
                            {
                                Console.Write($"{player.Name} ");
                            }
                        }
                        foreach (Player player in playerTwo)    
                        {
                            if (player.Position.Row == board[i,j].Position.Row
                            &&  player.Position.Column == 
                                board[i,j].Position.Column)
                            {
                                Console.Write($"{player.Name} ");
                            }
                        }
                    }     
                    else
                        Console.Write($"   ");  
                }
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
                default:
                    Console.WriteLine("");
                    break;
            }
        }

        public void RenderPlayer(string playerName)
        {
            Console.WriteLine($"{playerName} selected");
        }
    }
}