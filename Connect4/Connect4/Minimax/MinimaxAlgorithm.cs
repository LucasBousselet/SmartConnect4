using System;
using System.Diagnostics;

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
        /// The running time of the algorithm.
        /// </summary>
        private static string m_RunningTime = string.Empty;

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
        /// Get the algorithm running time.
        /// </summary>
        public static string ElapsedTime
        {
            get
            {
                return m_RunningTime;
            }
        }

        /// <summary>
        /// Access point for the Minimax algorithm.
        /// </summary>
        /// <param name="p_GameGrid"> The GameGrid to consider. </param>
        /// <param name="p_MaxDepth"> The maximum depth for Minimax. </param>
        /// <param name="p_MaximizingPlayer"> The maximizing player. </param>
        /// <param name="p_MinimizingPlayer"> The minimizing player. </param>
        /// <returns> The column to play in. </returns>
        public static int GetInWhichColumnPlay(GameGrid p_GameGrid, int p_MaxDepth, Connect4Player p_MaximizingPlayer, Connect4Player p_MinimizingPlayer)
        {
            m_IterationNumber = 0;
            Node startingNode = new Node(p_MaximizingPlayer, p_GameGrid, -1, 0);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Node result = Minimax(startingNode, p_MaxDepth, int.MinValue, int.MaxValue, p_MaximizingPlayer, p_MinimizingPlayer);
            stopWatch.Stop();

            m_RunningTime = string.Format("{0:0} s {1:00} ms", stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds);

            return result.TokenAddedInColumn;
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
                p_Node.Grid.CalculateGridScore(p_MaximizingPlayer.TokenColor);
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

                        if (p_Node.Depth == 0)
                        {
                            newGameGrid.CalculateGridScore(p_MaximizingPlayer.TokenColor);
                        }

                        if (p_Node.Depth == 0 && newGameGrid.FourTokenAligned)
                        {
                            return newNode;
                        }
                        else
                        {
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