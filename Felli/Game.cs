namespace Felli
{
    /// <summary>
    /// Class responsible for running the game
    /// </summary>
    public class Game
    {
        private Board[,] board { get; set; }
        public uint boardSize { get; set; } = 5;
        private static bool gameover = false;
        private Renderer printBoard;

        // Runs on main method start
        public Game()
        {
            this.board = new Board[boardSize,boardSize];
            printBoard = new Renderer(board, boardSize);
        }

        public void Run()
        {
            
            for (uint i = 0; i < boardSize; i++)
            {
                for (uint j = 0; j < boardSize; j++)
                {
                    // Creates true and false positions in game board
                    if (j == 2)
                        board[i,j] = new Board(new Position(i,j), 
                        new State(true));
                    else if (i == 2)
                        board[i,j] = new Board(new Position(i,j), 
                        new State(false));
                    else if (i % 2 == 0 && j %2 == 0)
                        board[i,j] = new Board(new Position(i,j), 
                        new State(true));
                    else if (i % 2 != 0 && j %2 != 0)
                        board[i,j] = new Board(new Position(i,j), 
                        new State(true));
                    else
                        board[i,j] = new Board(new Position(i,j), 
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