using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Connect4
{
    /// <summary>
    /// The game is composed of a number of cells that contain either a yellow token, a red one or nothing.
    /// </summary>
    partial class Cell : UserControl
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
        /// Default constructor for a cell.
        /// </summary>
        public Cell()
        {
            InitializeComponent();
            m_IsEmpty = true;
            m_IsRed = false;
            m_IsYellow = false;
        }

        /// <summary>
        /// Constructor used to create the duplicate of a given cell.
        /// </summary>
        /// <param name="ClonedCell"> The cell to duplicate. </param>
        public Cell(Cell ClonedCell)
        {
            InitializeComponent();
            m_IsEmpty = ClonedCell.m_IsEmpty;
            m_IsRed = ClonedCell.m_IsRed;
            m_IsYellow = ClonedCell.m_IsYellow;
        }

        #region Getters / Setters

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
                BitmapImage image = new BitmapImage(new Uri("Ressources/RedCell.png", UriKind.Relative));
                cellImage.Source = image;
                m_IsEmpty = !value;
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
                BitmapImage image = new BitmapImage(new Uri("Ressources/YellowCell.png", UriKind.Relative));
                cellImage.Source = image;
                m_IsEmpty = !value;
            }
        }

        /// <summary>
        /// Get / set if the cell is empty.
        /// </summary>
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
                    BitmapImage image = new BitmapImage(new Uri("Ressources/EmptyCell.png", UriKind.Relative));
                    cellImage.Source = image;
                    m_IsYellow = !value;
                }
            }
        }

        #endregion  
    }
}
