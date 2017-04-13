using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    /// <summary>
    /// The game grid is the support of the game, the structure that holds the colored tokens, 
    /// it has the list of the cells played so far, the score associated and an information whether 
    /// the game is won or not (4 tokens aligned)
    /// </summary>
    class GameGrid
    {
        /// <summary>
        /// Contains the 42 cells that compose the game
        /// </summary>
        private List<Cell> _listOfCell = new List<Cell>();

        /// <summary>
        /// The score associated with the grid, used by the minimax algorithm
        /// </summary>
        private int _score = new int();

        /// <summary>
        /// Indicates whether the game is finished as 4 tokens are aligned
        /// </summary>
        private bool _fourTokenAligned = new bool();

        public GameGrid()
        {
            _listOfCell = null;
            _score = 0;
            _fourTokenAligned = false;
        }

        #region Getters / Setters

        public List<Cell> GetCells
        {
            get
            {
                return _listOfCell;
            }
            set
            {
                _listOfCell = value;
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        public bool FourTokenAligned
        {
            get
            {
                return _fourTokenAligned;
            }
            set
            {
                _fourTokenAligned = value;
            }
        }

        #endregion
    }
}
