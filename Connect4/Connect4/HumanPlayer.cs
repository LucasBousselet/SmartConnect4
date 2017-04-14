namespace Connect4
{
    /// <summary>
    /// Class used to create a new human player.
    /// </summary>
    class HumanPlayer : Connect4Player
    {
        /// <summary>
        /// Create a new human player with his token color.
        /// </summary>
        /// <param name="p_TokenColor"> The color of the token for the player. </param>
        public HumanPlayer(string p_TokenColor) : base(p_TokenColor)
        {
            m_TokenColor = p_TokenColor;
        }
    }
}
