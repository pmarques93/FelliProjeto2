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
            byte firstToPlay = 0;
            bool canMove;
            
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
                                    // If position isn't occupied
                                    if (!(BoardOccupied(tempPosition)))
                                        if (input.Movement(currentPosition, 
                                            Board[tempPosition.Row,tempPosition.
                                            Column].Position))
                                        {
                                            newPosition = tempPosition;
                                            canMove = true;
                                            continue;
                                        }

                                    // If position is occupied
                                    if (BoardOccupied(tempPosition))
                                    {
                                        if (input.Eat(currentPosition, 
                                            Board[tempPosition.Row,tempPosition.
                                            Column].Position, Board))
                                        {
                                            // Kills enemy piece
                                            PlayerKill(playerName, tempPosition);
                                            
                                            tempPosition = input.EatMovement;
                                            newPosition = tempPosition;
                                            canMove = true;
                                            
                                            continue;
                                        }
                                        continue;
                                    }
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
                
                if (Board[newPosition.Row , newPosition.Column].Position.
                    IsPlayable)
                {
                    Board[currentPosition.Row, currentPosition.Column].Position
                        .FreeSpace();
                    selectedPlayer[pieceIndex].Position = newPosition;
                    Board[newPosition.Row, newPosition.Column].Position.
                        OccupySpace();
                }

                roundCounter ++;
                // Variable reset
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
        private void PlayerKill(string pName, Position tempPosition)
        {
            if (pName == "p1")
            {
                // Kills the eaten player
                foreach (Player p2 in playerTwo)
                {
                    if (ComparePosition(
                        Board[tempPosition.Row, tempPosition.Column], p2))
                    {
                        // cleans the desired position
                        Board[tempPosition.Row, tempPosition.Column].Position.
                            FreeSpace();
                        // Kills p2 piece
                        p2.Die();
                    }
                }
            }
            else if (pName == "p2")
            {
                foreach (Player p1 in playerOne)
                {
                    if (ComparePosition(Board[tempPosition.Row, tempPosition.
                        Column], p1))
                    {
                        // cleans the desired position
                        Board[tempPosition.Row, tempPosition.Column].Position.
                            FreeSpace();
                        // Kills p1 piece
                        p1.Die();
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