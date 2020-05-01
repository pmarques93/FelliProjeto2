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
        private Renderer print;
        private Player[] playerOne, playerTwo;
        private string pieceName = "";
        private byte firstToPlay = 0;

        // Runs on main method start
        public Game()
        {
            Board = new Board[boardSize,boardSize];
            print = new Renderer(Board, boardSize);
            playerOne = new Player[6];
            playerTwo = new Player[6];
        }

        public void Run()
        {
            byte roundCounter = 0;
            Player[] selectedPlayer;
            Position newPosition = new Position(0,0);
            Position currentPosition = new Position(0,0);
            byte pieceIndex = 0;
            CreateGameBoard();
            CreatePlayer(1);
            CreatePlayer(2);

            // Gameloop - while not game over
            while (!(gameover))
            {   

                if (roundCounter == 0)
                {
                    print.RenderMessage("FirstRound");
                    firstToPlay = Convert.ToByte(Console.ReadLine());
                }

                /* Checks if the round num is odd or even and defines the selected player 
                accordingly */
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
                pieceName = Console.ReadLine().ToUpper();
                pieceIndex = 0;
                foreach (Player piece in selectedPlayer)
                {
                    
                    if (piece.Name == pieceName)
                    {   
                        pieceIndex = piece.Index;
                        selectedPlayer[pieceIndex].Selected = true;
                        if (piece.Selected)
                        {
                            
                            print.RenderPlayer(piece.Name);

                            newPosition = piece.GetPosition();
                            currentPosition = new Position(piece.Position.Row, piece.Position.Column);
                        } 
                    }

                }

        
                if (Board[newPosition.Row, newPosition.Column].Position.IsPlayable)
                {
                    Board[currentPosition.Row, currentPosition.Column].Position.FreeSpace();
                    selectedPlayer[pieceIndex].Position = newPosition;
                    Board[newPosition.Row, newPosition.Column].Position.OccupySpace();
                }
                else
                {
                    print.RenderMessage("InvalidMove");
                    print.RenderBoard(playerOne, playerTwo);
                    continue;
                }
                                
                // print.RenderBoard(playerOne, playerTwo);


                // False to create the loop
                gameover = false;   
                roundCounter ++;
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