namespace Connect4
{
    class Connect4Player
    {
        protected string m_TokenColor = string.Empty;
        protected bool m_HasPlayed = new bool();

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

        public Connect4Player(string p_TokenColor)
        {
            m_TokenColor = p_TokenColor;
        }

        public void Play(GameGrid p_GameGrid, int p_ColumnPlayed)
        {
            p_GameGrid.AddTockenInColumn(p_ColumnPlayed, m_TokenColor);
        }
    }
}
