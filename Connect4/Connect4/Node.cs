namespace Connect4
{
    class Node
    {
        /// <summary>
        /// Player that will play the next token
        /// </summary>
        Connect4Player m_WhoseTurnItIs = null;

        /// <summary>
        /// Current state of the game stored in the node
        /// </summary>
        GameGrid m_Grid;

        /// <summary>
        /// Depth of the node, the greater the number, the deeper in the tree
        /// </summary>
        int m_Depth = -1;

        /// <summary>
        /// Column played since the last game state
        /// </summary>
        int m_TokenAddedInColumn = -1;

        public Connect4Player WhoseTurnItIs
        {
            get
            {
                return m_WhoseTurnItIs;
            }
        }

        public GameGrid Grid
        {
            get
            {
                return m_Grid;
            }
        }

        public int Depth
        {
            get
            {
                return m_Depth;
            }
        }

        public Node(Connect4Player p_WhoseTurnItIs, GameGrid p_Grid, int p_ColumnPlayed, int m_depth)
        {
            m_WhoseTurnItIs = p_WhoseTurnItIs;
            m_Grid = p_Grid;
            m_TokenAddedInColumn = p_ColumnPlayed;
            m_Depth = m_depth;
        }
    }
}
