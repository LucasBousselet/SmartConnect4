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
        }

        public void Connect4GameLoop(int p_ColumnIndex)
        {
            m_Player1.Play(MatrixOfCells, p_ColumnIndex);
            OnHumanPlayerPlayed();
            m_Player2.Play(MatrixOfCells);
        }

        public delegate void dlgOnHumanPlayerPlayed();
        public static dlgOnHumanPlayerPlayed OnHumanPlayerPlayed;

        }
    }