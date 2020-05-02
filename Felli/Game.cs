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

        // Runs on main method start
        public Game()
        {
            Board = new Board[boardSize,boardSize];
            playerOne = new Player[6];
            playerTwo = new Player[6];
        }

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
            // Piece choosing
            byte pieceIndex;
            string pieceChoice = "";

            CreateGameBoard();
            CreatePlayer(1);
            CreatePlayer(2);

            // Gameloop - while not game over
            while (!(gameover))
            {   
                newPosition = new Position(0,0);
                currentPosition = new Position(0,0);
                tempPosition = new Position(0,0);

                if (roundCounter == 0)
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
                    }
                        
                    else
                    {
                        print.RenderMessage("Player2Round");
                        selectedPlayer = playerTwo;
                    }
                        
                }
                else if (firstToPlay == 2)
                {
                    if (roundCounter % 2 == 0)
                    {
                        print.RenderMessage("Player2Round");
                        selectedPlayer = playerTwo;
                    }
                        
                    else
                    {
                        print.RenderMessage("Player1Round");
                        selectedPlayer = playerOne;
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
                            pieceIndex = piece.Index;
                            selectedPlayer[pieceIndex].Selected = true;
                            if (piece.Selected)
                            {
                                print.RenderPlayer(piece.Name);
                                
                                currentPosition = new Position(piece.Position.Row, piece.Position.Column);
                                do
                                {
                                    tempPosition = input.GetPosition();
                                    // If position isn't occupied
                                    if (!(Board[tempPosition.Row,tempPosition.Column].Position.Occupied))
                                        if (input.Movement(currentPosition, Board[tempPosition.Row,tempPosition.Column].Position))
                                        {
                                            newPosition = tempPosition;
                                            continue;
                                        }

                                    // If position is occupied
                                    if (Board[tempPosition.Row,tempPosition.Column].Position.Occupied)
                                    {
                                        
                                       
                                        // cleans the positions
                                        Board[currentPosition.Row, currentPosition.Column].Position.FreeSpace();
                                        Board[tempPosition.Row, tempPosition.Column].Position.FreeSpace();

                                        // Kills the eaten player
                                        foreach (Player player in playerTwo)
                                            if (Board[tempPosition.Row, tempPosition.Column].Position.Row == player.Position.Row &&
                                            Board[tempPosition.Row, tempPosition.Column].Position.Column == player.Position.Column)
                                                player.Die();

                                        foreach (Player player in playerOne)
                                            if (Board[tempPosition.Row, tempPosition.Column].Position.Row == player.Position.Row &&
                                            Board[tempPosition.Row, tempPosition.Column].Position.Column == player.Position.Column)
                                                player.Die();


                                        if (input.Eat(currentPosition, Board[tempPosition.Row,tempPosition.Column].Position, Board))
                                        {
                                            Console.Write("EQWEQWEQWE");
                                            newPosition = tempPosition;
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        print.RenderMessage("InvalidMove");
                                        print.RenderBoard(playerOne, playerTwo);
                                    }
                                }while(newPosition != tempPosition);  
                            } 
                        }
                    }
                    else
                        continue;
                }
                if (Board[newPosition.Row , newPosition.Column].Position.IsPlayable)
                {
                    Board[currentPosition.Row, currentPosition.Column].Position.FreeSpace();
                    selectedPlayer[pieceIndex].Position = newPosition;
                    Board[newPosition.Row, newPosition.Column].Position.OccupySpace();
                }

                            

                roundCounter ++;
                // Variable
                selectedPlayer[pieceIndex].Selected = false;
            }
            
        }

        private void CreateGameBoard()
        {
            // Creates board
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

        private void Quit()
        {
            gameover = true;
        }
    }
}