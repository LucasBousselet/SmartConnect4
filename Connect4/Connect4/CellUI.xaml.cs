using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Connect4
{
    /// <summary>
    /// The UI Image associated with the case state.
    /// </summary>
    partial class CellUI : UserControl
    {
        /// <summary>
        /// The bitmap image to display.
        /// </summary>
        BitmapImage m_Image;

        /// <summary>
        /// Create a new CellUI with the image of an empty cell.
        /// </summary>
        public CellUI()
        {
            InitializeComponent();
            ChangeImage("");
        }

        /// <summary>
        /// Changes the image displayed.
        /// </summary>
        /// <param name="p_color"> The new CellUI color. </param>
        public void ChangeImage(string p_color)
        {
            switch (p_color)
            {
                case "Red":
                    m_Image = new BitmapImage(new Uri("Ressources/RedCell.png", UriKind.Relative));
                    break;
                case "Yellow":
                    m_Image = new BitmapImage(new Uri("Ressources/YellowCell.png", UriKind.Relative));
                    break;
                default:
                    m_Image = new BitmapImage(new Uri("Ressources/EmptyCell.png", UriKind.Relative));
                    break;

            }
            m_CellImage.Source = m_Image;
        }
    }
}