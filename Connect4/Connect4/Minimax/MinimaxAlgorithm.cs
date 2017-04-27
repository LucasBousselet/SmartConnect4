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

            // We start the stopwatch to time the algorithm execution time.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // We run the minimax algorithm and get the result node which contains the column we need to play in.
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
        /* 
         * DISCLAIMER
         * The algorithm could probalby be written in a more summarized version but we chose
         * to clearly decompose it at the maximum to make it more understandable.
         */
        private static Node Minimax(Node p_Node, int p_MaxDepth, int p_Alpha, int p_Beta, Connect4Player p_MaximizingPlayer, Connect4Player p_MinimizingPlayer)
        {
            // We check if we have 4 tokens aligned.
            // It's interesting to note that since calculating the heuristic and checking
            // if we have 4 tokens align are almost the same (scan grid in all directions),
            // it takes as less time to calculate the score everytime than to scan it for
            // a winning combination and then calculate the score only if we have a
            // winning combination (which would mean scanning the grid entirely twice !)
            p_Node.Grid.CalculateGridScore(p_MaximizingPlayer.TokenColor);

            // If we reached the maximum depth defined for the algorith execution, we return the result node.
            if (p_Node.Grid.FourTokenAligned || p_Node.Depth == p_MaxDepth)
            {
                m_IterationNumber++;
                return p_Node;
            }

            // Otherwise we run the recurrence.
            else
            {
                // If it's the maximizing player's (i.e. AI player) turn...
                if (p_Node.WhoseTurnItIs == p_MaximizingPlayer)
                {
                    // Variable used to store the node with the maximum score.
                    Node nodeWithScoreMax = null;

                    // ...we play in each column that's not already full :
                    foreach (int column in p_Node.Grid.ColumnNotFull)
                    {
                        // We clone the grid.
                        GameGrid newGameGrid = p_Node.Grid.CloneGameGrid(p_Node.Grid);
                        // We add a tocken in the given column in the cloned cell.
                        newGameGrid.AddTokenInColumn(column, p_MaximizingPlayer.TokenColor);
                        // We create a new child node with these data.
                        Node newNode = new Node(p_MinimizingPlayer, newGameGrid, column, p_Node.Depth + 1);

                        // Accelerate the execution in case of an obvious win at a depth of 0 (i.e. at the first 
                        // comuted turn of the AI).
                        if (p_Node.Depth == 0)
                        {
                            newGameGrid.CalculateGridScore(p_MaximizingPlayer.TokenColor);
                            if (newGameGrid.FourTokenAligned)
                            {
                                return newNode;
                            }
                        }

                        // If we have no obvious win...
                        if (!newGameGrid.FourTokenAligned)
                        {
                            // ...we run the minimax algorithm recursively.
                            Node currentNode = Minimax(newNode, p_MaxDepth, p_Alpha, p_Beta, p_MaximizingPlayer, p_MinimizingPlayer);

                            // We store the first node found to initiate the comparison.
                            if (currentNode == null)
                            {
                                nodeWithScoreMax = newNode;
                            }

                            // After that, we compare the score of the node found in the recurrence with the one stored.
                            else
                            {
                                // We store the score computed by the recurrence.
                                newNode.Grid.Score = currentNode.Grid.Score;

                                // Case handling the 6 last turn when we cannot reach the reccurrence max depth, therefore we
                                // return the last node computed before get a null result.
                                if (nodeWithScoreMax == null)
                                {
                                    nodeWithScoreMax = newNode;
                                }

                                // if the recurrence result isn't null...
                                else
                                {
                                    // ...we compare the score of the node found in the recurrence with the one stored in nodeWithScoreMax.
                                    // if it's greater that the previous one, we update the value.
                                    nodeWithScoreMax = currentNode.Grid.Score > nodeWithScoreMax.Grid.Score ? newNode : nodeWithScoreMax;

                                    // Here we use the beta prunning to exclude the next nodes if the node score is greater
                                    // that the minimum one computed before (i.e. all next nodes will be discarded anyway).
                                    if (nodeWithScoreMax.Grid.Score > p_Beta)
                                    {
                                        return nodeWithScoreMax;
                                    }

                                    // We update the value for the alpha prunning.
                                    p_Alpha = Math.Max(p_Alpha, nodeWithScoreMax.Grid.Score);
                                }

                            }

                        }
                    }
                    return nodeWithScoreMax;
                }

                // If it's the minimizing player's (i.e. the human player) turn, the code structure is pretty much the same.
                else
                {
                    // Variable used to store the node with the minimum score.
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
                                // We compare the score of the node found in the recurrence with the one stored in nodeWithScoreMin.
                                // if it's smaller that the previous one, we update the value.
                                nodeWithScoreMin = currentNode.Grid.Score < nodeWithScoreMin.Grid.Score ? newNode : nodeWithScoreMin;

                                // Here we use the alpha prunning to exclude the next nodes if the node score is smaller
                                // that the maximum one computed before (i.e. all next nodes will be discarded anyway).
                                if (p_Alpha > nodeWithScoreMin.Grid.Score)
                                {
                                    return nodeWithScoreMin;
                                }

                                // We update the value for the beta prunning.
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