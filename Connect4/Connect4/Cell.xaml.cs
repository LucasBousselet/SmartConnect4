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
    /// The game is composed of a number of cells that contain either a yellow token, a red one or nothing
    /// </summary>
    public partial class Cell : UserControl
    {
        /// <summary>
        /// True if the cell is occupied by a red token
        /// </summary>
        private bool isRed = new bool();

        /// <summary>
        /// True if the cell is occupied by a yellow token
        /// </summary>
        private bool isYellow = new bool();

        /// <summary>
        /// True if the cell unoccupied 
        /// </summary>
        private bool isEmpty = new bool();

        public Cell()
        {
            InitializeComponent();
            isEmpty = true;
            isRed = false;
            isYellow = false;
        }

        #region Getters / Setters

        public bool IsRed
        {
            get
            {
                return isRed;
            }
            set
            {
                isRed = value;
                if (value == true)
                {
                    isEmpty = false;
                }
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
                if (value == true)
                {
                    isEmpty = false;
                }
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
                if (value == true)
                {
                    isRed = false;
                    isYellow = false;
                }
            }
        }

        #endregion  
    }
}
