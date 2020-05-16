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
        /// <param name="args">Number of arguments the program takes</param>
        static void Main(string[] args)
        {
            Game gameplay = new Game();
            gameplay.Run();
        }
    }
}
