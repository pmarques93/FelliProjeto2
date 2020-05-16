using System;
namespace Felli
{
    public class Victory
    {
        /// <summary>
        /// Defines victory condition.
        /// </summary>
        /// <param name="playerOne"> PlayerOne array.</param>
        /// <param name="playerTwo"> PlayerTwo array.</param>
        /// <param name="Board"> Array with all board positions.</param>
        /// <returns> Returns TRUE if a player won, otherwise FALSE.</returns>
        public bool Gameover(Player[] playerOne, Player[] playerTwo,
                            Board[,] board)
        {
            Input input = new Input();
            Renderer print = new Renderer();
            bool gameover = false;
            byte piecesLeftP1 = 0;
            byte piecesLeftP2 = 0;
            byte gameOverCountP1 = 0;
            byte gameOverCountP2 = 0;
            
            // Checks if there are plays left for each player
            foreach (Player p1 in playerOne)
            {
                if (p1.IsAlive)
                {
                    piecesLeftP1++;
                    if (CantMove(p1.Position, board))
                    {
                        gameOverCountP1++;
                    }
                }
            }
            if (gameOverCountP1 == piecesLeftP1)
            {
                print.RenderMessage("NoPlaysP1");
                gameover = true;
            }

            foreach (Player p2 in playerTwo)
            {
                if (p2.IsAlive)
                {
                    piecesLeftP2++;
                    if (CantMove(p2.Position, board))
                    { 
                        gameOverCountP2++;
                    }
                }
            }
            if (gameOverCountP2 == piecesLeftP2)
            {
                print.RenderMessage("NoPlaysP2");
                gameover = true;
            }

            return gameover;
        }
        
        /// <summary>
        /// Checks if the player's pieces are all blocked.
        /// </summary>
        /// <param name="p">Position of each player's piece.</param>
        /// <param name="board">Array with all  board positions.</param>
        /// <returns>Returns TRUE if the piece cant move, otherwise FALSE.
        /// </returns>
        private bool CantMove(Position p, Board[,] board)
        {
            Input input = new Input();
            bool gameOver = false;
            byte gameOverCount = 0;
            byte necessaryToWin = 8;

            sbyte [][] checkList = new sbyte [][]
            {
                new sbyte[2] {1, 1},
                new sbyte[2] {1, -1},
                new sbyte[2] {1, 0},
                new sbyte[2] {-1, 1},
                new sbyte[2] {-1, -1},
                new sbyte[2] {-1, 0},
                new sbyte[2] {0, -1},
                new sbyte[2] {0, 1}
            };
            
            foreach (sbyte[] pos in checkList)
            {
                try
                {
                    if (!(board[Convert.ToByte(p.Row + pos[0]), 
                    Convert.ToByte(p.Column + pos[1])].Position.IsPlayable))
                    {
                        gameOverCount++;    
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    necessaryToWin --;
                }
                catch (OverflowException)
                {
                    necessaryToWin --;
                }   

                try
                {
                    if (input.Eat(p, board[Convert.ToByte(pos[0]*2), 
                    Convert.ToByte(pos[1]*2)].Position, board))
                    {
                        gameOver = false;
                        gameOverCount = 0;
                        break;
                    } 
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
                catch (OverflowException)
                {
                    continue;
                }
            }

            if (gameOverCount == necessaryToWin)
                gameOver = true;

            return gameOver;
        }
    }
}