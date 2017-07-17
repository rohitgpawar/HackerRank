/*
 https://www.hackerrank.com/challenges/equal/problem
 */
/*
 Sample Input

1
4
2 2 3 7

Sample Output

2

 */

using System;
using System.Linq;

namespace HackerRank
{
    public class Equal
    {
        static int getOperations(int difference)
        {
            int diff = difference / 5;
            difference %= 5;
            diff += difference / 2;
            difference %= 2;
            diff += difference;
            return diff;
        }

        static int getWays(int[] c)
        {
            int minValue = c.Min();
            int minOperations = int.MaxValue;
            for (int i = 0; i < 4; i++)
            {
                int operations = 0;
                for (int j = 0; j < c.Length; j++)
                {
                     operations += getOperations(c[j] - minValue + i);
                }
                if (operations < minOperations)
                    minOperations = operations;
            }

            return minOperations;
        }


        public static void EqualMain()
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < t; i++)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                string[] c_temp = Console.ReadLine().Split(' ');
                int[] c = Array.ConvertAll(c_temp, Int32.Parse);
                int ways = getWays(c);
                Console.WriteLine("{0}", ways);
            }
        }
    }
}
