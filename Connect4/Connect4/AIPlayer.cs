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

        public void Play(GameGrid p_GameGrid)
        {
            Node startingNode = new Node(this, p_GameGrid, -1, 0);
            Node nodeToPlay = MinimaxAlgorithm.Minimax(startingNode, m_DepthMinimax, this, m_Opponent);

            Play(p_GameGrid, nodeToPlay.TokenAddedInColumn);
        }

    }
}
