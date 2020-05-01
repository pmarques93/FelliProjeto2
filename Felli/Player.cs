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

        public bool Selected { get; set; }

        private Renderer print = new Renderer();

        public Player(string name, Position position)
        {
            Name = name;
            Position = position;
        }

        public Position GetPosition()
        {
        
            uint aux1, aux2;
            Position pos;
            print.RenderMessage("InsertRow");
            // Console.Write("Insert a row number: ");
            aux1 = Convert.ToUInt32(Console.ReadLine());

            print.RenderMessage("InsertColumn");
            aux2 = Convert.ToUInt32(Console.ReadLine());
            pos = new Position(aux1,aux2);
            return pos;
        }

        public void SelectPlayer()
        {
            Selected = true;
        }

        public void DeselectPlayer()
        {
            Selected = false;
        }

    }
}