using System.Diagnostics;
using System.Linq;

namespace Connect4
{
    /// <summary>
    /// Create a new Connect4Game which embodies the rules ans player interactions.
    /// </summary>
    sealed class Connect4Game
    {
        /// <summary>
        /// The first player.
        /// </summary>
        private Connect4Player m_Player1;

        /// <summary>
        /// The second player.
        /// </summary>
        private Connect4Player m_Player2;

        /// <summary>
        /// The game board.
        /// </summary>
        private GameGrid m_GameGrid = new GameGrid();

        /// <summary>
        /// Get the board game.
        /// </summary>
        public GameGrid Gamegrid
        {
            get
            {
                return m_GameGrid;
            }
        }

        /// <summary>
        /// Create a new Connect4Game.
        /// </summary>
        public Connect4Game()
        {
            m_Player1 = new HumanPlayer("Yellow");
            m_Player2 = new AIPlayer("Red", m_Player1, 6);
            m_GameGrid.FillGameGridWithEmptyCells();
        }

        /// <summary>
        /// Delegate thrown when a player p_Winner wins.
        /// </summary>
        /// <param name="p_Winner"></param>
        public delegate void dlgOnWin(Connect4Player p_Winner);
        /// <summary>
        /// The function throwing the delegate.
        /// </summary>
        public static dlgOnWin OnWin;

        /// <summary>
        /// Delegate thrown when we calculate the score
        /// </summary>
        /// <param name="p_Score"> The score. </param>
        /// <param name="p_Time"> Minimax duration. </param>
        /// <param name="p_Iteration"> Minimax iteration. </param>
        public delegate void dlgOnScoreCalculated(int p_Score, string p_Time, int p_Iteration);
        /// <summary>
        /// The function throwing the delegate.
        /// </summary>
        public static dlgOnScoreCalculated OnScoreCalculated;

        /// <summary>
        /// Delegate trown when the first player played to wait for GUI synch.
        /// </summary>
        public delegate void dlgOnHumanPlayerPlayed();
        /// <summary>
        /// The function throwing the delegate.
        /// </summary>
        public static dlgOnHumanPlayerPlayed OnHumanPlayerPlayed;

        /// <summary>
        /// Start a play sequence i.e. human player plays and the AI player plays if human didn't win.
        /// </summary>
        /// <param name="p_ColumnIndex"> The column index the human player played in. </param>
        public void Connect4GameLoop(int p_ColumnIndex)
        {
            m_Player1.Play(m_GameGrid, p_ColumnIndex);
            OnHumanPlayerPlayed();
            m_GameGrid.CalculateGridScore(m_Player1);

            if (!CheckIfWinner(m_Player1))
            {
                m_Player2.Play(m_GameGrid);
                
                m_GameGrid.CalculateGridScore(m_Player2);
                OnScoreCalculated(m_GameGrid.Score, MinimaxAlgorithm.ElapsedTime, MinimaxAlgorithm.IterationNumber);
                CheckIfWinner(m_Player2);
            }
        }

        /// <summary>
        /// Check if a given player has won.
        /// </summary>
        /// <param name="p_PlayerToConsider"> The player to consider. </param>
        /// <returns></returns>
        private bool CheckIfWinner(Connect4Player p_PlayerToConsider)
        {
            if (m_GameGrid.FourTokenAligned)
            {
                OnWin(p_PlayerToConsider);
                return true;
            }
            if (!m_GameGrid.ColumnNotFull.Any())
            {
                OnWin(null);
            }
            return false;
        }
    }
}