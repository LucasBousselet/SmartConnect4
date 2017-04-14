using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Connect4
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Grid m_Connect4GUI = new Grid();

        private Grid m_WindowGrid = new Grid();

        private GameGrid m_MatrixOfCells = new GameGrid();

        private List<ColumnButton> ColumnButtonList = new List<ColumnButton>();

        public MainWindow()
        {
            InitializeComponent();

            InitializeGameWindow();

            Content = m_WindowGrid;

            AddColumnButtonToList();

            // Delegate for ColumnButton.onClick event.
            ColumnButton.OnButtonClicked += new ColumnButton.dlgOnButtonClicked(OnButtonClicked);

            HumanPlayer.OnColumnFull += new HumanPlayer.dlgOnColumnFull(OnColumnFull);
            AIPlayer.OnColumnFull += new AIPlayer.dlgOnColumnFull(OnColumnFull);
        }

        public void OnButtonClicked(int p_ColumnIndex)
        {
            MessageBox.Show(p_ColumnIndex.ToString());
            ColumnButtonEnabled(false);
        }

        public void OnColumnFull(int p_ColumnIndex)
        {
            ColumnButtonList[p_ColumnIndex].IsEnabled = false;
            ColumnButtonList.RemoveAt(p_ColumnIndex);
        }

        /// <summary>
        /// Initializes a Grid (GameWindow) that will contain two columns and one row :
        /// 1st column : the Connect 4 grid 
        /// 2nd column : information about the current game
        /// </summary>
        private void InitializeGameWindow()
        {
            // Creation of the rows and columns of the _WindowGrid
            RowDefinition windowRow1 = new RowDefinition();
            RowDefinition windowRow2 = new RowDefinition();
            windowRow1.Height = GridLength.Auto;
            windowRow2.Height = GridLength.Auto;
            m_WindowGrid.RowDefinitions.Add(windowRow1);
            ColumnDefinition windowColumn1 = new ColumnDefinition();
            ColumnDefinition WindowColumn2 = new ColumnDefinition();
            m_WindowGrid.ColumnDefinitions.Add(windowColumn1);
            m_WindowGrid.ColumnDefinitions.Add(WindowColumn2);

            InitializeConnect4Grid();

        }

        private void InitializeConnect4Grid()
        {
            // Creates the 6 rows of the connect 4, and adds them to the grid
            RowDefinition connect4Row1 = new RowDefinition();
            RowDefinition connect4Row2 = new RowDefinition();
            RowDefinition connect4Row3 = new RowDefinition();
            RowDefinition connect4Row4 = new RowDefinition();
            RowDefinition connect4Row5 = new RowDefinition();
            RowDefinition connect4Row6 = new RowDefinition();
            m_Connect4GUI.RowDefinitions.Add(connect4Row1);
            m_Connect4GUI.RowDefinitions.Add(connect4Row2);
            m_Connect4GUI.RowDefinitions.Add(connect4Row3);
            m_Connect4GUI.RowDefinitions.Add(connect4Row4);
            m_Connect4GUI.RowDefinitions.Add(connect4Row5);
            m_Connect4GUI.RowDefinitions.Add(connect4Row6);
            // Creates the 7 columns of the connect 4, and adds them to the grid
            ColumnDefinition connect4Column1 = new ColumnDefinition();
            ColumnDefinition connect4Column2 = new ColumnDefinition();
            ColumnDefinition connect4Column3 = new ColumnDefinition();
            ColumnDefinition connect4Column4 = new ColumnDefinition();
            ColumnDefinition connect4Column5 = new ColumnDefinition();
            ColumnDefinition connect4Column6 = new ColumnDefinition();
            ColumnDefinition connect4Column7 = new ColumnDefinition();
            m_Connect4GUI.ColumnDefinitions.Add(connect4Column1);
            m_Connect4GUI.ColumnDefinitions.Add(connect4Column2);
            m_Connect4GUI.ColumnDefinitions.Add(connect4Column3);
            m_Connect4GUI.ColumnDefinitions.Add(connect4Column4);
            m_Connect4GUI.ColumnDefinitions.Add(connect4Column5);
            m_Connect4GUI.ColumnDefinitions.Add(connect4Column6);
            m_Connect4GUI.ColumnDefinitions.Add(connect4Column7);

            // Sets Connect4 grid into the left _GameGrid cell 
            Grid.SetRow(m_Connect4GUI, 1);
            Grid.SetColumn(m_Connect4GUI, 0);
            m_WindowGrid.Children.Add(m_Connect4GUI);
        }
        private void AddColumnButtonToList()
        {
            
         //   ColumnButtonList.Add(m_ColumnButton0);
            /*
            ColumnButtonList.Add(m_ColumnButton1);
            ColumnButtonList.Add(m_ColumnButton2);
            ColumnButtonList.Add(m_ColumnButton3);
            ColumnButtonList.Add(m_ColumnButton4);
            ColumnButtonList.Add(m_ColumnButton5);
            ColumnButtonList.Add(m_ColumnButton6);
            */

            for (int i = 0; i < ColumnButtonList.Count; i++)
            {
                ColumnButtonList[i].ColumnIndex = i;
            }
        }

        private void ColumnButtonEnabled(bool p_State)
        {
            foreach (ColumnButton ColumnButton in ColumnButtonList)
            {
                ColumnButton.IsEnabled = p_State;
            }
        }

            /// <summary>
            /// Populates every cell of the Connect4 with a instance of Cell
            /// Every cell is placed in a list, with a number ranging from 0 to 8   0 (first row 0 -> 8, second row 9 -> 17, etc ...)
            /// </summary>
            private void PopulateConnect4GridWithCell()
            {
                try
                {
                for (int i = 0; i < m_MatrixOfCells.NumberOfLines; i++)
                {
                    for (int j = 0; j < m_MatrixOfCells.NumberOfColumns; j++)
                    {
                        // Adds a Cell in the Connect4 Grid(i,j)
                        Grid.SetRow(m_MatrixOfCells.ArrayOfCells[i, j], i);
                        Grid.SetColumn(m_MatrixOfCells.ArrayOfCells[i, j], j);
                        m_Connect4GUI.Children.Add(m_MatrixOfCells.ArrayOfCells[i, j]);
                    }
                }
                }
                catch (Exception ex)
                {
                }
            }
    }
}
