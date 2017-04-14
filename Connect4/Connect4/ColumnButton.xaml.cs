using System.Windows;
using System.Windows.Controls;

namespace Connect4
{
    /// <summary>
    /// Interaction logic for ColumnButton.xaml
    /// </summary>
    partial class ColumnButton : UserControl
    {
        /// <summary>
        /// Column index of the ColumnButton to know its location.
        /// </summary>
        private int m_ColumnIndex = new int();

        /// <summary>
        /// Get / set the column index.
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return m_ColumnIndex;
            }
            set
            {
                m_ColumnIndex = value;
            }
        }

        /// <summary>
        /// Delegate used to detect a button clicked in the MainWindow.
        /// </summary>
        /// <param name="p_ColumnIndex"></param>
        public delegate void dlgOnButtonClicked(int p_ColumnIndex);
        /// <summary>
        /// Function that throws the delegate.
        /// </summary>
        public static dlgOnButtonClicked OnButtonClicked;

        /// <summary>
        /// Create a new ColumnButton.
        /// </summary>
        public ColumnButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event thrown when the ColumnButton is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            OnButtonClicked(m_ColumnIndex);
        }
    }
}
