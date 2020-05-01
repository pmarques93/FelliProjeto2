using System;
namespace Felli
{
    /// <summary>
    /// Class for rendering
    /// </summary>
    public class Renderer
    {
        private Board[,] board;

        private uint boardSize;
        
        public Renderer(Board[,] board, uint boardSize)
        {
            this.board = board;
            this.boardSize = boardSize;
        }

        // Renders the board
        public void Render(Player[] playerOne, Player[] playerTwo)
        {
            Console.WriteLine("");
            for (uint i = 0; i < boardSize; i++)
            {
                Console.Write($" ");
                for (uint j = 0; j < boardSize; j++)
                {
                    // Prints playable positions
                    if (board[i,j].Position.IsPlayable)
                    {
                        Console.Write($"{board[i,j].Position.Row}");
                        Console.Write($"{board[i,j].Position.Column} ");
                    }

                    // Prints occupied cells with player 1 / 2
                    else if (board[i,j].Position.Occupied)
                    {
                        foreach (Player player in playerOne)
                        {
                            if (player.IsAlive)
                                if (ComparePosition(board[i,j], player))
                                    Console.Write($"{player.Name} ");
                        }
                        foreach (Player player in playerTwo)    
                        {
                            if (player.IsAlive)
                                if (ComparePosition(board[i,j], player))           
                                    Console.Write($"{player.Name} ");
                        }
                    } 
                    else
                        Console.Write($"   ");  
                }
                // Prints playable numbers on middle row
                if (i == 2)
                    PossiblePlays();

                Console.WriteLine("");
            }
        }

        // Prints possible plays
        public void PossiblePlays()
        {
            Console.Write($"              Playable Numbers: ");
            foreach (Board position in board)
            {
                if (position.Position.IsPlayable)
                {
                    Console.Write($"{position.Position.Row}");
                    Console.Write($"{position.Position.Column}");
                }
            }
        }

        // Returns true for equal positions
        public bool ComparePosition(Board board, Player player) 
        {
            bool x = false;
            if (board.Position.Row == player.Position.Row &&
                board.Position.Column == player.Position.Column)
                x = true;

            return x;
        }
    }
}