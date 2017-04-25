using System;

namespace Connect4
{
    /// <summary>
    /// The class Which contains the Minimax algorithm.
    /// </summary>
    static class MinimaxAlgorithm
    {
        /// <summary>
        /// The number of iteration for the minimax algorithm.
        /// </summary>
        private static int m_IterationNumber = new int();

        /// <summary>
        /// Get the number of iteration.
        /// </summary>
        public static int IterationNumber
        {
            get
            {
                return m_IterationNumber;
            }
        }

        /// <summary>
        /// Access point for the Minimax algorithm.
        /// </summary>
        /// <param name="p_GameGrid"> The GameGrid to consider. </param>
        /// <param name="p_MaxDepth"> The maximum depth for Minimax. </param>
        /// <param name="p_MaximizingPlayer"> The maximizing player. </param>
        /// <param name="p_MinimizingPlayer"> The minimizing player. </param>
        /// <returns> The node with the column to play in. </returns>
        public static Node Run(GameGrid p_GameGrid, int p_MaxDepth, Connect4Player p_MaximizingPlayer, Connect4Player p_MinimizingPlayer)
        {
            m_IterationNumber = 0;
            Node startingNode = new Node(p_MaximizingPlayer, p_GameGrid, -1, 0);
            return Minimax(startingNode, p_MaxDepth, int.MinValue, int.MaxValue, p_MaximizingPlayer, p_MinimizingPlayer);
        }

        /// <summary>
        /// Run the minimax algorithm.
        /// </summary>
        /// <param name="p_Node"> The node used to run the algorithm. </param>
        /// <param name="p_MaxDepth"> The maximum depth for the algorithm. </param>
        /// <param name="p_Alpha"> The alpha parameter for the prunning. </param>
        /// <param name="p_Beta"> The beta parameter for the prunning. </param>
        /// <param name="p_MaximizingPlayer"> The maximizing player. </param>
        /// <param name="p_MinimizingPlayer"> The minimizing player. </param>
        /// <returns> The node with the column to play in. </returns>
        private static Node Minimax(Node p_Node, int p_MaxDepth, int p_Alpha, int p_Beta, Connect4Player p_MaximizingPlayer, Connect4Player p_MinimizingPlayer)
        {
            if (p_Node.Grid.FourTokenAligned || p_Node.Depth == p_MaxDepth)
            {
                p_Node.Grid.CalculateGridScore(p_MaximizingPlayer);
                m_IterationNumber++;
                return p_Node;
            }

            else
            {
                if (p_Node.WhoseTurnItIs == p_MaximizingPlayer)
                {
                    Node nodeWithScoreMax = null;

                    foreach (int column in p_Node.Grid.ColumnNotFull)
                    {
                        GameGrid newGameGrid = p_Node.Grid.CloneGameGrid(p_Node.Grid);

                        newGameGrid.AddTokenInColumn(column, p_MaximizingPlayer.TokenColor);

                        Node newNode = new Node(p_MinimizingPlayer, newGameGrid, column, p_Node.Depth + 1);
                        Node currentNode = Minimax(newNode, p_MaxDepth, p_Alpha, p_Beta, p_MaximizingPlayer, p_MinimizingPlayer);

                        if (currentNode == null)
                        {
                            nodeWithScoreMax = newNode;
                        }
                        else
                        {
                            newNode.Grid.Score = currentNode.Grid.Score;

                            if (nodeWithScoreMax == null)
                            {
                                nodeWithScoreMax = newNode;
                            }
                            else
                            {
                                nodeWithScoreMax = currentNode.Grid.Score > nodeWithScoreMax.Grid.Score ? newNode : nodeWithScoreMax;

                                if (nodeWithScoreMax.Grid.Score > p_Beta)
                                {
                                    return nodeWithScoreMax;
                                }
                                p_Alpha = Math.Max(p_Alpha, nodeWithScoreMax.Grid.Score);
                            }


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
                        newGameGrid.AddTokenInColumn(column, p_MinimizingPlayer.TokenColor);
                        Node newNode = new Node(p_MaximizingPlayer, newGameGrid, column, p_Node.Depth + 1);

                        Node currentNode = Minimax(newNode, p_MaxDepth, p_Alpha, p_Beta, p_MaximizingPlayer, p_MinimizingPlayer);

                        if (currentNode == null)
                        {
                            nodeWithScoreMin = newNode;
                        }
                        else
                        {
                            newNode.Grid.Score = currentNode.Grid.Score;

                            if (nodeWithScoreMin == null)
                            {
                                nodeWithScoreMin = currentNode;
                            }
                            else
                            {
                                nodeWithScoreMin = currentNode.Grid.Score < nodeWithScoreMin.Grid.Score ? newNode : nodeWithScoreMin;

                                if (p_Alpha > nodeWithScoreMin.Grid.Score)
                                {
                                    return nodeWithScoreMin;
                                }
                                p_Beta = Math.Min(p_Beta, nodeWithScoreMin.Grid.Score);
                            }
                        }
                    }
                    return nodeWithScoreMin;
                }
            }
        }
    }
}