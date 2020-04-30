using System; // apagar
namespace Felli
{
    /// <summary>
    /// Class responsible for running the game
    /// </summary>
    public class Game
    {
        public Board[,] Board { get; private set; }
        public const uint boardSize = 5;
        private bool gameover = false;
        private Renderer printBoard;
        private Player[] playerOnePieces;
        private Player[] playerTwoPieces;

        // Runs on main method start
        public Game()
        {
            Board = new Board[boardSize,boardSize];
            printBoard = new Renderer(Board, boardSize);
            playerOnePieces = new Player[6];
            playerTwoPieces = new Player[6];
        }

        public void Run()
        {
            // Variables
            // Used to create player number/name
            int temp = 0;
            
            // Creates board
            for (uint i = 0; i < boardSize; i++)
            {
                for (uint j = 0; j < boardSize; j++)
                {
                    // Creates true and false positions in game board
                    if (j == 2)
                        Board[i,j] = new Board(new Position(i,j, 
                            new State(true)));
                    else if (i == 2)
                        Board[i,j] = new Board(new Position(i,j, 
                            new State(false)));
                    else if (i % 2 == 0 && j %2 == 0)
                        Board[i,j] = new Board(new Position(i,j, 
                            new State(true)));
                    else if (i % 2 != 0 && j %2 != 0)
                        Board[i,j] = new Board(new Position(i,j, 
                            new State(true)));
                    else
                        Board[i,j] = new Board(new Position(i,j, 
                            new State(false)));
                }
            }

            // Creates player pieces
            for (uint i = 0; i < 2; i++)
            {
                for (uint j = 0; j < boardSize; j++)
                {
                    ///////////// SEGMENTATION FAULT /////////////////////
                    /*
                    // Gives player1 positions
                    if (Board[i,j].Position.IsPlayable.PlayableCheck)
                    {
                        playerOnePieces[temp] = new Player($"P1{temp}",
                            new Position(i,j, new State(false)));
                        temp++;
                    }
                    */
                 
                }
            }




            // Gameloop - while not game over
            while (!(gameover))
            {
                // Renders board
                printBoard.Render();

                // False to create the loop
                gameover = true;
            }
        }
    }
}