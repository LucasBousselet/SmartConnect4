using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Connect4Game
    {
        private Connect4Player m_Player1;
        private Connect4Player m_Player2;

        private GameGrid m_MatrixOfCells = new GameGrid();

        public GameGrid MatrixOfCells
        {
            get
            {
                return m_MatrixOfCells;
            }
        }

        public Connect4Game()
        {
            m_Player1 = new HumanPlayer("Yellow");
            m_Player2 = new AIPlayer("Red", m_Player1, 4);
        }

        public void Connect4GameLoop(int p_ColumnIndex)
        {
            m_Player1.Play(m_MatrixOfCells, p_ColumnIndex);
            // UpdateGUI();

            // System.Threading.Thread.Sleep(2000);

            // Connect4Player player2 = new AIPlayer("Red");
            // player2.Play(m_MatrixOfCells, 0);
        }
    }
}
