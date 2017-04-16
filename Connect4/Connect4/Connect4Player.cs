namespace Connect4
{
    /// <summary>
    /// Create a new Connect4Player, either an AI player or a human player.
    /// </summary>
    class Connect4Player
    {
        /// <summary>
        /// Give the player a token color.
        /// </summary>
        protected string m_TokenColor = string.Empty;
        /// <summary>
        /// True if the player has played is turn, false otherwise.
        /// </summary>
        protected bool m_HasPlayed = new bool();

        /// <summary>
        /// Get the token color for the player.
        /// </summary>
        public string TokenColor
        {
            get
            {
                return m_TokenColor;
            }
        }
    
        /// <summary>
        /// Get / set if the Connect4Player has played is turn.
        /// </summary>
        public bool HasPlayed
        {
            get
            {
                return m_HasPlayed;
            }
            set
            {
                m_HasPlayed = value;
            }
        }

        /// <summary>
        /// Create a new Connect4Player with its token color.
        /// </summary>
        /// <param name="p_TokenColor"> The token color for the player. </param>
        public Connect4Player(string p_TokenColor)
        {
            m_TokenColor = p_TokenColor;
        }

        /// <summary>
        /// The method used fo a player to add a token in a given column.
        /// </summary>
        /// <param name="p_GameGrid"> The board he plays in. </param>
        /// <param name="p_ColumnPlayed"> The column he plays in. </param>
        public void Play(GameGrid p_GameGrid, int p_ColumnPlayed)
        {
            p_GameGrid.AddTokenInColumn(p_ColumnPlayed, m_TokenColor);
            HasPlayed = true;
        }
    }
}
