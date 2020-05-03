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
        public bool QuitInput {get; private set; }
        
        public Position GetPosition()
        {
            Renderer print = new Renderer();
            byte row = 0;
            byte column = 0;
            bool validInput = false;
            string rowString, columnString;
            Position pos;
            while (!(validInput))
            {
                print.RenderMessage("InsertRow");
                rowString = Console.ReadLine();
                if (rowString.ToLower() == "exit")
                {
                    validInput = true;
                    QuitInput = true;
                }
                else
                {
                    QuitInput = false;
                    print.RenderMessage("InsertColumn");
                    columnString = Console.ReadLine();
                    if (columnString.ToLower() == "exit")
                    {
                        validInput = true;
                        QuitInput = true;
                    }
                    else
                    {
                        if (CheckConvert(rowString) && CheckConvert(columnString))
                        {
                            row = Convert.ToByte(rowString);
                            column = Convert.ToByte(columnString);
                            pos = new Position(row, column);
                            if (GameBoundaries(pos))
                            {
                                validInput = true;
                            }
                            
                        }
                        QuitInput = false;
                    }
                }

            }

            pos = new Position(row, column);
            return pos;
        }

        public string GetPiece()
        {
            string pieceChoice;
            Renderer print = new Renderer();
            print.RenderMessage("SelectPiece");
            pieceChoice = Console.ReadLine().ToUpper();

            if (pieceChoice == "EXIT")
            {
                QuitInput = true;
            }
            else
            {
                QuitInput = false;
            }

            return pieceChoice;
        }

        public bool CheckConvert(string inputString)
        {
            Renderer print = new Renderer();
            bool validInput = false;
            byte aux;
            try 
            {
                aux = Convert.ToByte(inputString);
                validInput = true;
            }
            
            catch (FormatException)
            {
                print.RenderMessage("MovementString");
            }
            catch (OverflowException)
            {
                print.RenderMessage("MovementTooBig");
            }

            return validInput;

        }
        

        public bool GameBoundaries(Position nextPos)
        {
            bool result = false;

            byte nextRow = nextPos.Row;
            byte nextColumn = nextPos.Column;

            if (nextRow >= 0 && nextRow <= 4 &&
                nextColumn >= 0 && nextColumn <= 4)
            {
                result = true;
            }

            return result;
        }

        public bool Movement(Position currentPos, Position nextPos, 
                        Board[,] board)
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
            {
                if (CheckPossibleEat(currentPos, nextPos, board))
                {
                    canMove = true;
                }
            } 
                
            return canMove;
        }

        private bool OneSquareMovement(Position currentPos, Position nextPos)
        {
            bool canMove = false;
            bool checkColumn = (nextPos.Column == currentPos.Column + 1 ||
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

        private bool CheckPossibleEat(Position currentPos, Position nextPos, 
                                    Board[,] board)
        {
            bool canEat = false;

            // Checks if the player is trying to make a 2 cells move
            if ((Math.Abs(currentPos.Column - nextPos.Column)) <= 2 &&
                (Math.Abs(currentPos.Row - nextPos.Row)) <= 2)
            {
                if ((nextPos.Row - 1) < 0)
                {
                    if ((nextPos.Column - 1) < 0)
                    {
                        if (board[Convert.ToByte(nextPos.Row + 1), Convert.
                            ToByte(nextPos.Column + 1)].Position.Occupied)
                        {
                            canEat = true;
                            SetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row + 1),
                                            Convert.ToByte(nextPos.Column + 1));
                            
                        }
                    }
                    else if (Convert.ToByte(nextPos.Column + 1) > 4)
                    {
                        if (board[Convert.ToByte(nextPos.Row + 1), Convert.
                            ToByte(nextPos.Column - 1)].
                        Position.Occupied)
                        {
                            canEat = true;
                            SetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row + 1),
                                            Convert.ToByte(nextPos.Column - 1));
                        }
                    }
                    else
                    {
                        if (board[Convert.ToByte(nextPos.Row + 1), nextPos.
                            Column].Position.Occupied)
                        {
                            canEat = true;
                            SetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row + 1),
                                            nextPos.Column);
                        }
                    }
                }

                else if (Convert.ToByte(nextPos.Row + 1) > 4)
                {
                    if ((nextPos.Column - 1) < 0)
                    {
                        if (board[Convert.ToByte(nextPos.Row - 1), Convert.
                            ToByte(nextPos.Column + 1)].Position.Occupied)
                        {
                            canEat = true;
                            SetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row - 1),
                                            Convert.ToByte(nextPos.Column + 1));
                        }
                    }

                    else if (Convert.ToByte(nextPos.Column + 1) > 4)
                    {
                        if (board[Convert.ToByte(nextPos.Row - 1), Convert.
                            ToByte(nextPos.Column - 1)].Position.Occupied)
                        {
                            canEat = true;
                            SetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row - 1),
                                            Convert.ToByte(nextPos.Column - 1));
                            
                        }
                    }
                    else
                    {
                        
                        if (board[Convert.ToByte(nextPos.Row - 1), nextPos.
                        Column].Position.Occupied)
                        {
                            canEat = true;
                            SetEatMovement(nextPos.Row, nextPos.Column);
                            SetKilledPiecePos(Convert.ToByte(nextPos.Row - 1), 
                                            nextPos.Column);

                        }
                    }
                     
                }
                else
                {
                    if (nextPos.Row > currentPos.Row)
                    {
                        if (nextPos.Column > currentPos.Column)
                        {
                            if (board[Convert.ToByte(nextPos.Row - 1), Convert.
                                ToByte(nextPos.Column - 1)].Position.Occupied)
                            {
                                canEat = true;
                                SetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row-1),
                                            Convert.ToByte(nextPos.Column - 1));
                            }
                        }

                        else if (nextPos.Column < currentPos.Column)
                        {
                            if (board[Convert.ToByte(nextPos.Row - 1), Convert.
                                ToByte(nextPos.Column + 1)].Position.Occupied)
                            {
                                canEat = true;
                                SetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row-1),
                                            Convert.ToByte(nextPos.Column + 1));
                            }
                        }

                        else
                        {
                            if (board[Convert.ToByte(nextPos.Row - 1), nextPos.
                                Column].Position.Occupied)
                            {
                                canEat = true;
                                SetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row-1),
                                                nextPos.Column);
                            }
                        }
                    }


                    // 
                    else if (nextPos.Row < currentPos.Row)
                    {
                        if (nextPos.Column > currentPos.Column)
                        {
                            if (board[Convert.ToByte(nextPos.Row + 1), Convert.
                                ToByte(nextPos.Column - 1)].Position.Occupied)
                            {
                                canEat = true;
                                SetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row+1),
                                            Convert.ToByte(nextPos.Column - 1));
                            }
                        }

                        else if (nextPos.Column < currentPos.Column)
                        {
                            if (board[Convert.ToByte(nextPos.Row + 1), Convert.
                                ToByte(nextPos.Column + 1)].Position.Occupied)
                            {
                                canEat = true;
                                SetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row+1),
                                            Convert.ToByte(nextPos.Column + 1));
                            }
                        }

                        else
                        {
                            if (board[Convert.ToByte(nextPos.Row + 1), nextPos.
                                Column].Position.Occupied)
                            {
                                canEat = true;
                                SetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(Convert.ToByte(nextPos.Row+1),
                                                nextPos.Column);
                            }
                        }
                    }
                    else
                    {
                        if (nextPos.Column > currentPos.Column)
                        {
                            if (board[nextPos.Row, Convert.ToByte(nextPos.
                                Column - 1)].Position.Occupied)
                            {
                                canEat = true;
                                SetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(nextPos.Row, Convert.
                                                ToByte(nextPos.Column - 1));
                            }
                        }
                        else
                        {
                            if (board[nextPos.Row, Convert.ToByte(nextPos.
                                Column + 1)].Position.Occupied)
                            {
                                canEat = true;
                                SetEatMovement(nextPos.Row, nextPos.Column);
                                SetKilledPiecePos(nextPos.Row, Convert.
                                                ToByte(nextPos.Column + 1));
                            }
                        }
                    }
                }
            }
            return canEat;
        }

        private void SetEatMovement(byte row, byte column)
        {
            EatMovement = new Position (row, column);
        }

        private void SetKilledPiecePos(byte row, byte column)
        {
            KilledPiecePos = new Position (row, column);
        }
    }
}