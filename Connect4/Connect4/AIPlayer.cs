namespace Connect4
{
    class AIPlayer : Connect4Player
    {
        /// <summary>
        /// Create a new AI player with his token color.
        /// </summary>
        /// <param name="p_TokenColor"> The color of the token for the player. </param>
        public AIPlayer(string p_TokenColor) : base(p_TokenColor)
        {
            m_TokenColor = p_TokenColor;
        }

    }
}
