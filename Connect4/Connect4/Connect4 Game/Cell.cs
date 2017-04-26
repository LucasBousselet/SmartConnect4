namespace Connect4
{
    /// <summary>
    /// Create a new Connect4 cell.
    /// </summary>
    sealed class Cell
    {
        /// <summary>
        /// True if the cell is occupied by a red token.
        /// </summary>
        private bool m_IsRed = new bool();

        /// <summary>
        /// True if the cell is occupied by a yellow token.
        /// </summary>
        private bool m_IsYellow = new bool();

        /// <summary>
        /// True if the cell unoccupied. 
        /// </summary>
        private bool m_IsEmpty = new bool();

        /// <summary>
        /// Link a new CellUI.
        /// </summary>
        private CellUI m_CellUI = null;

        /// <summary>
        /// Get the CellUI.
        /// </summary>
        public CellUI CellUI
        {
            get
            {
                return m_CellUI;
            }
        }

        /// <summary>
        /// Create a new cell with its CellUI.
        /// </summary>
        public Cell()
        {
            m_IsEmpty = true;
            m_IsRed = false;
            m_IsYellow = false;
            m_CellUI = new CellUI();
        }

        /// <summary>
        /// Constructor used to create the duplicate of a given cell.
        /// </summary>
        /// <param name="p_ClonedCell"> The cell to duplicate. </param>
        public Cell(Cell p_ClonedCell)
        {
            m_IsEmpty = p_ClonedCell.m_IsEmpty;
            m_IsRed = p_ClonedCell.m_IsRed;
            m_IsYellow = p_ClonedCell.m_IsYellow;
        }

        /// <summary>
        /// Get / set if the cell contains a red token.
        /// </summary>
        public bool IsRed
        {
            get
            {
                return m_IsRed;
            }
            set
            {
                m_IsRed = value;
                m_IsEmpty = !value;
                if (m_CellUI != null)
                {
                    m_CellUI.ChangeImage("Red");
                }
            }
        }

        /// <summary>
        /// Get / set if the cell contains a yellow token.
        /// </summary>
        public bool IsYellow
        {
            get
            {
                return m_IsYellow;
            }
            set
            {
                m_IsYellow = value;
                m_IsEmpty = !value;
                if (m_CellUI != null)
                {
                    m_CellUI.ChangeImage("Yellow");
                }
            }
        }

        /// <summary>
        /// Get if the cell is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return m_IsEmpty;
            }
        }

    }
}