using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Connect4
{
    /// <summary>
    /// The game grid is the support of the game, the structure that holds the colored tokens, 
    /// it has the list of the cells composing the board, the score associated and an information whether 
    /// the game is won or not (4 tokens aligned).
    /// </summary>
    sealed class GameGrid
    {
        /// <summary>
        /// Number of lines in the Connect4 game board.
        /// </summary>
        private int m_NumberOfColumns = new int();

        /// <summary>
        /// Number of columns in the Connect4 game board.
        /// </summary>
        private int m_NumberOfLines = new int();

        /// <summary>
        /// Contains the 42 Cells (6 x 7) that compose the board.
        /// </summary>
        private Cell[,] m_MatrixOfCells = null;

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
        private List<int> m_ColumnNotFull = new List<int>();

        /// <summary>
        /// Create a new Gamegrid with 42 empty cells.
        /// </summary>
        public GameGrid(int p_NumberOfLines, int p_NumberOfColumns)
        {
            m_NumberOfLines = p_NumberOfLines;
            m_NumberOfColumns = p_NumberOfColumns;
            m_Score = 0;
            m_FourTokensAligned = false;
            m_MatrixOfCells = new Cell[m_NumberOfLines, m_NumberOfColumns];
        }

        /// <summary>
        /// Fill the m_MatrixOfCells with empty Cells.
        /// </summary>
        public void FillGameGridWithEmptyCells()
        {
            m_ColumnNotFull = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };

            for (int i = 0; i < m_NumberOfLines; i++)
            {
                for (int j = 0; j < m_NumberOfColumns; j++)
                {
                    m_MatrixOfCells[i, j] = new Cell();
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
                return m_NumberOfColumns;
            }
        }

        /// <summary>
        /// Get the matrix which composes the game board.
        /// </summary>
        public Cell[,] MatriceOfCells
        {
            get
            {
                return m_MatrixOfCells;
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
            set
            {
                m_Score = value;
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

        /// <summary>
        /// Get the list of columns not full.
        /// </summary>
        public List<int> ColumnNotFull
        {
            get
            {
                return m_ColumnNotFull;
            }
        }

        #endregion

        /// <summary>
        /// Delegate thrown when a column is full.
        /// </summary>
        /// <param name="p_ColumnIndex"> The column which is full. </param>
        public delegate void dlgOnColumnFull(int p_ColumnIndex);
        /// <summary>
        /// The function throwing the delegate.
        /// </summary>
        public static dlgOnColumnFull OnColumnFull;

        /// <summary>
        /// Play a token in a given column and test if it's full.
        /// </summary>
        /// <param name="p_ColumnPlayed"> The column to play a token in. </param>
        /// <param name="p_TokenColor"> The color of the token. </param>
        public void PlayTokenInColumn(int p_ColumnPlayed, string p_TokenColor)
        {
            int line = AddTokenInColumn(p_ColumnPlayed, p_TokenColor);

            if (line == m_NumberOfLines - 1)
            {
                OnColumnFull(p_ColumnPlayed);
            }
        }

        /// <summary>
        /// Add a token in the next free cell in a column.
        /// </summary>
        /// <param name="p_ColumnPlayed"> The column to consider. </param>
        /// <param name="p_TokenColor"> The color of the token to add. </param>
        public int AddTokenInColumn(int p_ColumnPlayed, string p_TokenColor)
        {
            int nextPossibleLine = GetNextPossibleLine(p_ColumnPlayed);

            if (p_TokenColor.Equals("Red"))
            {
                MatriceOfCells[nextPossibleLine, p_ColumnPlayed].IsRed = true;
            }
            else
            {
                if (p_TokenColor.Equals("Yellow"))
                {
                    MatriceOfCells[nextPossibleLine, p_ColumnPlayed].IsYellow = true;
                }
            }
            if (nextPossibleLine == m_NumberOfLines - 1)
            {
                m_ColumnNotFull.Remove(p_ColumnPlayed);
            }

            return nextPossibleLine;
        }

        /// <summary>
        /// Get the next free cell in a given column.
        /// </summary>
        /// <param name="p_ColumnPlayed"> The column to consider. </param>
        /// <returns> The next free cell in a given column. </returns>
        private int GetNextPossibleLine(int p_ColumnPlayed)
        {
            for (int i = 0; i < m_NumberOfLines; i++)
            {
                if (m_MatrixOfCells[i, p_ColumnPlayed].IsEmpty)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Create a duplicate of a given board.
        /// </summary>
        /// <param name="p_GridToClone"> The board to clone. </param>
        /// <returns> The duplicate of the given board. </returns>
        public GameGrid CloneGameGrid(GameGrid p_GridToClone)
        {
            GameGrid ClonedGrid = new GameGrid(p_GridToClone.m_NumberOfLines, p_GridToClone.m_NumberOfColumns);

            for (int i = 0; i < m_NumberOfLines; i++)
            {
                for (int j = 0; j < m_NumberOfColumns; j++)
                {
                    ClonedGrid.m_MatrixOfCells[i, j] = new Cell(p_GridToClone.m_MatrixOfCells[i, j]);
                }
            }

            for (int i = 0; i < p_GridToClone.ColumnNotFull.Count; i++)
            {
                ClonedGrid.m_ColumnNotFull.Add(p_GridToClone.ColumnNotFull[i]);
            }

            return ClonedGrid;
        }

        /// <summary>
        /// Calculate the score for a given player.
        /// </summary>
        /// <param name="p_tokenColor"> The token color to consider. </param>
        public void CalculateGridScore(string p_tokenColor)
        {
            m_Score = CalculateLinesScore(p_tokenColor) +
                CalculateColumnsScore(p_tokenColor) +
                CalculateUpperRightDiagonalsScore(p_tokenColor) +
                CalculateUpperLeftDiagonalsScore(p_tokenColor);
        }

        /// <summary>
        /// We calculate the score according to the following rules :
        /// - if there are both yellow and red tokens in the line, it's not a winning line, we give a score of 0.
        /// - if there are no tocken in the line, it's not a winning line, we give a score of 0.
        /// - if there are only "p_ColorToConsider" tokens, we might be able to win in this line,
        ///   we give 1 point for 1 token in the line, 10 points for 2 tokens, 100 points for 3 tokens
        ///   and 10000 points for 4 tokens.
        /// - if there are only the other color, we might loose in this line, we give -1 point for 1 token in the line,
        ///   -10 points for 2 tokens, -100 points for 3 tokens and -10000 points for 4 tokens.
        /// </summary>
        /// <param name="p_ColorToConsider"> The color to consider. </param>
        /// <param name="p_RedCount"> The red token count in the bloc. </param>
        /// <param name="p_YellowCount"> The yellow token count in the bloc. </param>
        /// <returns></returns>
        private int CalculateBlocScore(string p_ColorToConsider, int p_RedCount, int p_YellowCount)
        {
            int result = 0;

            if (((p_RedCount > 0) && (p_YellowCount > 0)) || ((p_RedCount == 0) && (p_YellowCount == 0)))
            {
                result = 0;
            }
            else
            {
                if (p_ColorToConsider.Equals("Red"))
                {
                    if (p_RedCount == 4)
                    {
                        result = 10000;
                        m_FourTokensAligned = true;
                    }
                    else
                    {
                        if (p_YellowCount == 4)
                        {
                            result = -100000;
                            m_FourTokensAligned = true;
                        }
                        else
                        {
                            result = p_RedCount > 0 ? (int)Math.Pow(10, p_RedCount - 1) : -1 * (int)Math.Pow(10, p_YellowCount - 1);
                        }
                    }
                }
                else if (p_ColorToConsider.Equals("Yellow"))
                {
                    if (p_RedCount == 4)
                    {
                        result = -100000;
                        m_FourTokensAligned = true;
                    }
                    else
                    {
                        if (p_YellowCount == 4)
                        {
                            result = 10000;
                            m_FourTokensAligned = true;
                        }
                        else
                        {
                            result = p_YellowCount > 0 ? (int)Math.Pow(10, p_YellowCount - 1) : -1 * (int)Math.Pow(10, p_RedCount - 1);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Calculate the score for the board lines as follow :
        /// Each given line is divided in 4 sets of adjacent cells : [cell0, cell1, cell2, cell3],
        /// [cell1, cell2, cell3, cell4], [cell2, cell3, cell4, cell5], [cell3, cell4, cell5, cell6]
        /// and we count the number of yellow and red token if the blocs.
        /// </summary>
        /// <returns> The score for the board lines, computed as explained above. </returns>
        private int CalculateLinesScore(string p_ColorToConsider)
        {
            int linesScore = 0;

            Parallel.For(0, m_MatrixOfCells.GetLength(0), i =>
            //for (int i = 0; i < m_ArrayOfCells.GetLength(0); i++)
            {
                for (int j = 0; j < m_MatrixOfCells.GetLength(1) - 3; j++)
                {
                    int redCount = 0;
                    int yellowCount = 0;

                    for (int k = 0; k < 4; k++)
                    {
                        if (m_MatrixOfCells[i, j + k].IsRed)
                        {
                            redCount++;
                        }
                        if (m_MatrixOfCells[i, j + k].IsYellow)
                        {
                            yellowCount++;
                        }
                    }

                    linesScore += CalculateBlocScore(p_ColorToConsider, redCount, yellowCount);
                }
            });

            return linesScore;
        }

        /// <summary>
        /// Compute the score for the board columns like CalculateLinesScore() does for lines.
        /// </summary>
        /// <returns> The score for the board columns. </returns>
        private int CalculateColumnsScore(string p_ColorToConsider)
        {
            int columnsScore = 0;

            Parallel.For(0, m_MatrixOfCells.GetLength(1), i =>
            //for (int i = 0; i < m_ArrayOfCells.GetLength(1); i++)
            {
                for (int j = 0; j < m_MatrixOfCells.GetLength(0) - 3; j++)
                {
                    int redCount = 0;
                    int yellowCount = 0;

                    for (int k = 0; k < 4; k++)
                    {
                        if (m_MatrixOfCells[j + k, i].IsRed)
                        {
                            redCount++;
                        }
                        if (m_MatrixOfCells[j + k, i].IsYellow)
                        {
                            yellowCount++;
                        }
                    }

                    columnsScore += CalculateBlocScore(p_ColorToConsider, redCount, yellowCount);
                }
            });

            return columnsScore;
        }

        /// <summary>
        /// Compute the score for the board upper right diagonals like CalculateLinesScore() does for lines.
        /// </summary>
        /// <returns> The score for the board upper right diagonals. </returns>
        private int CalculateUpperRightDiagonalsScore(string p_ColorToConsider)
        {
            int upperRightDiagonalsScore = 0;

            Parallel.For(0, m_MatrixOfCells.GetLength(0) - 3, i =>
            //for (int i = 0; i < m_ArrayOfCells.GetLength(0) - 3; i++)
            {
                for (int j = 0; j < m_MatrixOfCells.GetLength(1) - 3; j++)
                {
                    int redCount = 0;
                    int yellowCount = 0;

                    for (int k = 0; k < 4; k++)
                    {
                        if (m_MatrixOfCells[i + k, j + k].IsRed)
                        {
                            redCount++;
                        }
                        if (m_MatrixOfCells[i + k, j + k].IsYellow)
                        {
                            yellowCount++;
                        }
                    }

                    upperRightDiagonalsScore += CalculateBlocScore(p_ColorToConsider, redCount, yellowCount);
                }
            });

            return upperRightDiagonalsScore;
        }

        /// <summary>
        /// Compute the score for the board upper left diagonals like CalculateLinesScore() does for lines.
        /// </summary>
        /// <returns> The score for the board upper left diagonals. </returns>
        private int CalculateUpperLeftDiagonalsScore(string p_ColorToConsider)
        {
            int upperLeftDiagonalsScore = 0;

            Parallel.For(3, m_MatrixOfCells.GetLength(0), i =>
            //for (int i = 3; i < m_ArrayOfCells.GetLength(0); i++)
            {
                for (int j = 0; j < m_MatrixOfCells.GetLength(1) - 3; j++)
                {
                    int redCount = 0;
                    int yellowCount = 0;

                    for (int k = 0; k < 4; k++)
                    {
                        if (m_MatrixOfCells[i - k, j + k].IsRed)
                        {
                            redCount++;
                        }
                        if (m_MatrixOfCells[i - k, j + k].IsYellow)
                        {
                            yellowCount++;
                        }
                    }

                    upperLeftDiagonalsScore += CalculateBlocScore(p_ColorToConsider, redCount, yellowCount);
                }
            });

            return upperLeftDiagonalsScore;
        }
    }
}