using System;
namespace Felli
{
    /// <summary>
    /// Class for user input
    /// </summary>
    public class Input
    {
        public Input(){}

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
                if (nextPos.Occupied == false)
                    if (nextPos.IsPlayable)
                        if (nextPos.Row == currentPos.Row + 1 ||
                            nextPos.Row == currentPos.Row - 1 ||
                            nextPos.Column == currentPos.Column + 1 ||
                            nextPos.Column == currentPos.Column -1)
                        {
                            canMove = true;
                        }

            return canMove;
        }
    }
}