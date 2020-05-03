using System;

namespace Felli
{
    public class Victory
    {
        public Board[,] Board {get; set;}
        public Victory (Board[,] board)
        {
            Board = board;
        }
        
        public bool WinChecker(Position currentPos, Board[,] board, Player[] SelectedPlayer)
        {
            bool playerWon = false;
            bool canMoveLeft  = true;
            bool canMoveRight = true;
            bool canMoveDown = true;
            bool canMoveDownLeft = true;
            bool canMoveDownRight = true;
            bool canMoveUp = true;
            bool canMoveUpRight = true;
            bool canMoveUpLeft = true;
            int deadPiece = 0;
            foreach(Player piece in SelectedPlayer)
            {

                if ((currentPos.Row - 1) < 0)
                    {
                        if ((currentPos.Column - 1) < 0)
                        {
                            if (board[Convert.ToByte(currentPos.Row + 1), Convert.ToByte(currentPos.Column + 1)].
                            Position.Occupied)
                            {
                                canMoveUpRight = false;
                            }
                        }
                        else if (Convert.ToByte(currentPos.Column + 1) > 4)
                        {
                            if (board[Convert.ToByte(currentPos.Row + 1), Convert.ToByte(currentPos.Column - 1)].
                            Position.Occupied)
                            {
                                canMoveUpLeft = false;
                            }
                        }
                    }
                else if (Convert.ToByte(currentPos.Row + 1) > 4)
                {
                    if ((currentPos.Column - 1) < 0)
                    {
                        if (board[Convert.ToByte(currentPos.Row - 1), Convert.ToByte(currentPos.Column + 1)].
                        Position.Occupied)
                        {
                            canMoveDownRight = false;                           
                        }
                    }

                    else if (Convert.ToByte(currentPos.Column + 1) > 4)
                    {
                        if (board[Convert.ToByte(currentPos.Row - 1), Convert.ToByte(currentPos.Column - 1)].
                        Position.Occupied)
                        {
                            canMoveDownLeft = false;
                        }
                    }
                    else
                    {
                        if (board[Convert.ToByte(currentPos.Row - 1), currentPos.Column].
                        Position.Occupied)
                        {
                            canMoveDown = false;
                        }
                    }
                        
                }
                else
                {
                    if (board[Convert.ToByte(currentPos.Row - 1), Convert.ToByte(currentPos.Column - 1)].Position.Occupied)
                    {
                        canMoveDownLeft = false;
                    }

                    if (board[Convert.ToByte(currentPos.Row - 1), Convert.ToByte(currentPos.Column + 1)].Position.Occupied)
                    {
                        canMoveDownRight = false;
                    }
                    if (board[Convert.ToByte(currentPos.Row - 1), currentPos.Column].Position.Occupied)
                    {
                        canMoveLeft = false;
                    }
                    
                    if (board[Convert.ToByte(currentPos.Row + 1), Convert.ToByte(currentPos.Column - 1)].Position.Occupied)
                    {
                        canMoveUpLeft = false;
                    }

                    if (board[Convert.ToByte(currentPos.Row + 1), Convert.ToByte(currentPos.Column + 1)].Position.Occupied)
                    {
                        canMoveUpRight = false;
                    }

                    if (board[Convert.ToByte(currentPos.Row + 1), currentPos.Column].Position.Occupied)
                    {
                        canMoveUp = false;
                    }

                    if (board[currentPos.Row, Convert.ToByte(currentPos.Column - 1)].Position.Occupied)
                    {
                        canMoveLeft = false;
                    }

                    if (board[currentPos.Row, Convert.ToByte(currentPos.Column + 1)].Position.Occupied)
                    {
                        canMoveRight = false;
                    }
                }
                
                if( canMoveDown == false && canMoveUp == false && canMoveUpLeft == false
                && canMoveUpRight == false && canMoveDownLeft == false && 
                canMoveDownRight == false && canMoveLeft == false && canMoveRight == false)
                {
                    playerWon = true; 
                }
                
                else if(piece.IsAlive == true)
                {
                   
                    continue;
                }
                else
                {
                    deadPiece++;
                    if (deadPiece == 6)
                    {
                        playerWon = true;
                    }
                }
            }

            return playerWon;
        }
    }
}