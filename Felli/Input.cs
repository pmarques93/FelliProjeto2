using System;
namespace Felli
{
    /// <summary>
    /// Class for user input
    /// </summary>
    public class Input
    {
        public Position EatMovement { get; private set; }
        public Position KilledPiecePos {get; private set;}

        public Position GetPosition()
        {
            Renderer print = new Renderer();
            byte aux1, aux2;
            Position pos;
            print.RenderMessage("InsertRow");
            aux1 = Convert.ToByte(Console.ReadLine());
            print.RenderMessage("InsertColumn");
            aux2 = Convert.ToByte(Console.ReadLine());
            pos = new Position(aux1,aux2);
            return pos;
        }

        public bool GameBoundaries(Position nextPos)
        {
            bool result = true;

            // TRY-CATCH



            return result;
        }

        public bool Movement(Position currentPos, Position nextPos, Board[,] board)
        {
            bool canMove = false;   
            

            if (GameBoundaries(nextPos))
            {
                if (nextPos.IsPlayable)
                {
                    if (OneSquareMovement(currentPos,nextPos))
                    {
                        canMove = true;
                    }
                    
                }
            }


            return canMove;
        }

        public bool Eat(Position currentPos, Position nextPos, Board[,] board)
        {
            bool canMove = false;
            
            if (GameBoundaries(nextPos))
                if (CheckPossibleEat(currentPos, nextPos, board))
                    canMove = true;
                
            return canMove;
        }

        private bool OneSquareMovement(Position currentPos, Position nextPos)
        {
            bool canMove = false;
            bool checkColumn = (
                    nextPos.Column == currentPos.Column + 1 ||
                    nextPos.Column == currentPos.Column - 1 ||
                    nextPos.Column == currentPos.Column);

            if (nextPos.Row == currentPos.Row + 1)
            {
                if (checkColumn)
                    canMove = true; 
            }

            else if (nextPos.Row == currentPos.Row - 1)
            {
                if (checkColumn)
                    canMove = true;
            }
            else
            {
                if (nextPos.Column == currentPos.Column + 1 ||
                    nextPos.Column == currentPos.Column - 1)
                    canMove = true;
            }
            return canMove;
        }

        private bool CheckPossibleEat(Position currentPos, Position nextPos, Board[,] board)
        {
            bool canEat = false;
            if ((Math.Abs(currentPos.Column - nextPos.Column)) <= 2 && 
                (Math.Abs(currentPos.Row - nextPos.Row)) <= 2)
            {
                if ((nextPos.Row - 1) < 0)
                {
                    if ((nextPos.Column - 1) < 0)
                        if (board[Convert.ToByte(nextPos.Row + 1), Convert.ToByte(nextPos.Column + 1)].
                        Position.Occupied)
                        {
                            canEat = true;
                            GetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row + 1), Convert.ToByte(nextPos.Column + 1));
                            
                        }

                    else if (Convert.ToByte(nextPos.Column + 1) > 4)
                        if (board[Convert.ToByte(nextPos.Row + 1), Convert.ToByte(nextPos.Column - 1)].
                        Position.Occupied)
                        {
                            canEat = true;
                            GetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row + 1), Convert.ToByte(nextPos.Column - 1));
                        }
                    else
                    {
                        if (board[Convert.ToByte(nextPos.Row + 1), nextPos.Column].
                        Position.Occupied)
                        {
                            canEat = true;
                            GetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row + 1), nextPos.Column);
                        }
                    }
                }

                else if (Convert.ToByte(nextPos.Row + 1) > 4)
                {
                    if ((nextPos.Column - 1) < 0)
                    {
                        if (board[Convert.ToByte(nextPos.Row - 1), Convert.ToByte(nextPos.Column + 1)].
                        Position.Occupied)
                        {
                            canEat = true;
                            GetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row - 1), Convert.ToByte(nextPos.Column + 1));
                        }
                    }

                    else if (Convert.ToByte(nextPos.Column + 1) > 4)
                    {
                        if (board[Convert.ToByte(nextPos.Row - 1), Convert.ToByte(nextPos.Column - 1)].
                        Position.Occupied)
                        {
                            canEat = true;
                            GetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row - 1), Convert.ToByte(nextPos.Column - 1));
                            
                        }
                    }
                    else
                    {
                        
                        if (board[Convert.ToByte(nextPos.Row - 1), nextPos.Column].
                        Position.Occupied)
                        {
                            canEat = true;
                            GetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row - 1), nextPos.Column);

                        }
                    }
                     
                }
                else
                {
                    if (nextPos.Row > currentPos.Row)
                    {
                        if (nextPos.Column > currentPos.Column)
                        {
                            if (board[Convert.ToByte(nextPos.Row - 1), Convert.ToByte(nextPos.Column - 1)].Position.Occupied)
                            {
                                canEat = true;
                                GetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row - 1), Convert.ToByte(nextPos.Column - 1));
                            }
                        }

                        else if (nextPos.Column < currentPos.Column)
                        {
                            if (board[Convert.ToByte(nextPos.Row - 1), Convert.ToByte(nextPos.Column + 1)].Position.Occupied)
                            {
                                canEat = true;
                                GetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row - 1), Convert.ToByte(nextPos.Column + 1));
                            }
                        }

                        else
                        {
                            if (board[Convert.ToByte(nextPos.Row - 1), nextPos.Column].Position.Occupied)
                            {
                                canEat = true;
                                GetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row - 1), nextPos.Column);
                            }
                        }
                    }


                    // 
                    else if (nextPos.Row < currentPos.Row)
                    {
                        if (nextPos.Column > currentPos.Column)
                        {
                            if (board[Convert.ToByte(nextPos.Row + 1), Convert.ToByte(nextPos.Column - 1)].Position.Occupied)
                            {
                                canEat = true;
                                GetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row + 1), Convert.ToByte(nextPos.Column - 1));
                            }
                        }

                        else if (nextPos.Column < currentPos.Column)
                        {
                            if (board[Convert.ToByte(nextPos.Row + 1), Convert.ToByte(nextPos.Column + 1)].Position.Occupied)
                            {
                                canEat = true;
                                GetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row + 1), Convert.ToByte(nextPos.Column + 1));
                            }
                        }

                        else
                        {
                            if (board[Convert.ToByte(nextPos.Row + 1), nextPos.Column].Position.Occupied)
                            {
                                canEat = true;
                                GetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row + 1), nextPos.Column);
                            }
                        }
                    }
                    else
                    {
                        if (nextPos.Column > currentPos.Column)
                        {
                            if (board[nextPos.Row, Convert.ToByte(nextPos.Column - 1)].Position.Occupied)
                            {
                                canEat = true;
                                GetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(nextPos.Row, Convert.ToByte(nextPos.Column - 1));
                            }
                        }
                        else
                        {
                            if (board[nextPos.Row, Convert.ToByte(nextPos.Column + 1)].Position.Occupied)
                            {
                                canEat = true;
                                GetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(nextPos.Row, Convert.ToByte(nextPos.Column + 1));
                            }
                        }
                    }
                    
                }
                
            }
             
            return canEat;
        }

        private void GetEatMovement(byte row, byte column)
        {
            EatMovement = new Position (row, column);
        }

        private void SetKilledPiecePos(byte row, byte column)
        {
            KilledPiecePos = new Position (row, column);
        }
    }
}