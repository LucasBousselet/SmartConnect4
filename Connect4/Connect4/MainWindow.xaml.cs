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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            button0.ColumnIndex = 0;
            button1.ColumnIndex = 1;

            /* Architecture Delegate ColumnButton */
            ColumnButton.OnButtonClicked += new ColumnButton.dlgOnButtonClicked(OnButtonClicked);
        }

        public void OnButtonClicked(int p_ColumnIndex)
        {
            MessageBox.Show(p_ColumnIndex.ToString());
        }
    }
}
