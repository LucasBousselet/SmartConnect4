using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Connect4
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Graphical grid that act as the connect4 structure, that will be displayed on the GUI.
        /// </summary>
        private Grid m_Connect4GUI = new Grid();

        /// <summary>
        /// Main GUI, it is split in two, on the left is the connect 4 grid game,
        /// and on the right is information about the game (whose turn it is, etc.).
        /// </summary>
        private Grid m_WindowGrid = new Grid();

        /// <summary>
        /// The MainWindow possesses this matrix of 42 cells, it is used to reach every
        /// cell we need, and each one is associated in a case of the GUI grid for display purpose.
        /// </summary>
        private Connect4Game m_Connect4Game = new Connect4Game();

        /// In order to select a column in which we should insert a token, this list contains
        /// 7 buttons, one will be displayed above
        /// <summary> the each column.
        /// </summary>
        private List<ColumnButton> ColumnButtonList = new List<ColumnButton>();

        /// <summary>
        /// Create a new GameWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeGameWindow();

            PopulateConnect4WithColumnButtons();
            PopulateConnect4GridWithCell();
            UpdateGUI();

            // Delegate for ColumnButton.onClick event.
            GameGrid.OnColumnFull += new GameGrid.dlgOnColumnFull(OnColumnFull);
            ColumnButton.OnButtonClicked += new ColumnButton.dlgOnButtonClicked(OnColumnButtonClicked);
        }

        /// <summary>
        /// Initializes a Grid (GameWindow) that will contain two columns and one row :
        /// 1st column : the Connect 4 grid + Column buttons;
        /// 2nd column : information about the current game.
        /// </summary>
        private void InitializeGameWindow()
        {
            // Creation of the rows and columns of the _WindowGrid.
            RowDefinition windowRow1 = new RowDefinition();
            windowRow1.Height = GridLength.Auto;
            m_WindowGrid.RowDefinitions.Add(windowRow1);
            ColumnDefinition windowColumn1 = new ColumnDefinition();
            ColumnDefinition WindowColumn2 = new ColumnDefinition();
            m_WindowGrid.ColumnDefinitions.Add(windowColumn1);
            m_WindowGrid.ColumnDefinitions.Add(WindowColumn2);

            InitializeConnect4Grid();
        }

        /// <summary>
        /// Initialize the graphical grid that will contain the 42 game cells and the 7 ColumnButtons.
        /// </summary>
        private void InitializeConnect4Grid()
        {
            // Creates the 7 rows of the connect 4, and adds them to the grid.
            // The first row will hold the 7 buttons used to play a token in a given column.
            RowDefinition connect4Row1 = new RowDefinition();
            RowDefinition connect4Row2 = new RowDefinition();
            RowDefinition connect4Row3 = new RowDefinition();
            RowDefinition connect4Row4 = new RowDefinition();
            RowDefinition connect4Row5 = new RowDefinition();
            RowDefinition connect4Row6 = new RowDefinition();
            RowDefinition connect4Row7 = new RowDefinition();
            m_Connect4GUI.RowDefinitions.Add(connect4Row1);
            m_Connect4GUI.RowDefinitions.Add(connect4Row2);
            m_Connect4GUI.RowDefinitions.Add(connect4Row3);
            m_Connect4GUI.RowDefinitions.Add(connect4Row4);
            m_Connect4GUI.RowDefinitions.Add(connect4Row5);
            m_Connect4GUI.RowDefinitions.Add(connect4Row6);
            m_Connect4GUI.RowDefinitions.Add(connect4Row7);
            // Creates the 7 columns of the connect 4, and adds them to the grid.
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

            // Sets Connect4 grid into the left _GameGrid cell. 
            Grid.SetRow(m_Connect4GUI, 1);
            Grid.SetColumn(m_Connect4GUI, 0);
            m_WindowGrid.Children.Add(m_Connect4GUI);
        }

        /// <summary>
        /// Populates every cell of the Connect4 with a instance of Cell.
        /// Every cell is placed in a list, with a number ranging from 0 to 8   0 (first row 0 -> 8, second row 9 -> 17, etc.).
        /// </summary>
        private void PopulateConnect4GridWithCell()
        {
            try
            {
                // We start at i = 1 because we leave one empty row to place our buttons.
                /*  for (int i = m_MatrixOfCells.NumberOfLines; i >= 1; i--)
                  {
                      for (int j = 0; j < m_MatrixOfCells.NumberOfColumns; j++)
                      {
                          // Adds a Cell in the Connect4 Grid(i,j).
                             Grid.SetRow(m_MatrixOfCells.ArrayOfCells[i - 1, j], i);
                             Grid.SetColumn(m_MatrixOfCells.ArrayOfCells[i - 1, j], j);
                             m_Connect4GUI.Children.Add(m_MatrixOfCells.ArrayOfCells[i - 1, j]);*/
                for (int i = 1; i <= m_Connect4Game.MatrixOfCells.NumberOfLines; i++)
                {
                    for (int j = 0; j < m_Connect4Game.MatrixOfCells.NumberOfColumns; j++)
                    {
                        Grid.SetRow(m_Connect4Game.MatrixOfCells.ArrayOfCells[i - 1, j], m_Connect4Game.MatrixOfCells.NumberOfLines - i + 1);
                        Grid.SetColumn(m_Connect4Game.MatrixOfCells.ArrayOfCells[i - 1, j], j);
                        m_Connect4GUI.Children.Add(m_Connect4Game.MatrixOfCells.ArrayOfCells[i - 1, j]);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Populates every cell of the 1st row with an instance of ColumnButton.
        /// </summary>
        private void PopulateConnect4WithColumnButtons()
        {
            try
            {
                for (int i = 0; i < m_Connect4Game.MatrixOfCells.NumberOfColumns; i++)
                {
                    ColumnButton button = new ColumnButton();
                    button.ColumnIndex = i;
                    ColumnButtonList.Add(button);

                    button.HorizontalAlignment = HorizontalAlignment.Center;
                    // Adds a ColumnButton in the row n°i of the Connect4 Grid.
                    Grid.SetRow(button, 0);
                    Grid.SetColumn(button, i);
                    m_Connect4GUI.Children.Add(button);
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
                throw;
            }
        }

        private void UpdateGUI()
        {
            Content = m_WindowGrid;
        }

        /// <summary>
        /// Method used to enabe ou disable a ColumnButton.
        /// </summary>
        /// <param name="p_State"> The state of the button : true = enabled, false = disabled. </param>
        private void ColumnButtonEnabled(bool p_State)
        {
            foreach (ColumnButton ColumnButton in ColumnButtonList)
            {
                ColumnButton.IsEnabled = p_State;
            }
        }

        #region Events

        /// <summary>
        /// Event triggerend when we click on a ColumnButton.
        /// </summary>
        /// <param name="p_ColumnIndex"> The column index used to locate the ColumnButton. </param>
        public void OnColumnButtonClicked(int p_ColumnIndex)
        {
            // MessageBox.Show(p_ColumnIndex.ToString());
            ColumnButtonEnabled(false);
            m_Connect4Game.Connect4GameLoop(p_ColumnIndex);
            ColumnButtonEnabled(true);
        }

        /// <summary>
        /// Event triggered when a column is full to avoid anyone to play in it.
        /// </summary>
        /// <param name="p_ColumnIndex"> The index of the full column. </param>
        public void OnColumnFull(int p_ColumnIndex)
        {
            for (int i = 0; i < ColumnButtonList.Count; i++)
            {
                if (ColumnButtonList[i].ColumnIndex == p_ColumnIndex)
                {
                    ColumnButtonList[i].IsEnabled = false;
                    ColumnButtonList.Remove(ColumnButtonList[i]);
                }
            }
        }

        #endregion
    }
}
