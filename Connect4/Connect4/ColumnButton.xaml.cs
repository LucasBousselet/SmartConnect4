using System.Windows;
using System.Windows.Controls;

namespace Connect4
{
    /// <summary>
    /// Interaction logic for ColumnButton.xaml
    /// </summary>
    partial class ColumnButton : UserControl
    {
        private int m_ColumnIndex = new int();

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

        public delegate void dlgOnButtonClicked(int p_ColumnIndex);
        public static dlgOnButtonClicked OnButtonClicked;

        public ColumnButton()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OnButtonClicked(m_ColumnIndex);
        }
    }
}
