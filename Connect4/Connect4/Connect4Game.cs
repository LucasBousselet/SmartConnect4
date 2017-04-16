using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    partial class Connect4Game
    {
        private Connect4Player m_Player1;
        private Connect4Player m_Player2;

        private GameGrid m_MatrixOfCells = new GameGrid();

       // ColumnButton.OnButtonClicked += new ColumnButton.dlgOnButtonClicked(OnColumnButtonClicked);
        

        public GameGrid MatrixOfCells
        {
            get
            {
                return m_MatrixOfCells;
            }
        }

        public Connect4Game()
        {
            Connect4Player player1 = new HumanPlayer("Yellow");
            Connect4Player player2 = new AIPlayer("Red");
        }

        /// <summary>
        /// Event triggerend when we click on a ColumnButton.
        /// </summary>
        /// <param name="p_ColumnIndex"> The column index used to locate the ColumnButton. </param>
        public void OnColumnButtonClicked(int p_ColumnIndex)
        {
            // MessageBox.Show(p_ColumnIndex.ToString());
            // ColumnButtonEnabled(false);
            Connect4GameLoop(p_ColumnIndex);
        }

        private void Connect4GameLoop(int p_ColumnIndex)
        {
            Connect4Player player1 = new HumanPlayer("Yellow");
            player1.Play(m_MatrixOfCells, p_ColumnIndex);
            //     UpdateGUI();

            //   System.Threading.Thread.Sleep(2000);

            //   Connect4Player player2 = new AIPlayer("Red");
            //   player2.Play(m_MatrixOfCells, 0);
        }
    }
}
