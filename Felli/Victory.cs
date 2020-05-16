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
            

            bool gameOver = false;
            byte gameOverCount = 0;
            // Borders
            if (p.Row == 0 && p.Column == 0)
                if ((board[p.Row+1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row+2,p.Column+2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+4].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 0 && p.Column == 2)
                if ((board[p.Row+1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row+2,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-2].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 0 && p.Column == 4)
                if ((board[p.Row+1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row+2,p.Column-2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-4].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 4 && p.Column == 0)
                if ((board[p.Row-1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row-2,p.Column+2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+4].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 4 && p.Column == 2)
                if ((board[p.Row-1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row-1,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row-1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row-2,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-2].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 4 && p.Column == 4)
                if ((board[p.Row-1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row-2,p.Column-2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-2].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-4].Position.IsPlayable == false))
                    gameOverCount ++;


            // Borders - 1
            if (p.Row == 1 && p.Column == 1)
                if ((board[p.Row-1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row-1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+2].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row+2,p.Column+2].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 1 && p.Column == 3)
                if ((board[p.Row-1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row-1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-2].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row+2,p.Column-2].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 3 && p.Column == 3)
                if ((board[p.Row+1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-2].Position.IsPlayable == false) &&
                    (board[p.Row-1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row-2,p.Column-2].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 3 && p.Column == 1)
                if ((board[p.Row+1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+2].Position.IsPlayable == false) &&
                    (board[p.Row-1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row-2,p.Column+2].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 1 && p.Column == 2)
                if ((board[p.Row-1,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row+2,p.Column].Position.IsPlayable == false))
                    gameOverCount ++;

            if (p.Row == 3 && p.Column == 2)
                if ((board[p.Row+1,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row-1,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row-2,p.Column].Position.IsPlayable == false))
                    gameOverCount ++;

            // Middle
            if (p.Row == 2 && p.Column == 2)
                if ((board[p.Row-1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row-2,p.Column-2].Position.IsPlayable == false) &&
                    (board[p.Row-1,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row-2,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row-1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row-2,p.Column+2].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column-1].Position.IsPlayable == false) &&
                    (board[p.Row+2,p.Column-2].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row+2,p.Column].Position.IsPlayable == false) &&
                    (board[p.Row+1,p.Column+1].Position.IsPlayable == false) &&
                    (board[p.Row+2,p.Column+2].Position.IsPlayable == false))
                    gameOverCount ++;


            if (gameOverCount > 0)
                gameOver = true;

            return gameOver;
        }
    }
}