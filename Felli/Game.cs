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

        /// <summary>
        /// Game class constructor
        /// </summary>
        public Game()
        {
            Board = new Board[boardSize,boardSize];
            playerOne = new Player[6];
            playerTwo = new Player[6];
        }

        /// <summary>
        /// Used to run the game, contains the main gameloop
        /// </summary>
        public void Run()
        {
            // Variables for input / renderer classes
            Input input = new Input();
            Renderer print = new Renderer(Board, boardSize);
            byte roundCounter = 0;
            // Player movement variables
            Player[] selectedPlayer;
            Position newPosition;
            Position currentPosition;
            Position tempPosition;
            Victory winCondition;
            byte firstToPlay = 0;
            
            
            // Piece choosing
            byte pieceIndex;
            string pieceChoice = "";
            string playerName = "";
            
            CreateGameBoard();
            CreatePlayer(1);
            CreatePlayer(2);

            // Gameloop - while not game over
            while (!(gameover))
            {   
                
                
                newPosition = new Position(0,0);
                currentPosition = new Position(0,0);
                tempPosition = new Position(0,0);
                bool validPiece = false;
                canMove = false;

                if (roundCounter == 0 && firstToPlay == 0)
                {
                    print.RenderMessage("FirstRound");
                    firstToPlay = Convert.ToByte(Console.ReadLine());
                }
                // Checks round to define player turn
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
                else
                    continue;
                
                print.RenderBoard(playerOne, playerTwo);
                print.RenderMessage("SelectPiece");
                pieceChoice = Console.ReadLine().ToUpper();
                pieceIndex = 0;
                // for (int i = 0; i < 5; i ++)
                // {
                //     for (int j = 0; j < 5; j ++)
                //     {
                //         if (Board[i,j].Position.Occupied)
                //             Console.WriteLine($"[{i}, {j}: Occupied]");
                //         else
                //             Console.WriteLine($"[{i}, {j}: Free    ]");

                winCondition = new Victory (Board);
                if(winCondition.WinChecker(currentPosition, Board, selectedPlayer) == true)
                {
                    Console.WriteLine("this did it");
                    gameover = true;
                }
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
                                do
                                {
                                    tempPosition = input.GetPosition();
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
                                        // Check if it's possible to eat a piece
                                        if (input.Eat(currentPosition, Board[tempPosition.Row,tempPosition.Column].Position, Board))
                                        {
                                            
                                            // Kills enemy piece
                                            PlayerKill(playerName, tempPosition, input);
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
                                    else
                                    {
                                        print.RenderMessage("InvalidMove");
                                        print.RenderBoard(playerOne, playerTwo);
                                    }
    
                                }while(canMove == false);  
                            } 
                            break;
                        }
                        else
                            validPiece = false;
                    }
                    else
                        continue;
                }

                if (!(validPiece))
                {
                    continue;
                }
                
                // Check if the position where the player if trying to go is playable
                if (Board[newPosition.Row , newPosition.Column].Position.
                    IsPlayable)
                {
                    // Makes piece's previous position playable and availabe
                    Board[currentPosition.Row, currentPosition.Column].Position
                        .FreeSpace();
                    // Makes the curret piece position occupied and not playable
                    selectedPlayer[pieceIndex].Position = newPosition;
                    Board[newPosition.Row, newPosition.Column].Position.
                        OccupySpace();
                }
                
                // Increments the number of rounds
                roundCounter ++;

                // Variable
                selectedPlayer[pieceIndex].Selected = false;
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
        private void PlayerKill(string pName, Position tempPosition, Input input)
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
        /// Compares position beetween the board and a player
        /// </summary>
        /// <param name="board"> Board parameter to compare</param>
        /// <param name="player"> Player Parameter to compare</param>
        /// <returns> Returns true if both positions are equal</returns>
        private bool ComparePosition(Board board, Player player) 
        {
            bool x = false;
            if (board.Position.Row == player.Position.Row &&
                board.Position.Column == player.Position.Column)
                x = true;

            return x;
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