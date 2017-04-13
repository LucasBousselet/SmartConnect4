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
    /// Logique d'interaction pour Cell.xaml
    /// </summary>
    public partial class Cell : UserControl
    {
        private bool isRed = new bool();

        private bool isYellow = new bool();

        private bool isEmpty = new bool();

        public Cell()
        {
            InitializeComponent();
            isEmpty = true;
            isRed = false;
            isYellow = false;
        }

        public bool IsRed
        {
            get
            {
                return isRed;
            }
            set
            {
                isRed = value;
            }
        }

        public bool IsYellow
        {
            get
            {
                return isYellow;
            }
            set
            {
                isYellow = value;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return isEmpty;
            }
            set
            {
                isEmpty = value;
                isRed = false;
                isYellow = false;
            }
        }
    }
}
