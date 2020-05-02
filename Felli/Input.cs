using System;
namespace Felli
{
    /// <summary>
    /// Class for user input
    /// </summary>
    public class Input
    {
        public Position pos { get; private set; }

        public Position GetPosition()
        {
            Renderer print = new Renderer();
            byte aux1, aux2;
            Position pos;
            print.RenderMessage("InsertRow");
            aux1 = Convert.ToByte(Console.ReadLine());
            print.RenderMessage("InsertColumn");
            aux2 = Convert.ToByte(Console.ReadLine());
            pos = new Position(aux1,aux2);
            return pos;
        }

        public bool GameBoundaries(Position nextPos)
        {
            bool result = true;

            // TRY-CATCH



            return result;
        }

        public bool Movement(Position currentPos, Position nextPos)
        {
            bool canMove = false;

            if (GameBoundaries(nextPos))
                if (nextPos.IsPlayable)
                    if (OneSquareMovement(currentPos,nextPos))
                        canMove = true;

            return canMove;
        }

        public bool Eat(Position currentPos, Position nextPos, Board[,] board)
        {
            bool canMove = false;
            
            if (GameBoundaries(nextPos))
                if (OneSquareMovement(currentPos,nextPos))
                    if (CheckPossibleEat(currentPos, nextPos, board))
                        canMove = true;
                
            return canMove;
        }

        private bool OneSquareMovement(Position currentPos, Position nextPos)
        {
            bool canMove = false;

            if (nextPos.Row == currentPos.Row + 1 ||
                nextPos.Row == currentPos.Row - 1 ||
                nextPos.Column == currentPos.Column + 1 ||
                nextPos.Column == currentPos.Column -1)
                canMove = true;

            return canMove;
        }

        private bool CheckPossibleEat(Position currentPos, Position nextPos, Board[,] board)
        {
            bool canEat = false;

            if (currentPos.Row < nextPos.Row && currentPos.Column < nextPos.Column)
                if (board[nextPos.Row+1,nextPos.Column+1].Position.IsPlayable)
                {
                    canEat = true;
                    EatMovement(Convert.ToByte(nextPos.Row+1),Convert.ToByte(nextPos.Column+1));
                }
            if (currentPos.Row < nextPos.Row && currentPos.Column > nextPos.Column)
                if (board[nextPos.Row+1,nextPos.Column-1].Position.IsPlayable)
                {
                    canEat = true;
                    EatMovement(Convert.ToByte(nextPos.Row+1),Convert.ToByte(nextPos.Column-1));
                }
            if (currentPos.Row > nextPos.Row && currentPos.Column < nextPos.Column)
                if (board[nextPos.Row-1,nextPos.Column+1].Position.IsPlayable)
                {
                    canEat = true;
                    EatMovement(Convert.ToByte(nextPos.Row-1),Convert.ToByte(nextPos.Column+1));
                }
            if (currentPos.Row > nextPos.Row && currentPos.Column > nextPos.Column)
                if (board[nextPos.Row-1,nextPos.Column-1].Position.IsPlayable)
                {
                    canEat = true;
                    EatMovement(Convert.ToByte(nextPos.Row-1),Convert.ToByte(nextPos.Column-1));
                }
            if (currentPos.Row == nextPos.Row && currentPos.Column < nextPos.Column)
                if (board[nextPos.Row,nextPos.Column+1].Position.IsPlayable)
                {
                    canEat = true;
                    EatMovement(Convert.ToByte(nextPos.Row),Convert.ToByte(nextPos.Column+1));
                }
            if (currentPos.Row == nextPos.Row && currentPos.Column > nextPos.Column)
                if (board[nextPos.Row,nextPos.Column-1].Position.IsPlayable)
                {
                    canEat = true;
                    EatMovement(Convert.ToByte(nextPos.Row),Convert.ToByte(nextPos.Column-1));
                }
            if (currentPos.Row < nextPos.Row && currentPos.Column == nextPos.Column)
                if (board[nextPos.Row+1,nextPos.Column].Position.IsPlayable)
                {
                    canEat = true;
                    EatMovement(Convert.ToByte(nextPos.Row+1),Convert.ToByte(nextPos.Column));
                }
            if (currentPos.Row > nextPos.Row && currentPos.Column == nextPos.Column)
                if (board[nextPos.Row-1,nextPos.Column].Position.IsPlayable)
                {
                    canEat = true;
                    EatMovement(Convert.ToByte(nextPos.Row-1),Convert.ToByte(nextPos.Column));
                }
                    
            return canEat;
        }

        public void EatMovement(byte row, byte column)
        {
            pos = new Position (row, column);
        }
    }
}