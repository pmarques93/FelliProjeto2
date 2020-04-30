namespace Felli
{
    /// <summary>
    /// Class responsible for running the game
    /// </summary>
    public class Game
    {
        private Board[,] Board { get; set; }
        public uint Boardsize { get; set; } = 5;
        static bool gameover = false;
        private Renderer printBoard;

        // Runs on main method start
        public Game()
        {
            Board = new Board[Boardsize,Boardsize];
            printBoard = new Renderer(Board, Boardsize);
        }

        public void Run()
        {
            
            for (uint i = 0; i < Boardsize; i++)
            {
                for (uint j = 0; j < Boardsize; j++)
                {
                    // Creates true and false positions in game board
                    if (j == 2)
                        Board[i,j] = new Board(new Position(i,j), 
                        new State(true));
                    else if (i == 2)
                        Board[i,j] = new Board(new Position(i,j), 
                        new State(false));
                    else if (i % 2 == 0 && j %2 == 0)
                        Board[i,j] = new Board(new Position(i,j), 
                        new State(true));
                    else if (i % 2 != 0 && j %2 != 0)
                        Board[i,j] = new Board(new Position(i,j), 
                        new State(true));
                    else
                        Board[i,j] = new Board(new Position(i,j), 
                        new State(false));
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