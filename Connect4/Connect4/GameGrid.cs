using System;
using System.Collections.Generic;

namespace Connect4
{
    /// <summary>
    /// The game grid is the support of the game, the structure that holds the colored tokens, 
    /// it has the list of the cells played so far, the score associated and an information whether 
    /// the game is won or not (4 tokens aligned).
    /// </summary>
    class GameGrid
    {
        /// <summary>
        /// Number of lines in the Connect4 game board.
        /// </summary>
        private int m_NomberOfColumns = 7;

        /// <summary>
        /// Number of columns in the Connect4 game board.
        /// </summary>
        private int m_NumberOfLines = 6;

        /// <summary>
        /// Contains the 42 cells (6 x 7) that compose the board.
        /// </summary>
        private Cell[,] m_ArrayOfCells = null;

        /// <summary>
        /// The score associated with the grid, used by the minimax algorithm.
        /// </summary>
        private int m_Score = new int();

        /// <summary>
        /// Indicates whether the game is finished as 4 tokens are aligned.
        /// </summary>
        private bool m_FourTokensAligned = new bool();

        /// <summary>
        /// A list containing the index of the full columns.
        /// </summary>
        private List<int> m_ColumnFullIndex = new List<int>();

        /// <summary>
        /// Create a new Gamegrid with 42 empty cells.
        /// </summary>
        public GameGrid()
        {
            m_Score = 0;
            m_FourTokensAligned = false;
            m_ArrayOfCells = new Cell[m_NumberOfLines, m_NomberOfColumns];

            for (int i = 0; i < m_NumberOfLines; i++)
            {
                for (int j = 0; j < m_NomberOfColumns; j++)
                {
                    m_ArrayOfCells[i, j] = new Cell();
                }
            }
        }

        #region Getters / Setters

        /// <summary>
        /// Get the number of lines of the board.
        /// </summary>
        public int NumberOfLines
        {
            get
            {
                return m_NumberOfLines;
            }
        }

        /// <summary>
        /// Get the number fo columns of the board.
        /// </summary>
        public int NumberOfColumns
        {
            get
            {
                return m_NomberOfColumns;
            }
        }

        /// <summary>
        /// Get the matrix which composes the game board.
        /// </summary>
        public Cell[,] ArrayOfCells
        {
            get
            {
                return m_ArrayOfCells;
            }
        }

        /// <summary>
        /// Get the grid score.
        /// </summary>
        public int Score
        {
            get
            {
                return m_Score;
            }
        }

        /// <summary>
        /// Boolean that indicates if there are 4 token aligned in the board.
        /// </summary>
        public bool FourTokenAligned
        {
            get
            {
                return m_FourTokensAligned;
            }
        }

        #endregion

        public delegate void dlgOnColumnFull(int p_ColumnIndex);
        public static dlgOnColumnFull OnColumnFull;

        /// <summary>
        /// Add a token in the next free cell in a column.
        /// </summary>
        /// <param name="p_ColumnPlayed"> The column to consider. </param>
        /// <param name="p_TokenColor"> The color of the token to add. </param>
        public void AddTockenInColumn(int p_ColumnPlayed, string p_TokenColor)
        {
            if (p_TokenColor.Equals("Red"))
            {
                ArrayOfCells[GetNextPossibleLine(p_ColumnPlayed), p_ColumnPlayed].IsRed = true;
            }
            else
            {
                if (p_TokenColor.Equals("Yellow"))
                {
                    ArrayOfCells[GetNextPossibleLine(p_ColumnPlayed), p_ColumnPlayed].IsYellow = true;
                }
            }
        }

        /// <summary>
        /// Get the next free cell in a given column.
        /// </summary>
        /// <param name="p_ColumnPlayed"> The column to consider. </param>
        /// <returns> The next free cell in a given column. </returns>
        public int GetNextPossibleLine(int p_ColumnPlayed)
        {
            for (int i = 0; i < m_NumberOfLines; i++)
            {
                if (m_ArrayOfCells[i, p_ColumnPlayed].IsEmpty)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Create a duplicate of a given board.
        /// </summary>
        /// <param name="GridToClone"> The board to clone. </param>
        /// <returns> The duplicate of the given board. </returns>
        public GameGrid CloneGameGrid(GameGrid GridToClone)
        {
            GameGrid ClonedGrid = new GameGrid();

            for (int i = 0; i < m_NumberOfLines; i++)
            {
                for (int j = 0; j < m_NomberOfColumns; j++)
                {
                    ClonedGrid.m_ArrayOfCells[i, j] = new Cell(GridToClone.m_ArrayOfCells[i, j]);
                }
            }

            return ClonedGrid;
        }

        /// <summary>
        /// Calculate the score of a given board.
        /// </summary>
        /// <returns> The score for a given board. </returns>
        public int CalculateGridScore(Connect4Player p_PlayerToConsider)
        {
            string tokenColor = p_PlayerToConsider.TokenColor;

            return CalculateLinesScore(tokenColor) +
                CalculateColumnsScore(tokenColor) +
                CalculateUpperRightDiagonalsScore(tokenColor) +
                CalculateUpperLeftDiagonalsScore(tokenColor);
        }

        /// <summary>
        /// Calculate the score for the board lines as follow :
        /// Each given line is divided in 4 sets of adjacent cells : [cell0, cell1, cell2, cell3],
        /// [cell1, cell2, cell3, cell4], [cell2, cell3, cell4, cell5], [cell3, cell4, cell5, cell6]
        /// and in each set we calculate the score according to the following rules :
        /// - if there are both yellow and red tokens in the line, it's not a winning line, we give a score of 0.
        /// - if there are no tocken in the line, it's not a winning line, we give a score of 0.
        /// - if there are only "p_ColorToConsider" tokens, we might be able to win in this line,
        ///   we give 1 point for 1 token in the line, 10 points for 2 tokens, 100 points for 3 tokens
        ///   and 1000 points for 4 tokens.
        /// - if there are only the other color, we might loose in this line, we give -1 point for 1 token in the line,
        ///   -10 points for 2 tokens, -100 points for 3 tokens and -1000 points for 4 tokens.
        /// </summary>
        /// <returns> The score for the board lines, computed as explained above. </returns>
        public int CalculateLinesScore(string p_ColorToConsider)
        {
            int linesScore = 0;

            for (int i = 0; i < m_ArrayOfCells.GetLength(0); i++)
            {
                int lineScore = 0;

                for (int j = 0; j < m_ArrayOfCells.GetLength(1) - 3; j++)
                {
                    int redCount = 0;
                    int yellowCount = 0;

                    for (int k = 0; k < 4; k++)
                    {
                        if (m_ArrayOfCells[i, j + k].IsRed)
                        {
                            redCount++;
                        }
                        if (m_ArrayOfCells[i, j + k].IsYellow)
                        {
                            yellowCount++;
                        }
                    }

                    if (((redCount > 0) && (yellowCount > 0)) || ((redCount == 0) && (yellowCount == 0)))
                    {
                        lineScore = 0;
                    }
                    else
                    {
                        if (p_ColorToConsider.Equals("Red"))
                        {
                            lineScore = redCount > 0 ? (int)Math.Pow(10, redCount - 1) : -1 * (int)Math.Pow(10, yellowCount - 1);
                        }
                        else if (p_ColorToConsider.Equals("Yellow"))
                        {
                            lineScore = yellowCount > 0 ? (int)Math.Pow(10, yellowCount - 1) : -1 * (int)Math.Pow(10, redCount - 1);
                        }
                    }

                    if (lineScore == 1000 || lineScore == -1000)
                    {
                        m_FourTokensAligned = true;
                    }
                }

                linesScore += lineScore;
            }

            return linesScore;
        }

        /// <summary>
        /// Compute the score for the board columns like CalculateLinesScore() does for lines.
        /// </summary>
        /// <returns> The score for the board columns. </returns>
        public int CalculateColumnsScore(string p_ColorToConsider)
        {
            int columnsScore = 0;

            for (int i = 0; i < m_ArrayOfCells.GetLength(1); i++)
            {
                int columnScore = 0;

                for (int j = 0; j < m_ArrayOfCells.GetLength(0) - 3; j++)
                {
                    int redCount = 0;
                    int yellowCount = 0;

                    for (int k = 0; k < 4; k++)
                    {
                        if (m_ArrayOfCells[j + k, i].IsRed)
                        {
                            redCount++;
                        }
                        if (m_ArrayOfCells[j + k, i].IsYellow)
                        {
                            yellowCount++;
                        }
                    }

                    if (((redCount > 0) && (yellowCount > 0)) || ((redCount == 0) && (yellowCount == 0)))
                    {
                        columnScore = 0;
                    }
                    else
                    {
                        if (p_ColorToConsider.Equals("Red"))
                        {
                            columnScore = redCount > 0 ? (int)Math.Pow(10, redCount - 1) : -1 * (int)Math.Pow(10, yellowCount - 1);
                        }
                        else if (p_ColorToConsider.Equals("Yellow"))
                        {
                            columnScore = yellowCount > 0 ? (int)Math.Pow(10, yellowCount - 1) : -1 * (int)Math.Pow(10, redCount - 1);
                        }
                    }

                    if (columnScore == 1000 || columnScore == -1000)
                    {
                        m_FourTokensAligned = true;
                    }
                }

                columnsScore += columnScore;
            }

            return columnsScore;
        }

        /// <summary>
        /// Compute the score for the board upper right diagonals like CalculateLinesScore() does for lines.
        /// </summary>
        /// <returns> The score for the board upper right diagonals. </returns>
        public int CalculateUpperRightDiagonalsScore(string p_ColorToConsider)
        {
            int upperRightDiagonalsScore = 0;

            for (int i = 0; i < m_ArrayOfCells.GetLength(0) - 3; i++)
            {
                int upperRightDiagonalScore = 0;

                for (int j = 0; j < m_ArrayOfCells.GetLength(1) - 3; j++)
                {
                    int redCount = 0;
                    int yellowCount = 0;

                    for (int k = 0; k < 4; k++)
                    {
                        if (m_ArrayOfCells[i + k, j + k].IsRed)
                        {
                            redCount++;
                        }
                        if (m_ArrayOfCells[i + k, j + k].IsYellow)
                        {
                            yellowCount++;
                        }
                    }

                    if (((redCount > 0) && (yellowCount > 0)) || ((redCount == 0) && (yellowCount == 0)))
                    {
                        upperRightDiagonalScore = 0;
                    }
                    else
                    {
                        if (p_ColorToConsider.Equals("Red"))
                        {
                            upperRightDiagonalScore = redCount > 0 ? (int)Math.Pow(10, redCount - 1) : -1 * (int)Math.Pow(10, yellowCount - 1);
                        }
                        else if (p_ColorToConsider.Equals("Yellow"))
                        {
                            upperRightDiagonalScore = yellowCount > 0 ? (int)Math.Pow(10, yellowCount - 1) : -1 * (int)Math.Pow(10, redCount - 1);
                        }
                    }

                    if (upperRightDiagonalScore == 1000 || upperRightDiagonalScore == -1000)
                    {
                        m_FourTokensAligned = true;
                    }
                }

                upperRightDiagonalsScore += upperRightDiagonalScore;
            }

            return upperRightDiagonalsScore;
        }

        /// <summary>
        /// Compute the score for the board upper left diagonals like CalculateLinesScore() does for lines.
        /// </summary>
        /// <returns> The score for the board upper left diagonals. </returns>
        public int CalculateUpperLeftDiagonalsScore(string p_ColorToConsider)
        {
            int upperLeftDiagonalsScore = 0;

            for (int i = 3; i < m_ArrayOfCells.GetLength(0); i++)
            {
                int upperLeftDiagonalScore = 0;

                for (int j = 0; j < m_ArrayOfCells.GetLength(1) - 3; j++)
                {
                    int redCount = 0;
                    int yellowCount = 0;

                    for (int k = 0; k < 4; k++)
                    {
                        if (m_ArrayOfCells[i - k, j + k].IsRed)
                        {
                            redCount++;
                        }
                        if (m_ArrayOfCells[i - k, j + k].IsYellow)
                        {
                            yellowCount++;
                        }
                    }

                    if (((redCount > 0) && (yellowCount > 0)) || ((redCount == 0) && (yellowCount == 0)))
                    {
                        upperLeftDiagonalScore = 0;
                    }
                    else
                    {
                        if (p_ColorToConsider.Equals("Red"))
                        {
                            upperLeftDiagonalScore = redCount > 0 ? (int)Math.Pow(10, redCount - 1) : -1 * (int)Math.Pow(10, yellowCount - 1);
                        }
                        else if (p_ColorToConsider.Equals("Yellow"))
                        {
                            upperLeftDiagonalScore = yellowCount > 0 ? (int)Math.Pow(10, yellowCount - 1) : -1 * (int)Math.Pow(10, redCount - 1);
                        }
                    }

                    if (upperLeftDiagonalScore == 1000 || upperLeftDiagonalScore == -1000)
                    {
                        m_FourTokensAligned = true;
                    }
                }

                upperLeftDiagonalsScore += upperLeftDiagonalScore;
            }

            return upperLeftDiagonalsScore;
        }

    }
}
