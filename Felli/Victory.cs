using System;

namespace Felli
{
    public class Victory
    {
        public Board[,] Board {get; set;}
        public Player[] SelectedPlayer {get; set;}
        private bool playerWon;
        public Victory (Board[,] board, Player[] selectedPlayer)
        {
            Board = board;
            SelectedPlayer = selectedPlayer;
        }
        
        public bool WinChecker()
        {
            Console.WriteLine("Test 0");
            byte posRow;
            byte posColumn;
            
            foreach(Player piece in SelectedPlayer)
            {
                 
                posRow = piece.Position.Row;
                posColumn = piece.Position.Column;
                
                if(Board[posRow+1, posColumn+1].Position.IsPlayable)
                {
                    Console.WriteLine("Test1");
                    playerWon = false;
                }
                if(Board[posRow+1, posColumn-1].Position.IsPlayable)
                {
                    Console.WriteLine("Test2");
                    playerWon = false;
                }
                if(Board[posRow-1, posColumn+1].Position.IsPlayable)
                {
                    Console.WriteLine("Test3");
                    playerWon = false;
                }
                if(Board[posRow-1, posColumn-1].Position.IsPlayable)
                {
                    Console.WriteLine("Test4");
                    playerWon = false;
                }
                if(Board[posRow+1, posColumn].Position.IsPlayable)
                {
                    Console.WriteLine("Test5");
                    playerWon = false;
                }
                if(Board[posRow-1, posColumn].Position.IsPlayable)
                {
                    Console.WriteLine("Test6");
                    playerWon = false;
                }
                if(Board[posRow, posColumn+1].Position.IsPlayable)
                {
                    Console.WriteLine("Test7");
                    playerWon = false;
                }
                if(Board[posRow, posColumn-1].Position.IsPlayable)
                {
                    Console.WriteLine("Test8");
                    playerWon = false;
                }
                else
                {
                    playerWon = true;
                }
            }
            return playerWon;
        }
    }
}