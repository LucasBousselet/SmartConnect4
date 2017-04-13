using System.Windows.Controls;

namespace Connect4
{
    /// <summary>
    /// The game is composed of a number of cells that contain either a yellow token, a red one or nothing
    /// </summary>
    public partial class Cell : UserControl
    {
        /// <summary>
        /// True if the cell is occupied by a red token
        /// </summary>
        private bool m_IsRed = new bool();

        /// <summary>
        /// True if the cell is occupied by a yellow token
        /// </summary>
        private bool m_IsYellow = new bool();

        /// <summary>
        /// True if the cell unoccupied 
        /// </summary>
        private bool m_IsEmpty = new bool();

        public Cell()
        {
            InitializeComponent();
            m_IsEmpty = true;
            m_IsRed = false;
            m_IsYellow = false;
        }

        #region Getters / Setters

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
            }
        }

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
            }
        }

        public bool IsEmpty
        {
            get
            {
                return m_IsEmpty;
            }
            set
            {
                m_IsEmpty = value;
                if (value)
                {
                    m_IsRed = !value;
                    m_IsYellow = !value;
                }
            }
        }

        #endregion  
    }
}
