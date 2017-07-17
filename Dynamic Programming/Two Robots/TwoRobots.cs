/*
 Problem Statement : https://www.hackerrank.com/challenges/two-robots/copy-from/49454121
 */

using System;
using System.Collections;

namespace HackerRank
{
    public class TwoRobots
    {
        public static void TwoRobotsMain()
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */
            int testCases = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < testCases; i++)
            {
                string[] mn = Console.ReadLine().Split(' ');
                int containers = Convert.ToInt32(mn[0]);
                int queryCount = Convert.ToInt32(mn[1]);
                int[,] queries = new int[queryCount, 2];
                for (int j = 0; j < queryCount; j++)
                {
                    string[] inputQueries = Console.ReadLine().Split(' ');
                    queries[j, 0] = Convert.ToInt32(inputQueries[0]);
                    queries[j, 1] = Convert.ToInt32(inputQueries[1]);
                }
                Hashtable queryTable = new Hashtable();
                int distance = CountDistance(queries, 1, queries[0, 1], -1, queryTable);
                Console.WriteLine(distance);
            }
        }

        static int CountDistance(int[,] queries, int queryIndex, int positionR1, int positionR2, Hashtable queryTable)
        {
            string key = queryIndex + "-" + positionR1+"-"+ positionR2;
            if (queryTable.ContainsKey(key))
            {
               return Convert.ToInt32(queryTable[key]);
            }
            if (queryIndex == queries.GetLength(0))
            {
                return Math.Abs(queries[0, 1] - queries[0, 0]);
            }

            int currentDistance = Math.Abs(queries[queryIndex, 1] - queries[queryIndex, 0]);

            int r1Distance = Math.Abs(positionR1 - queries[queryIndex, 0]) + currentDistance + CountDistance(queries, queryIndex + 1, queries[queryIndex, 1], positionR2, queryTable);
            int r2Distance = (positionR2 == -1 ? 0 : Math.Abs(positionR2 - queries[queryIndex, 0])) + currentDistance + CountDistance(queries, queryIndex + 1, positionR1, queries[queryIndex, 1], queryTable);
            int minDistance = Math.Min(r1Distance, r2Distance);
            if (!queryTable.ContainsKey(key))
                queryTable.Add(key, minDistance);
            return minDistance;

        }
    }
}
