
/*
 PROBLEM STATEMENT: https://www.hackerrank.com/contests/w34/challenges/same-occurrence

 DIT NOT PASS ALL TEST CASES.
 */

using System;

namespace HackerRank
{
    public class SameOccurance
    {
        static int countSubArrayWithSameOccurance(int[] arr, int x, int y)
        {
            int countX = 0;
            int countY = 0;
            int countSubArray = 0;
            int startIndex = 0;
            int lastZeroIndex = -1;
            bool zeroFound = false;
            int localCountX = countX;
            int localCountY = countY;
            int countXEqualsY = 1;
            int lastMatchX = -1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (x == arr[i])
                {
                    if (lastMatchX == 1)
                        countSubArray++;
                    countX++;
                    if(lastMatchX != -1)
                        lastMatchX = 0;
                }
                if (y == arr[i])
                {
                    countY++;
                    if(lastMatchX != -1)
                        lastMatchX = 1;
                }
                startIndex = 0;
                localCountX = countX;
                localCountY = countY;
                if (zeroFound && (x == arr[i] || y == arr[i]))
                {
                    countSubArray += (i - lastZeroIndex) * (i - lastZeroIndex + 1) / 2;
                    zeroFound = false;
                }
                if (!zeroFound && x != arr[i] && y != arr[i])
                {
                    zeroFound = true;
                    lastZeroIndex = i;
                }

                if (countX == countY && countX != 0)
                {
                    if (lastMatchX == -1)
                        lastMatchX = x == arr[i] ? 0 : 1;
                    countXEqualsY++;
                    
                }
            }
            if (zeroFound)
            {
                countSubArray += (arr.Length - lastZeroIndex) * (arr.Length - lastZeroIndex + 1) / 2;
                zeroFound = false;
            }
            countSubArray += countXEqualsY * (countXEqualsY - 1) / 2;
            return countSubArray;
        }

        public static void SameOccuranceMain()
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int q = Convert.ToInt32(tokens_n[1]);
            string[] arr_temp = Console.ReadLine().Split(' ');
            int[] arr = Array.ConvertAll(arr_temp, Int32.Parse);
            for (int a0 = 0; a0 < q; a0++)
            {
                string[] tokens_x = Console.ReadLine().Split(' ');
                int x = Convert.ToInt32(tokens_x[0]);
                int y = Convert.ToInt32(tokens_x[1]);
                Console.WriteLine(countSubArrayWithSameOccurance(arr, x, y));
                // Write Your Code Here
            }
        }
    }
}
