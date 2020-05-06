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
        public bool ChangePieceInput {get; private set; }
        public bool ValidMove { get; private set; }

        private Player[] PlayerOne {get; set;}
        private Player[] PlayerTwo {get; set;}
        private string PlayerName {get; set;}
        private Board[,] Board {get; set;}

        public Input (Player[] playerOne, Player[] playerTwo, string playerName, Board[,] board)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            PlayerName = playerName;
            Board = board;

        }
        public Input (){}
        
        public Position GetPosition(Position currentPos)
        {
            Renderer print = new Renderer();
            byte row = 0;
            byte column = 0;
            bool validInput = false;
            string rowString, columnString;
            Position newPos;
            while (!(validInput))
            {
                print.RenderMessage("InsertRow");
                rowString = Console.ReadLine();
                if (rowString.ToLower() == "exit")
                {
                    validInput = true;
                    QuitInput = true;
                }
                else if (rowString.ToLower() == "back")
                {
                    validInput = true;
                    ChangePieceInput = true;
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
                    else if (columnString.ToLower() == "back")
                    {
                        validInput = true;
                        ChangePieceInput = true;
                    }
                    else
                    {
                        if (CheckConvert(rowString) && 
                            CheckConvert(columnString))
                        {
                            row = Convert.ToByte(rowString);
                            column = Convert.ToByte(columnString);
                            newPos = new Position(row, column);
                        
                            
                            if ((Math.Abs(
                                currentPos.Column - newPos.Column)) <= 2 &&
                                (Math.Abs(
                                    currentPos.Row - newPos.Row)) <= 2)
                            {
                                validInput = true;
                                ValidMove = true;
                            }
                                                            
                            else
                            {
                                validInput = true;
                                ValidMove = false;

                            }
                        }
                            
                            
                        QuitInput = false;
                    }
                }

            }

            newPos = new Position(row, column);
            return newPos;
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
            Renderer print = new Renderer(Board, 5);
            bool validInput = false;
            byte aux;
            try 
            {
                aux = Convert.ToByte(inputString);
                validInput = true;
            }
            
            catch (FormatException)
            {
                print.RenderBoard(PlayerOne, PlayerTwo, PlayerName);
                print.RenderMessage("MovementString");
            }
            catch (OverflowException)
            {
                print.RenderBoard(PlayerOne, PlayerTwo, PlayerName);
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

        public bool OneSquareMovement(Position currentPos, Position nextPos)
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

            sbyte [][] checkNext = new sbyte [8][]{
                new sbyte [2]{-2,-2}, new sbyte [2]{-2,2}, new sbyte [2]{2,2},
                new sbyte [2]{2,-2}, new sbyte [2]{-2,0}, new sbyte [2]{2,0},
                new sbyte [2]{0,2}, new sbyte [2]{0,-2}

            };

            for (sbyte i = 0; i < 8; i++)
            {
                if (nextPos.Row - currentPos.Row == checkNext[i][0] &&
                    nextPos.Column - currentPos.Column == checkNext[i][1])
                {
                    if (!(board[nextPos.Row,nextPos.Column].Position.Occupied)&&
                        board[
                            Convert.ToByte(
                                nextPos.Row + ((checkNext[i][0]/2)*(-1))), 
                            Convert.ToByte(
                                nextPos.Column + ((checkNext[i][1]/2)*(-1)))].
                                Position.Occupied)
                    {
                        canEat = true;
                        SetEatMovement(nextPos.Row, nextPos.Column);
                        SetKilledPiecePos(
                            Convert.ToByte(
                                nextPos.Row + ((checkNext[i][0]/2)*(-1))), 
                            Convert.ToByte(
                            nextPos.Column + ((checkNext[i][1]/2)*(-1))));
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