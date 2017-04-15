using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Node
    {
        String m_WhoseTurnItIs = String.Empty;

        GameGrid m_Grid;

        int m_Depth = -1;

        public Node(String p_PlayerColor, GameGrid p_Grid, int m_depth)
        {
            m_WhoseTurnItIs = p_PlayerColor;
            m_Grid = p_Grid;
            m_Depth = m_depth;
        }
    }
}
