using System;
namespace Felli
{
    /// <summary>
    /// Class for user input.
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

        /// <summary>
        /// Constructor for the Input Class.
        /// </summary>
        /// <param name="playerOne">Variable from the Player class that.
        /// represents player One</param>
        /// <param name="playerTwo">Variable from the Player class that.
        /// represents player Two</param>
        /// <param name="playerName">Name of each player's pieces.</param>
        /// <param name="board"></param>
        public Input (Player[] playerOne, Player[] playerTwo, 
                string playerName, Board[,] board)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
            PlayerName = playerName;
            Board = board;

        }
        /// <summary>
        /// Empty Constructor used to create instaces of the class.
        /// </summary>
        public Input (){}
        
        /// <summary>
        /// Gets player's intended move coordinates.
        /// </summary>
        /// <param name="currentPos">Current Player position.</param>
        /// <returns>Returns an instace of the selected piece</returns>
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
                //Asks input to the player
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
                        //Checks if Input is Valid
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

        /// <summary>
        /// Method that asks for Input to select a piece
        /// </summary>
        /// <returns>Returns the name of the chosen piece</returns>
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
        
        /// <summary>
        /// Method responsible for testing the input of the player for invalid
        /// inputs
        /// </summary>
        /// <param name="inputString">Variable that saves the 
        /// player's input</param>
        /// <returns>Returns TRUE if the input is valid or FALSE
        /// if it isnt.</returns>
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
        

        /// <summary>
        /// Method responsible for checking if the movement is within the 
        /// board's boundaries
        /// </summary>
        /// <param name="nextPos">Variable that holds the input for 
        /// the movement</param>
        /// <returns>Returns TRUE if the input is valid or FALSE if not</returns>
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

        /// <summary>
        /// Method that checks if the movement is possible
        /// </summary>
        /// <param name="currentPos">Variable that holds the current position of
        /// the selected piece</param>
        /// <param name="nextPos">Variable that holds the input for the 
        /// movement</param>
        /// <param name="board">Instance of the Board Class that holds all the 
        /// parameters of the current game</param>
        /// <returns>Returns TRUE if removing the piece is possible or
        /// FALSE if not</returns>
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


        /// <summary>
        /// Method that checks if its possible to remove an opponents piece
        /// with the movement input
        /// </summary>
        /// <param name="currentPos">Variable that holds the current position of
        /// the selected piece</param>
        /// <param name="nextPos">Variable that holds the input for the 
        /// movement</param>
        /// <param name="board">Instance of the Board Class that holds all the 
        /// parameters of the current game</param>
        /// <returns>Returns TRUE if conditions are met or FALSE
        /// otherwise</returns>
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

        /// <summary>
        /// Method that checks if the input is whitin one house of distance from
        /// the current position
        /// </summary>
        /// <param name="currentPos">Variable that holds the current position of
        /// the selected piece</param>
        /// <param name="nextPos">Variable that holds the input for the 
        /// movement</param>
        /// <returns>Returns TRUE if the input is valid or FALSE if 
        /// not</returns>
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
                if(currentPos.Row == 0 || currentPos.Row == 4)
                {
                    if (nextPos.Column == currentPos.Column + 2 ||
                        nextPos.Column == currentPos.Column - 2)
                        {
                            canMove = true;
                        }
                }
                else if (nextPos.Column == currentPos.Column + 1 ||
                    nextPos.Column == currentPos.Column - 1)
                    canMove = true;
            }
            return canMove;
        }


        /// <summary>
        /// Method that checks if the input to remove a piece is valid or 
        /// possible
        /// </summary>
        /// <param name="currentPos">Variable that holds the current position of
        /// the selected piece</param>
        /// <param name="nextPos">Variable that holds the input 
        /// for the movement</param>
        /// <returns>Returns TRUE if the input is valid or FALSE if not</param>
        /// <param name="board">Instance of the Board Class that holds all the 
        /// parameters of the current game</param>
        /// <returns></returns>
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

        /// <summary>
        /// Method that sets the piece's movement to the one inputed
        /// </summary>
        /// <param name="row"> Variable that holds the row value
        ///  of the piece </param>
        /// <param name="column">Variable that holds the column value
        ///  of the piece</param>
        private void SetEatMovement(byte row, byte column)
        {
            EatMovement = new Position (row, column);
        }

        /// <summary>
        /// Method that sets the piece's position has the removed piece
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>//
        private void SetKilledPiecePos(byte row, byte column)
        {
            KilledPiecePos = new Position (row, column);
        }
    }
}