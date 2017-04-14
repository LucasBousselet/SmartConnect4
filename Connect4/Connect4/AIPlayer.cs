namespace Connect4
{
    class AIPlayer : Connect4Player
    {

        public override void Play(GameGrid p_GameGrid, int p_ColumnPlayed)
        {
            p_GameGrid.ArrayOfCells[p_GameGrid.GetNextPossibleLine(p_ColumnPlayed), p_ColumnPlayed].IsRed = true;
        }

    }
}
