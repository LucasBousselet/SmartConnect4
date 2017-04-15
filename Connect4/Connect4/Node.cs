namespace Connect4
{
    class Node
    {
        Connect4Player m_WhoseTurnItIs = null;

        GameGrid m_Grid;

        int m_Depth = -1;

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

        public Node(Connect4Player p_WhoseTurnItIs, GameGrid p_Grid, int m_depth)
        {
            m_WhoseTurnItIs = p_WhoseTurnItIs;
            m_Grid = p_Grid;
            m_Depth = m_depth;
        }
    }
}
