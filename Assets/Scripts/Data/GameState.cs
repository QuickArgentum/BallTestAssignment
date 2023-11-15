namespace Data
{
    /// <summary>
    /// The game uses a simple finite state machine to track it's progress and run its components accordingly.
    /// This enum describes all the possible states of the game
    /// </summary>
    public enum GameState
    {
        None,
        
        /// <summary>
        /// Game has just started and fade in animation is playing
        /// </summary>
        FadingIn,
        
        /// <summary>
        /// Game is currently in progress and accepting player input
        /// </summary>
        Playing,
        
        /// <summary>
        /// Player has won the game, their input is no longer accepted and the victory animation is playing
        /// </summary>
        WinAnimationShowing,
        
        /// <summary>
        /// Game over screen (either win or loss version) is showing its appearance animation
        /// </summary>
        GameOverScreenShowing,
        
        /// <summary>
        /// Game over screen is visible and the game awaits the player to tap the screen
        /// </summary>
        GameOver,
        
        /// <summary>
        /// Fade out animation is playing and the game will be restarted once it's over
        /// </summary>
        FadingOut
    }
}