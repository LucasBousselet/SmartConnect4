using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class HumanPlayer
    {
        private bool m_HasPlayed = new bool();

        public bool HasPlayed
        {
            get
            {
                return m_HasPlayed;
            }
            set
            {
                m_HasPlayed = value;
            }
        }

        public delegate void dlgOnColumnFull(int p_ColumnIndex);
        public static dlgOnColumnFull OnColumnFull;

        public void Play(GameGrid p_GameGrid, int p_ColumnPlayed)
        {
            int LinePlayed = p_GameGrid.GetNextPossibleLine(p_ColumnPlayed);

            if(LinePlayed == p_GameGrid.NumberOfLines - 1)
            {
                OnColumnFull(p_ColumnPlayed);
            }
        }
    }
}
