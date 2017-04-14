namespace Connect4
{
    class HumanPlayer
    {
        private bool m_HasPlayed = new bool();

        public bool HasPlayed
        {
            get
            {
                return m_HasPlayed;
            }
            set
            {
                m_HasPlayed = value;
            }
        }

        public void Play(GameGrid p_GameGrid, int p_ColumnPlayed)
        {
            int LinePlayed = p_GameGrid.GetNextPossibleLine(p_ColumnPlayed);

            p_GameGrid.ArrayOfCells[LinePlayed, p_ColumnPlayed].IsYellow = true;

            if (LinePlayed == p_GameGrid.NumberOfLines - 1)
            {
                
            }
        }
    }
}
