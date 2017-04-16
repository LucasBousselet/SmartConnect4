namespace Connect4
{
    static class MinimaxAlgorithm
    {
        public static Node Minimax(Node p_Node, int p_MaxDepth, Connect4Player p_Player, Connect4Player p_Opponent)
        {
            if (p_Node.Grid.FourTokenAligned || p_Node.Depth == p_MaxDepth)
            {
                p_Node.Grid.CalculateGridScore(p_Player);
                return p_Node;
            }

            else
            {
                if (p_Node.WhoseTurnItIs == p_Player)
                {
                    Node nodeWithScoreMax = null;

                    foreach (int column in p_Node.Grid.ColumnNotFull)
                    {
                        GameGrid newGameGrid = p_Node.Grid.CloneGameGrid(p_Node.Grid);
                        newGameGrid.AddTokenInColumn(column, p_Player.TokenColor);
                        Node newNode = new Node(p_Opponent, newGameGrid, column, p_Node.Depth + 1);
                        Node currentNode = Minimax(newNode, p_MaxDepth, p_Player, p_Opponent);

                        if (nodeWithScoreMax == null)
                        {
                            nodeWithScoreMax = currentNode;
                        }
                        else
                        {
                            nodeWithScoreMax = currentNode.Grid.Score > nodeWithScoreMax.Grid.Score ? currentNode : nodeWithScoreMax;
                        }
                    }
                    return nodeWithScoreMax;
                }

                else
                {
                    Node nodeWithScoreMin = null;

                    foreach (int column in p_Node.Grid.ColumnNotFull)
                    {
                        GameGrid newGameGrid = p_Node.Grid.CloneGameGrid(p_Node.Grid);
                        newGameGrid.AddTokenInColumn(column, p_Opponent.TokenColor);
                        Node newNode = new Node(p_Player, newGameGrid, column, p_Node.Depth + 1);
                        Node currentNode = Minimax(newNode, p_MaxDepth, p_Player, p_Opponent);

                        if (nodeWithScoreMin == null)
                        {
                            nodeWithScoreMin = currentNode;
                        }
                        else
                        {
                            nodeWithScoreMin = currentNode.Grid.Score > nodeWithScoreMin.Grid.Score ? currentNode : nodeWithScoreMin;
                        }
                    }
                    return nodeWithScoreMin;
                }
            }
        }
    }
}
