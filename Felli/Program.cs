using System;

namespace Felli
{
    /// <summary>
    /// Class for main Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Game gameplay = new Game();
            gameplay.Run();
        }
    }
}
