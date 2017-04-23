using System;

namespace Connect4
{
    static class MinimaxAlgorithm
    {
        public static Node Minimax(Node p_Node, int p_Depth, int p_Alpha, int p_Beta, Connect4Player p_PlayerMaximisant, Connect4Player p_PlayerMinimisant)
        {
            if (p_Node.Grid.FourTokenAligned || p_Node.Depth == p_Depth)
            {
                p_Node.Grid.CalculateGridScore(p_PlayerMaximisant);
                return p_Node;
            }

            else
            {
                if (p_Node.WhoseTurnItIs == p_PlayerMaximisant)
                {
                    Node nodeWithScoreMax = null;

                    foreach (int column in p_Node.Grid.ColumnNotFull)
                    {
                        GameGrid newGameGrid = p_Node.Grid.CloneGameGrid(p_Node.Grid);
                        newGameGrid.AddTokenInColumn(column, p_PlayerMaximisant.TokenColor);
                        Node newNode = new Node(p_PlayerMinimisant, newGameGrid, column, p_Node.Depth + 1);
                        Node currentNode = Minimax(newNode, p_Depth, p_Alpha, p_Beta, p_PlayerMaximisant, p_PlayerMinimisant);

                        if (nodeWithScoreMax == null)
                        {
                            nodeWithScoreMax = currentNode;
                        }
                        else
                        {
                            nodeWithScoreMax = currentNode.Grid.Score > nodeWithScoreMax.Grid.Score ? currentNode : nodeWithScoreMax;
                            /*if (nodeWithScoreMax.Grid.Score > p_Beta)
                            {
                                return nodeWithScoreMax;
                            }
                            p_Alpha = Math.Max(p_Alpha, nodeWithScoreMax.Grid.Score);*/
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
                        newGameGrid.AddTokenInColumn(column, p_PlayerMinimisant.TokenColor);
                        Node newNode = new Node(p_PlayerMaximisant, newGameGrid, column, p_Node.Depth + 1);
                        Node currentNode = Minimax(newNode, p_Depth, p_Alpha, p_Beta, p_PlayerMaximisant, p_PlayerMinimisant);

                        if (nodeWithScoreMin == null)
                        {
                            nodeWithScoreMin = currentNode;
                        }
                        else
                        {
                            nodeWithScoreMin = currentNode.Grid.Score > nodeWithScoreMin.Grid.Score ? currentNode : nodeWithScoreMin;

                            int a = 0;
                            a++;

                            /*if (p_Alpha > nodeWithScoreMin.Grid.Score)
                            {
                                return nodeWithScoreMin;
                            }
                            p_Beta = Math.Min(p_Beta, nodeWithScoreMin.Grid.Score);*/
                        }
                    }
                    return nodeWithScoreMin;
                }
            }
        }
    }
}