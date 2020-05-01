using System.Reflection.Metadata;
using System;
namespace Felli
{
    /// <summary>
    /// Class responsible for running the game
    /// </summary>
    public class Game
    {
        public Board[,] Board { get; private set; }
        private const uint boardSize = 5;
        private bool gameover = false;
        private Renderer printBoard;
        private Player[] playerOne, playerTwo;

        // Runs on main method start
        public Game()
        {
            Board = new Board[boardSize,boardSize];
            printBoard = new Renderer(Board, boardSize);
            playerOne = new Player[6];
            playerTwo = new Player[6];
        }

        public void Run()
        {
            int roundCounter = 0;
            Player[] selectedPlayer;
            Position tempPosition = new Position(0,0);
            Position currentPosition = new Position(0,0);
            CreateGameBoard();
            CreatePlayer(1);
            CreatePlayer(2);

            // Gameloop - while not game over
            while (!(gameover))
            {   
                if (roundCounter % 2 == 0)
                    selectedPlayer = playerOne;
                else
                    selectedPlayer = playerTwo;

                tempPosition = selectedPlayer[0].GetInput();
                currentPosition = new Position(selectedPlayer[0].Position.Row, selectedPlayer[0].Position.Column);

                if(roundCounter > 0)
                    if (Board[tempPosition.Row, tempPosition.Column].Position.IsPlayable)
                    {
                        Board[currentPosition.Row, currentPosition.Column].Position.FreeSpace();
                        selectedPlayer[0].Position = tempPosition;
                        Board[tempPosition.Row, tempPosition.Column].Position.OccupySpace();
                        tempPosition = new Position(0,0);
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid move");
                        printBoard.Render(playerOne, playerTwo);
                        continue;
                    }
                
                for (uint i = 0; i < boardSize; i++)
                {
                    for (uint j = 0; j < boardSize; j++)
                    {
                        Console.WriteLine($"[{i}, {j} - {Board[i,j].Position.IsPlayable}]");
                    }
                }    
            
                


                printBoard.Render(playerOne, playerTwo);


                // False to create the loop
                gameover = false;
                roundCounter ++;
            }
            
        }


        public void CreateGameBoard()
        {
            // Creates board
            for (uint i = 0; i < boardSize; i++)
            {
                for (uint j = 0; j < boardSize; j++)
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


        public void CreatePlayer(int x)
        {   
            int temp = 0;
            if (x == 1)
        
                // Creates player1 pieces
                for (uint i = 0; i < 2; i++)
                {
                    for (uint j = 0; j < boardSize; j++)
                    {
                        // Gives player1 positions
                        // Sets positions on board to false
                        if (Board[i,j].Position.IsPlayable)              
                        {
                            playerOne[temp] = new Player($"W{temp}",
                                new Position(i,j));
                            Board[i,j].Position.OccupySpace();
                            temp++;
                        }                
                    }
                }
            else
                // Creates player2 pieces
                for (uint i = 3; i < 5; i++)
                {
                    for (uint j = 0; j < boardSize; j++)
                    {
                        // Gives player1 positions
                        // Sets positions on board to false
                        if (Board[i,j].Position.IsPlayable)              
                        {
                            playerTwo[temp] = new Player($"B{temp}",
                                new Position(i,j));
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