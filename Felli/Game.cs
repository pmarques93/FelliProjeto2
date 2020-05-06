using System;
namespace Felli
{
    /// <summary>
    /// Class responsible for running the game
    /// </summary>
    public class Game
    {
        public Board[,] Board { get; private set; }
        private const byte boardSize = 5;
        private bool gameover = false;
        private Player[] playerOne, playerTwo;
        private bool canMove;
        private Player[] selectedPlayer;
        private Renderer print;
        private string playerName;

        /// <summary>
        /// Game class constructor
        /// </summary>
        public Game()
        {
            Board = new Board[boardSize,boardSize];
            playerOne = new Player[6];
            playerTwo = new Player[6];
            print = new Renderer(Board, boardSize);
        }

        /// <summary>
        /// Used to run the game, contains the main gameloop
        /// </summary>
        public void Run()
        {
            // Input class
            // Input input = new Input();
            Victory winCondition = new Victory();
            
            byte roundCounter = 0;
            // Player movement variables
            Position newPosition;
            Position currentPosition;
            Position tempPosition;
            byte firstToPlay = 0;
            
            // Piece choosing
            byte pieceIndex;
            string pieceChoice = "";
            playerName = "";
            
            // Initial run methods
            print.PrintRules();
            CreateGameBoard();
            CreatePlayer(1);
            CreatePlayer(2);
            
            // Gameloop - while not game over
            while (!(gameover))
            {   
                // Variables Reset
                newPosition = new Position(0,0);
                currentPosition = new Position(0,0);
                tempPosition = new Position(0,0);
                bool validPiece = false;
                bool changePiece = false;
                canMove = false;
                pieceIndex = 0;
                Input input = new Input(playerOne, playerTwo, playerName, Board);
                if (roundCounter == 0 && firstToPlay != 1 && firstToPlay != 2)
                {
                    print.RenderMessage("FirstRound");
                    string auxInput = Console.ReadLine();
                    if (auxInput.ToLower() == "exit")
                        Quit();
                    else
                    {
                        if (input.CheckConvert(auxInput))
                        {
                            firstToPlay = Convert.ToByte(auxInput);
                        }
                            
                        else
                            continue;
                    }
                }
                // Checks round to define player turn
                if (firstToPlay == 1 || firstToPlay == 2)
                    PlayerTurn(firstToPlay, roundCounter);
                else
                {
                    continue;
                }
           
                print.RenderBoard(playerOne, playerTwo, playerName);
                pieceChoice = input.GetPiece();
                pieceIndex = 0;
                gameover = input.QuitInput;

    
                foreach (Player piece in selectedPlayer)
                {   
                    if (piece.IsAlive)
                    {
                        if (piece.Name == pieceChoice)
                        {   
                            validPiece = true;
                            pieceIndex = piece.Index;
                            selectedPlayer[pieceIndex].Selected = true;
                            if (piece.Selected)
                            {
                                print.RenderPlayer(piece.Name);
                                
                                currentPosition = new Position(piece.Position.
                                    Row, piece.Position.Column);

                                // Variable to print board if input fails
                                byte boardFailInput = 0;
                                do
                                {
                                    // Prints board only if input fails
                                    if (boardFailInput > 0)
                                        print.RenderBoard(playerOne, playerTwo,
                                                       playerName);


                                    tempPosition = input.GetPosition(currentPosition);
                                    gameover = input.QuitInput;
                                    changePiece = input.ChangePieceInput;

                                    if (!(input.GameBoundaries(tempPosition)) || !(input.ValidMove))
                                    {
                                        print.RenderBoard(playerOne, playerTwo,
                                        playerName);
                                        print.RenderMessage("InvalidMove");
                                        continue;
                                    }
                                    // Checks if position isn't occupied
                                    if (!(BoardOccupied(tempPosition)))
                                    {
                                        // Check if it's possible to move 1 cell
                                        if (input.Movement(currentPosition, 
                                            Board[tempPosition.Row,tempPosition.
                                            Column].Position, Board))
                                        {
                                            newPosition = tempPosition;
                                            canMove = true;
                                            continue;
                                        }
                                        // If the movement is greater than 1
                                        // Check if it's possible to eat a piece
                                        if (input.Eat(currentPosition, 
                                            Board[tempPosition.Row,tempPosition.
                                            Column].Position, Board))
                                        {
                                            
                                            // Kills enemy piece
                                            PlayerKill(playerName, tempPosition,
                                                    input);
                                            // Gives player new pos
                                            if (canMove == true)
                                            {
                                                tempPosition = input.EatMovement;
                                                newPosition = tempPosition;
                                                continue;
                                            }
                                        }
                                     
                                    }
                                    // If a move or a eat isn't possible
                                    // the move is consider as invalid
                                    else if (!(gameover) && !changePiece)
                                    {  
                                        print.RenderMessage("InvalidMove");
                                        if (boardFailInput > 0)
                                            print.RenderBoard(playerOne, 
                                                        playerTwo, playerName);
                                        
                                    }

                                    // Variable to print board if input fails
                                    boardFailInput++;
                                }while(canMove == false && gameover == false &&
                                       changePiece == false);  
                            } 
                            break;
                        }
                        else
                            validPiece = false;
                    }
                    else
                        continue;
                }

                
                if (!(validPiece) || (changePiece) || !(input.ValidMove))
                {
                    continue;
                }
                
                // Check if desired position is playable
                if (Board[newPosition.Row , newPosition.Column].Position.
                    IsPlayable)
                {
                    // Makes piece's previous position playable and availabe
                    Board[currentPosition.Row, currentPosition.Column].Position.
                        FreeSpace();
                    // Makes the curret piece position occupied and not playable
                    selectedPlayer[pieceIndex].Position = newPosition;
                    Board[newPosition.Row, newPosition.Column].Position.
                        OccupySpace();
                }
                

                // Gameover checking
                if (roundCounter > 0)
                {
                    if(winCondition.Gameover(playerOne, playerTwo, Board))
                    {
                        print.RenderBoard(playerOne, playerTwo, playerName);
                        Quit();
                        break;
                    }
                }

                // Increments the number of rounds
                roundCounter ++;

                // Variable
                selectedPlayer[pieceIndex].Selected = false;
            }
        }

        /// <summary>
        /// Defines player's turn
        /// </summary>
        /// <param name="firstToPlay"> Defines first player to play</param>
        /// <param name="roundCounter"> Parameter with round number</param>
        private void PlayerTurn(byte firstToPlay, byte roundCounter)
        {
            if (firstToPlay == 1)
            {    
                if (roundCounter % 2 == 0)
                {
                    print.RenderMessage("Player1Round");
                    selectedPlayer = playerOne;
                    playerName = "p1";
                }
                    
                else
                {
                    print.RenderMessage("Player2Round");
                    selectedPlayer = playerTwo;
                    playerName = "p2";
                }
            }
            else if (firstToPlay == 2)
                {
                    if (roundCounter % 2 == 0)
                    {
                        print.RenderMessage("Player2Round");
                        selectedPlayer = playerTwo;
                        playerName = "p2";
                    }
                        
                    else
                    {
                        print.RenderMessage("Player1Round");
                        selectedPlayer = playerOne;
                        playerName = "p1";
                    }
                }
        }

        /// <summary>
        /// Checks if a position is occupied
        /// </summary>
        /// <param name="tempPosition"> Position to be checked</param>
        /// <returns> True if position is occupied</returns>//
        private bool BoardOccupied(Position tempPosition)
        {
            bool occupied = false;

            if (Board[tempPosition.Row,tempPosition.Column].Position.Occupied)
                occupied = true;

            return occupied;
        }

        /// <summary>
        /// Kills enemy piece
        /// </summary>
        /// <param name="pName">Chosen player</param>
        /// <param name="tempPosition">Temporary position input</param>
        private void PlayerKill(string pName, Position tempPosition,Input input)
        {
            byte killedPieceRow = input.KilledPiecePos.Row;
            byte killedPieceColumn = input.KilledPiecePos.Column;

            // Kills the piece with a position that corresponds to the 
            // killedPiece position
            if (pName == "p1")
            {
                foreach (Player p2 in playerTwo)
                {
                    if (killedPieceRow == p2.Position.Row &&
                        killedPieceColumn == p2.Position.Column)
                    {
                        p2.Die();
                        Board[killedPieceRow, killedPieceColumn].
                        Position.FreeSpace();
                        canMove = true;
                    }
                }
            }
            else if (pName == "p2")
            {
                foreach (Player p1 in playerOne)
                {
                    if (killedPieceRow == p1.Position.Row &&
                        killedPieceColumn == p1.Position.Column)
                    {
                        p1.Die();
                        Board[killedPieceRow, killedPieceColumn].
                        Position.FreeSpace();
                        canMove = true;
                    }
                }
            }
        }

        /// <summary>
        /// Creates game board
        /// </summary>
        private void CreateGameBoard()
        {
            for (byte i = 0; i < boardSize; i++)
            {
                for (byte j = 0; j < boardSize; j++)
                {
                    // Creates true and false positions in game board
                    if (j == 2)
                        Board[i,j] = new Board(new Position(i,j, true));
                    else if (i == 2)
                        Board[i,j] = new Board(new Position(i,j, false));
                    else if (i % 2 == 0 && j %2 == 0)
                        Board[i,j] = new Board(new Position(i,j, true));
                    else if (i % 2 != 0 && j %2 != 0)
                        Board[i,j] = new Board(new Position(i,j, true));
                    else
                        Board[i,j] = new Board(new Position(i, j, false));
                }
            }
        }

        /// <summary>
        /// Creates players
        /// </summary>
        /// <param name="x">Defines the number of the creating player</param>
        private void CreatePlayer(byte x)
        {   
            byte temp = 0;
            if (x == 1)
        
                // Creates player1 pieces
                for (byte i = 0; i < 2; i++)
                {
                    for (byte j = 0; j < boardSize; j++)
                    {
                        // Gives player1 positions
                        // Sets positions on board to false
                        if (Board[i,j].Position.IsPlayable)              
                        {
                            playerOne[temp] = new Player($"W{temp}",
                                new Position(i,j), true);
                            Board[i,j].Position.OccupySpace();
                            temp++;
                        }                
                    }
                }
            else
                // Creates player2 pieces
                for (byte i = 3; i < 5; i++)
                {
                    for (byte j = 0; j < boardSize; j++)
                    {
                        // Gives player1 positions
                        // Sets positions on board to false
                        if (Board[i,j].Position.IsPlayable)              
                        {
                            playerTwo[temp] = new Player($"B{temp}",
                                new Position(i,j), true);
                            Board[i,j].Position.OccupySpace();
                            temp++;
                        }                
                    }
                }
        }

        /// <summary>
        /// Quits the gameloop
        /// </summary>
        private void Quit()
        {
            gameover = true;
        }
    }
}