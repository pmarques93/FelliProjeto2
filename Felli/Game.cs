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
            Renderer print = new Renderer(Board, boardSize);
            byte roundCounter = 0;
            Player[] selectedPlayer;
            Position newPosition = new Position(0,0);
            Position currentPosition = new Position(0,0);
            byte selectedPiece = 0;
            CreateGameBoard();
            CreatePlayer(1);
            CreatePlayer(2);

            // Gameloop - while not game over
            while (!(gameover))
            {   
                /* Checks if the round num is odd or even and defines the selected player 
                accordingly */
                if (roundCounter % 2 == 0)
                    selectedPlayer = playerOne;
                else
                    selectedPlayer = playerTwo;

                print.RenderMessage("SelectPiece");
                
                selectedPiece = Convert.ToByte(Console.ReadLine());
                selectedPlayer[selectedPiece].Selected = true;

                foreach (Player player in selectedPlayer)
                {
                    if (player.Selected)
                    {
                        print.RenderPlayer(player.Name);

                        newPosition = player.GetPosition();
                        currentPosition = new Position(player.Position.Row, player.Position.Column);
                    } 
                }

                if(roundCounter > 0)
                    if (Board[newPosition.Row, newPosition.Column].Position.IsPlayable)
                    {
                        Board[currentPosition.Row, currentPosition.Column].Position.FreeSpace();
                        selectedPlayer[selectedPiece].Position = newPosition;
                        Board[newPosition.Row, newPosition.Column].Position.OccupySpace();
                    }
                    else
                    {
                        print.RenderMessage("InvalidMove");
                        print.RenderBoard(playerOne, playerTwo);
                        continue;
                    }
            
                print.RenderBoard(playerOne, playerTwo);
                
                roundCounter ++;
                selectedPlayer[selectedPiece].Selected = false;
            }
            
        }

        public void CreateGameBoard()
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


        public void CreatePlayer(byte x)
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

        public void Quit()
            {
                gameover = true;
            }
    }
}