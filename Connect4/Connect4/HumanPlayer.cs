namespace Connect4
{
    class HumanPlayer : Connect4Player
    {
        private bool m_HasPlayed = new bool();

        bool HasPlayed
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

        public override void Play(GameGrid p_GameGrid, int p_ColumnPlayed)
        {

        }
    }
}
