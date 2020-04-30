namespace Felli
{
    /// <summary>
    /// Class for state in board's position
    /// </summary>
    public class State
    {
        public bool PlayableCheck { get; set; }

        public State(bool playableCheck)
        {
            PlayableCheck = playableCheck;
        }

    }
}