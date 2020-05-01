using System.ComponentModel;
using System.Dynamic;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System;
namespace Felli
{
    /// <summary>
    /// Class for player character
    /// </summary>
    public class Player
    {
        public string Name { get; }

        public Position Position { get; set; }

        public Player(string name, Position position)
        {
            Name = name;
            Position = position;
        }

        public Position GetInput()
        {
            uint aux1, aux2;
            Console.Write("Insert a row number: ");
            aux1 = Convert.ToUInt32(Console.ReadLine());

            Console.Write("Insert a row number: ");
            aux2 = Convert.ToUInt32(Console.ReadLine());
            Position pos = new Position(aux1,aux2);
            return pos;
        }

    }
}