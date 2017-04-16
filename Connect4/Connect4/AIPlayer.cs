namespace Connect4
{
    /// <summary>
    /// Class used to create an AI player.
    /// </summary>
    class AIPlayer : Connect4Player
    {
        private int m_DepthMinimax = new int();

        private Connect4Player m_Opponent;

        /// <summary>
        /// Create a new AI player with his token color.
        /// </summary>
        /// <param name="p_TokenColor"> The color of the token for the player. </param>
        public AIPlayer(string p_TokenColor, Connect4Player p_Opponent, int p_DepthMinimax) : base(p_TokenColor)
        {
            m_Opponent = p_Opponent;
            m_TokenColor = p_TokenColor;
            m_DepthMinimax = p_DepthMinimax;
        }

        public override void Play(GameGrid p_GameGrid, int p_ColumnPlayed)
        {
            Node startingNode = new Node(this, p_GameGrid, m_DepthMinimax);
            Node nodeToPlay = MinimaxAlgorithm.Minimax(startingNode, m_DepthMinimax, this, m_Opponent);

            base.Play(p_GameGrid, p_ColumnPlayed);
        }

    }
}
