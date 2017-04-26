using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Connect4
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml.
    /// </summary>
    public sealed partial class MainWindow : Window
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
        /// The textbox to display the score calculated by the AI.
        /// </summary>
        private TextBox m_ScoreTextBox = new TextBox();

        /// <summary>
        /// The MainWindow possesses this matrix of 42 cells, it is used to reach every
        /// cell we need, and each one is associated in a case of the GUI grid for display purpose.
        /// </summary>
        private Connect4Game m_Connect4Game = new Connect4Game();

        /// In order to select a column in which we should insert a token, this list contains
        /// 7 buttons, one will be displayed above
        /// <summary> the each column.
        /// </summary>
        private List<ColumnButton> m_ColumnButtonList = new List<ColumnButton>();

        /// <summary>
        /// Create a new GameWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Initialize playing board.
            InitializeGameWindow();

            // Delegate for varios events.
            GameGrid.OnColumnFull += new GameGrid.dlgOnColumnFull(OnColumnFull);
            ColumnButton.OnColumnButtonClicked += new ColumnButton.dlgOnButtonClicked(OnColumnButtonClicked);
            Connect4Game.OnHumanPlayerPlayed += new Connect4Game.dlgOnHumanPlayerPlayed(UpdateGUI);
            Connect4Game.OnWin += new Connect4Game.dlgOnWin(OnWin);
            Connect4Game.OnScoreCalculated += new Connect4Game.dlgOnScoreCalculated(ModifyScoreTextBox);
            
            Content = m_WindowGrid;
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
            RowDefinition windowRow2 = new RowDefinition();
            windowRow1.Height = GridLength.Auto;
            windowRow2.Height = new GridLength(50);
            m_WindowGrid.RowDefinitions.Add(windowRow1);
            m_WindowGrid.RowDefinitions.Add(windowRow2);
            ColumnDefinition windowColumn1 = new ColumnDefinition();
            m_WindowGrid.ColumnDefinitions.Add(windowColumn1);

            InitializeConnect4Board();
            FillConnect4Board();

            AddScoreField();
        }

        /// <summary>
        /// Initialize the graphical grid that will contain the 42 game cells and the 7 ColumnButtons.
        /// </summary>
        private void InitializeConnect4Board()
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
            Grid.SetRow(m_Connect4GUI, 0);
            Grid.SetColumn(m_Connect4GUI, 0);
            m_WindowGrid.Children.Add(m_Connect4GUI);
        }

        private void FillConnect4Board()
        {
            m_ColumnButtonList.Clear();
            ResetScoreBox();
            // Populate the first line with ColumnButtons
            PopulateConnect4WithColumnButtons();
            // Populate the grid cell with Cells
            PopulateConnect4GridWithCell();
        }

        /// <summary>
        /// Add the m_ScoreBox to the GUI.
        /// </summary>
        private void AddScoreField()
        {
            Grid.SetRow(m_ScoreTextBox, 1);
            Grid.SetColumn(m_ScoreTextBox, 0);
            m_WindowGrid.Children.Add(m_ScoreTextBox);
        }

        /// <summary>
        /// Reset the m_ScoreBox text.
        /// </summary>
        private void ResetScoreBox()
        {
            m_ScoreTextBox.Text = "\n  Current grid score for AI : 0";
        }

        /// <summary>
        /// Populates every cell of the grid with a cell from the MatrixOfCells.
        /// Every cell is placed in a list, with a number ranging from 0 to 8   0 (first row 0 -> 8, second row 9 -> 17, etc.).
        /// </summary>
        private void PopulateConnect4GridWithCell()
        {
            try
            {
                for (int i = 1; i <= m_Connect4Game.Gamegrid.NumberOfLines; i++)
                {
                    for (int j = 0; j < m_Connect4Game.Gamegrid.NumberOfColumns; j++)
                    {
                        Grid.SetRow(m_Connect4Game.Gamegrid.MatriceOfCells[i - 1, j].CellUI, m_Connect4Game.Gamegrid.NumberOfLines - i + 1);
                        Grid.SetColumn(m_Connect4Game.Gamegrid.MatriceOfCells[i - 1, j].CellUI, j);
                        m_Connect4GUI.Children.Add(m_Connect4Game.Gamegrid.MatriceOfCells[i - 1, j].CellUI);
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
                for (int i = 0; i < m_Connect4Game.Gamegrid.NumberOfColumns; i++)
                {
                    ColumnButton button = new ColumnButton();
                    button.ColumnIndex = i;
                    m_ColumnButtonList.Add(button);

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

        /// <summary>
        /// Method used to enabe ou disable a ColumnButton.
        /// </summary>
        /// <param name="p_State"> The state of the button : true = enabled, false = disabled. </param>
        private void ColumnButtonEnabled(bool p_State)
        {
            foreach (ColumnButton ColumnButton in m_ColumnButtonList)
            {
                ColumnButton.IsEnabled = p_State;
            }
        }

        #region Events

        /// <summary>
        /// Event triggered when a column is full to avoid anyone to play in it.
        /// </summary>
        /// <param name="p_ColumnIndex"> The index of the full column. </param>
        private void OnColumnFull(int p_ColumnIndex)
        {
            for (int i = 0; i < m_ColumnButtonList.Count; i++)
            {
                if (m_ColumnButtonList[i].ColumnIndex == p_ColumnIndex)
                {
                    m_ColumnButtonList[i].IsEnabled = false;
                    m_ColumnButtonList.Remove(m_ColumnButtonList[i]);
                }
            }
        }

        /// <summary>
        /// Event triggerend when we click on a ColumnButton.
        /// </summary>
        /// <param name="p_ColumnIndex"> The column index used to locate the ColumnButton. </param>
        private void OnColumnButtonClicked(int p_ColumnIndex)
        {
            ColumnButtonEnabled(false);
            m_Connect4Game.Connect4GameLoop(p_ColumnIndex);
            ColumnButtonEnabled(true);
        }

        /// <summary>
        /// Wait for the GUI to update before having the AI play.
        /// </summary>
        private void UpdateGUI()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => { })).Wait();
        }

        /// <summary>
        /// Display a textbox when a player wins.
        /// </summary>
        /// <param name="p_Player"> The winner. </param>
        private void OnWin(Connect4Player p_Player)
        {
            if (p_Player == null)
            {
                MessageBox.Show("Looks like we have a draw gentlemen !");
            }
            else
            {
                if (p_Player is HumanPlayer)
                {
                    MessageBox.Show("You won this game, congratulations !");
                }
                else
                {
                    MessageBox.Show("Seems like you lost this game, want to retry ?");
                }
            }

            m_Connect4Game = new Connect4Game();
            FillConnect4Board();
        }

        /// <summary>
        /// Update the m_ScoreBox text.
        /// </summary>
        /// <param name="p_Score"> The new score for the grid. </param>
        /// <param name="p_Time"> The execution time. </param>
        /// <param name="p_IterationNumber"> The number of iterations. </param>
        private void ModifyScoreTextBox(int p_Score, string p_Time, string p_IterationNumber)
        {
            m_ScoreTextBox.Text = "\n  Current grid score for AI : " + p_Score.ToString() + " / Calculation time : " + p_Time + " for " + p_IterationNumber + " iterations";
        }

        #endregion
    }
}