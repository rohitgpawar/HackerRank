/*
 PROBLEM STATEMENT : https://www.hackerrank.com/challenges/ctci-array-left-rotation
 */

using System;

namespace HackerRank
{
    public class ArrayLeftRotation
    {
        public static void ArrayLeftRotationMain()
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int k = Convert.ToInt32(tokens_n[1]);
            string[] a_temp = Console.ReadLine().Split(' ');
            int[] a = Array.ConvertAll(a_temp, Int32.Parse);
            int[] output = new int[a.Length];

            k = k >= a.Length ? k - a.Length : k; // if k is equal to array length it will rotate back to the original order so k-a.Length will give correct location after the array is left rotated more than a.Length
            for (int i = 0; i < a.Length; i++)
            {//Store left rotation output in new array.
                if (k == a.Length) // If k == Length the next element will be 0th element.
                    k = 0;

                output[i] = a[k];
                k++;

                Console.Write(output[i].ToString() + ' ');
            }
        }

    }
}
