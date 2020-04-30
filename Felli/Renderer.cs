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

        // Renders the board
        public void Render()
        {
            Console.WriteLine("");
            for (uint i = 0; i < boardSize; i++)
            {
                Console.Write($" ");
                for (uint j = 0; j < boardSize; j++)
                {
                    // Prints true positions
                    if (board[i,j].IsPlayable.PlayableCheck)
                    {
                        Console.Write($"{board[i,j].Position.Row}");
                        Console.Write($"{board[i,j].Position.Column}");
                    }
                    else
                        Console.Write($"  ");
                }
                Console.WriteLine("");
            }
        }
    }
}