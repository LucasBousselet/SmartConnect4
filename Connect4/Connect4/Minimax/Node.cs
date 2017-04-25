namespace Connect4
{
    sealed class Node
    {
        /// <summary>
        /// Player that will play the next token
        /// </summary>
        private Connect4Player m_WhoseTurnItIs = null;

        /// <summary>
        /// Current state of the game stored in the node
        /// </summary>
        private GameGrid m_Grid = null;

        /// <summary>
        /// Depth of the node, the greater the number, the deeper in the tree
        /// </summary>
        private int m_Depth = new int();

        /// <summary>
        /// Column played since the last game state
        /// </summary>
        private int m_TokenAddedInColumn = new int();

        /// <summary>
        /// Create a new node.
        /// </summary>
        /// <param name="p_WhoseTurnItIs"> Whose turn it is to play. </param>
        /// <param name="p_Grid"> The current grid. </param>
        /// <param name="p_ColumnPlayed"> In which column we played to reach this node. </param>
        /// <param name="m_depth"> The depth of the node. </param>
        public Node(Connect4Player p_WhoseTurnItIs, GameGrid p_Grid, int p_ColumnPlayed, int m_depth)
        {
            m_WhoseTurnItIs = p_WhoseTurnItIs;
            m_Grid = p_Grid;
            m_TokenAddedInColumn = p_ColumnPlayed;
            m_Depth = m_depth;
        }

        /// <summary>
        /// Get whose turn it is to play.
        /// </summary>
        public Connect4Player WhoseTurnItIs
        {
            get
            {
                return m_WhoseTurnItIs;
            }
        }

        /// <summary>
        /// Get the grid of the node.
        /// </summary>
        public GameGrid Grid
        {
            get
            {
                return m_Grid;
            }
        }

        /// <summary>
        /// Get the depth of the node.
        /// </summary>
        public int Depth
        {
            get
            {
                return m_Depth;
            }
        }

        /// <summary>
        /// Get the column we played in to reach this node.
        /// </summary>
        public int TokenAddedInColumn
        {
            get
            {
                return m_TokenAddedInColumn;
            }
        }
    }
}