using System;

namespace Felli
{
    class Program
    {
        // Variables
        static bool gameover = false;

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Game();
        }



        /// <summary>
        /// Creates gameplay board
        /// </summary>
        static private void Game()
        {
            Board[,] board;
            const int boardSize = 5;
            board = new Board[boardSize,boardSize];

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    // Creates true and false positions in game board
                    if (j == 2)
                        board[i,j] = new Board(i,j, true);
                    else if (i == 2)
                        board[i,j] = new Board(i,j, false);
                    else if (i % 2 == 0 && j %2 == 0)
                        board[i,j] = new Board(i,j, true);
                    else if (i % 2 != 0 && j %2 != 0)
                        board[i,j] = new Board(i,j, true);
                    else
                        board[i,j] = new Board(i,j, false);
                }
            }
            PrintBoard(board, boardSize);

            // Gameloop - while not game over
            while (!(gameover))
            {
                gameover = true;
            }
        }



        /// <summary>
        /// Prints the game board
        /// </summary>
        static private void PrintBoard(Board[,] board, int boardSize)
        {
            Console.WriteLine("");
            for (int i = 0; i < boardSize; i++)
            {
                Console.Write($" ");
                for (int j = 0; j < boardSize; j++)
                {
                    // Prints true positions
                    if (board[i,j].IsPlayable())
                    {
                        Console.Write($"{board[i,j].PrintRow()}");
                        Console.Write($"{board[i,j].PrintColumn()}");
                    }
                    else
                        Console.Write($"  ");
                }
                Console.WriteLine("");
            }
        }
    }
}
