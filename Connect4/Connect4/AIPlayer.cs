﻿namespace Connect4
{
    class AIPlayer
    {
        public delegate void dlgOnColumnFull(int p_ColumnIndex);
        public static dlgOnColumnFull OnColumnFull;

        public void Play(GameGrid p_GameGrid, int p_ColumnPlayed)
        {

            int LinePlayed = p_GameGrid.GetNextPossibleLine(p_ColumnPlayed);

            if (LinePlayed == p_GameGrid.NumberOfLines - 1)
            {
                OnColumnFull(p_ColumnPlayed);
            }

        }

    }
}
