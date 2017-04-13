using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Connect4
{
    /// <summary>
    /// Interaction logic for ColumnButton.xaml
    /// </summary>
    public partial class ColumnButton : UserControl
    {
        private int m_ColumnIndex = -1;

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
