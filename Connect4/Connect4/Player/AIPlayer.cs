namespace Connect4
{
    /// <summary>
    /// Class used to create an AI player.
    /// </summary>
    sealed class AIPlayer : Connect4Player
    {
        /// <summary>
        /// The maximum depth for the minimax algorithm.
        /// </summary>
        private int m_MaxDepth = new int();

        /// <summary>
        /// The opponent of the AI player.
        /// </summary>
        private Connect4Player m_Opponent = null;

        /// <summary>
        /// Create a new AI player with his token color, his opponent and his minimax depth.
        /// </summary>
        /// <param name="p_TokenColor"> The color of the AIPlayer's token. </param>
        /// <param name="p_Opponent"> The opponent a.k.a. minimizing player. </param>
        /// <param name="p_DepthMinimax"> The depth for minimax. </param>
        public AIPlayer(string p_TokenColor, Connect4Player p_Opponent, int p_DepthMinimax) : base(p_TokenColor)
        {
            m_Opponent = p_Opponent;
            m_TokenColor = p_TokenColor;
            m_MaxDepth = p_DepthMinimax;
        }

        /// <summary>
        /// Run the Minimax algorithm with alpha-beta prunning to determine in which column we play.
        /// </summary>
        /// <param name="p_GameGrid"> The grid to consider. </param>
        /// <returns> The calculation details (time and number of iterations) </returns>
        public override string[] Play(GameGrid p_GameGrid)
        {
            Play(p_GameGrid, MinimaxAlgorithm.GetInWhichColumnPlay(p_GameGrid, m_MaxDepth, this, m_Opponent));
            return new string[2] { MinimaxAlgorithm.ElapsedTime, MinimaxAlgorithm.IterationNumber.ToString() };
        }

    }
}