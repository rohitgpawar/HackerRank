/*
Problem Statement : https://www.hackerrank.com/challenges/coin-change

Given a value N, if we want to make change for N cents, and we have infinite supply of each of S = { S1, S2, .. , Sm} valued coins, how many ways can we make the change? The order of coins doesn’t matter.

For example, for N = 4 and S = {1,2,3}, there are four solutions: {1,1,1,1},{1,1,2},{2,2},{1,3}. So output should be 4. For N = 10 and S = {2, 5, 3, 6}, there are five solutions: {2,2,2,2,2}, {2,2,3,3}, {2,2,6}, {2,3,5} and {5,5}. So the output should be 5.
 */

/*
INPUT: 
4 3
1 2 3

OUTPUT: 4

INPUT:
10 4
2 5 3 6

OUTPUT: 5

 */

using System;
using System.Collections.Generic;

namespace HackerRank
{
    public class CoinChangeProblem
    {
        static long getWays(long n, long[] c)
        {
            // Complete this function
            return countWays(c, n, 0, new Dictionary<String, long>());
        }

        static long countWays(long[] c, long n, int index, Dictionary<String, long> map)
        {
            //Console.WriteLine("{0},{1}",n,index);
            if (n == 0)
            {
                return 1;
            }
            if (index >= c.Length)
            {
                return 0;
            }
            string key = n + "-" + index;
            if (map.ContainsKey(key))
            {
                return map[key];
            }
            long amount = 0;
            long ways = 0;
            while (amount <= n)
            {
                long remaningAmount = n - amount;
                ways += countWays(c, remaningAmount, index + 1, map);
                amount += c[index];
            }
            map.Add(key, ways);
            return ways;
        }

        public static void CoinChangeProblemMain()
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int m = Convert.ToInt32(tokens_n[1]);
            string[] c_temp = Console.ReadLine().Split(' ');
            long[] c = Array.ConvertAll(c_temp, Int64.Parse);
            // Print the number of ways of making change for 'n' units using coins having the values given by 'c'
            long ways = getWays(n, c);
            Console.WriteLine("{0}", ways);
        }
    }
}
