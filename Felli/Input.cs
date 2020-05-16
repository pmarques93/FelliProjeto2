using System;
namespace Felli
{
    /// <summary>
    /// Class for user input.
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Gets or sets a Position value.
        /// </summary>
        public Position EatMovement { get; private set; }

        /// <summary>
        /// Gets or sets a Position value.
        /// </summary>
        public Position KilledPiecePos {get; private set;}

        /// <summary>
        /// Gets or sets a value indicating whether the game is over or not.
        /// </summary>
        public bool QuitInput {get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player changed piece or
        /// not.
        /// </summary>
        public bool ChangePieceInput {get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the movement is valid or 
        /// not.
        /// </summary>
        public bool ValidMove { get; private set; }

        /// <summary>
        /// Gets or sets a Player value.
        /// </summary>
        private Player[] PlayerOne {get; set;}

        /// <summary>
        /// Gets or sets a Player value.
        /// </summary>
        private Player[] PlayerTwo {get; set;}

        /// <summary>
        /// Gets or sets a Player's name.
        /// </summary>
        private string PlayerName {get; set;}

        /// <summary>
        /// Gets or sets a Board value.
        /// </summary>
        private Board[,] Board {get; set;}

        /// <summary>
        /// Constructor for the Input Class.
        /// </summary>
        /// <param name="playerOne">Input for Player 1.</param>
        /// <param name="playerTwo">Input for Player 2.</param>
        /// <param name="playerName">Name of each player's pieces.</param>
        /// <param name="board">Array with all board positions.</param>

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
        /// <returns>Returns an instace of the selected piece.</returns>
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
        /// Asks for Input to select a piece.
        /// </summary>
        /// <returns>Returns the name of the chosen piece.</returns>
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
        /// Tests the input of the player for invalid
        /// </summary>
        /// <param name="inputString">Player's input</param>
        /// <returns>Returns TRUE if the input is valid, otherwise FALSE.</returns>
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
        /// Checks if the movement is within the board's boundaries.
        /// </summary>
        /// <param name="nextPos">Input for desired movement</param>
        /// <returns>Returns TRUE if the input is valid, otherwise FALSE.</returns>
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
        /// Checks if the movement is possible.
        /// </summary>
        /// <param name="currentPos">Current piece Position.</param>
        /// <param name="nextPos">Input for movement.</param>
        /// <param name="board">Array with all board Positions.</param>
        /// <returns>Returns TRUE if removing the piece is possible, otherwise
        /// FALSE.</returns>
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
        /// Checks if its possible to remove an opponents piece.
        /// </summary>
        /// <param name="currentPos">Current position of the selected piece.</param>
        /// <param name="nextPos">Input for the movement.</param>
        /// <param name="board">Array with all board Positions.</param>
        /// <returns>Returns TRUE if its possible to remove the other player's
        /// piece, otherwise FALSE.</returns>
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
        /// Checks if the input is whithin one house distance from the current 
        /// position.
        /// </summary>
        /// <param name="currentPos">Current position of the selected 
        /// piece</param>
        /// <param name="nextPos">Input for the movement</param>
        /// <returns>Returns TRUE if the input is valid, otherwise FALSE
        /// </returns>
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
                            if (nextPos.Row == currentPos.Row)
                            {
                                Console.WriteLine("\nentou\n");
                                canMove = true;
                            }
                        }

                }
                else if (nextPos.Column == currentPos.Column + 1 ||
                    nextPos.Column == currentPos.Column - 1)
                    canMove = true;
            }
            return canMove;
        }


        /// <summary>
        /// Checks if the input to remove a piece is valid.
        /// </summary>
        /// <param name="currentPos">Current position of the selected piece.
        /// </param>
        /// <param name="nextPos">Input for the movement.</param>
        /// <param name="board">Array with all board Positions.</param>
        /// <returns>returns TRUE if the input is valid, otherwise FALSE.
        /// </returns>
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
        /// Sets the piece's movement to the chosen one.
        /// </summary>
        /// <param name="row"> Row value of the piece. </param>
        /// <param name="column">Column value of the piece.</param>
        private void SetEatMovement(byte row, byte column)
        {
            EatMovement = new Position (row, column);
        }

        /// <summary>
        /// Sets the piece's position to the removed piece's position.
        /// </summary>
        /// <param name="row"> Row value of the piece. </param>
        /// <param name="column">Column value of the piece.</param>
        private void SetKilledPiecePos(byte row, byte column)
        {
            KilledPiecePos = new Position (row, column);
        }
    }
}