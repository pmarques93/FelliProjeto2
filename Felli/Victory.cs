using System;
namespace Felli
{
    public class Victory
    {
        /// <summary>
        /// Defines victory condition
        /// </summary>
        /// <param name="playerOne"> Parameter with playerOne array</param>
        /// <param name="playerTwo"> Parameter with playerTwo array</param>
        /// <param name="Board"> Parameter with board array</param>
        /// <returns> Returns victory condition if one of the players
        /// runs out of pieces;
        /// Returns victory condition if one of the players runs out 
        /// of possible plays;</returns>
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
        /// Method that checks if the player's pieces are all blocked
        /// </summary>
        /// <param name="p">Variable that holds the position of each player's
        /// piece</param>
        /// <param name="board">Instance of the Board Class that holds all the 
        /// parameters of the current game</param>
        /// <returns></returns>
        private bool CantMove(Position p, Board[,] board)
        {
            bool gameOver = false;
            byte gameOverCount = 0;
            byte necessaryToWin = 8;
    
            sbyte [][] checkList = new sbyte [][]
            {
                new sbyte[2] {(sbyte)(p.Row + 1), (sbyte)(p.Column + 1)}, 
                new sbyte[2] {(sbyte)(p.Row + 1), (sbyte)(p.Column - 1)},
                new sbyte[2] {(sbyte)(p.Row + 1), (sbyte)p.Column},
                new sbyte[2] {(sbyte)(p.Row - 1), (sbyte)(p.Column + 1)},
                new sbyte[2] {(sbyte)(p.Row - 1), (sbyte)(p.Column - 1)},
                new sbyte[2] {(sbyte)(p.Row - 1), (sbyte)p.Column},
                new sbyte[2] {(sbyte)p.Row, (sbyte)(p.Column - 1)},
                new sbyte[2] {(sbyte)p.Row, (sbyte)(p.Column + 1)}
            };
            
            foreach (sbyte[] pos in checkList)
            {
                try
                {
                    if (!(board[Convert.ToByte(pos[0]), Convert.ToByte(pos[1])].
                        Position.IsPlayable))
                        gameOverCount ++;       
                }
                catch (IndexOutOfRangeException)
                {
                    necessaryToWin --;
                    continue;
                }
                catch (OverflowException)
                {
                    necessaryToWin --;
                    continue;
                }   
            }

            if (gameOverCount == necessaryToWin)
                gameOver = true;

            return gameOver;
        }
    }
}