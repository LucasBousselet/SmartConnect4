using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Connect4
{
    class Connect4Game
    {
        private Connect4Player m_Player1;
        private Connect4Player m_Player2;
        private Connect4Player m_Winner;
        private GameGrid m_MatrixOfCells = new GameGrid();


        public Connect4Player PlayerYellow
        {
            get
            {
                return m_Player1;
            }
        }

        public Connect4Player PlayerRed
        {
            get
            {
                return m_Player2;
            }
        }

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
            m_Player2 = new AIPlayer("Red", m_Player1, 2);
            /*
            m_Player1.Play(m_MatrixOfCells, 2);
            m_Player1.Play(m_MatrixOfCells, 3);
            m_Player1.Play(m_MatrixOfCells, 4);
            */
        }

        public void Connect4GameLoop(int p_ColumnIndex)
        {
            m_Player1.Play(MatrixOfCells, p_ColumnIndex);
            OnHumanPlayerPlayed();
            m_Player2.Play(MatrixOfCells);
        }

        public delegate void dlgOnHumanPlayerPlayed();
        public static dlgOnHumanPlayerPlayed OnHumanPlayerPlayed;

        /*
        public void Connect4GameLoop()
        {
            while (!MatrixOfCells.FourTokenAligned)
            {
                m_Player1.HasPlayed = false;
                m_Player2.HasPlayed = false;

                if (m_Player1 is HumanPlayer)
                {
                    OnHumanPlayer(true);
                }
                while (m_Player1.HasPlayed == false)
                {
                }

                if (m_Player2 is HumanPlayer)
                {
                    OnHumanPlayer(true);
                }
                while (m_Player2.HasPlayed == false)
                {
                }

            }
            MessageBox.Show("Player " + m_Winner.TokenColor + " won the game, how fun !");
            Connect4Player player1 = new HumanPlayer("Yellow");
            player1.Play(m_MatrixOfCells, p_ColumnIndex);
            // UpdateGUI();

            // System.Threading.Thread.Sleep(2000);

            // Connect4Player player2 = new AIPlayer("Red");
            // player2.Play(m_MatrixOfCells, 0);
        }
        */
    }
}
