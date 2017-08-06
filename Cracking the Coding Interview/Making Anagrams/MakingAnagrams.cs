/*
 PROBLEM STATEMENT: https://www.hackerrank.com/challenges/ctci-making-anagrams/problem
 */

using System;

namespace HackerRank
{
    public class MakingAnagrams
    {
        static int[] convertStringToIntCount(string str)
        {//Fill int[] with count of characters from the string.
            int[] countChars = new int[26];
            foreach (char c in str)
            {
                countChars[(int)c - (int)'a']++;
            }
            return countChars;
        }

        public static void MakingAnagramsMain()
        {
            string a = Console.ReadLine();
            string b = Console.ReadLine();
            int[] countStr1Char = convertStringToIntCount(a);
            int[] countStr2Char = convertStringToIntCount(b);
            int differenceInCharCount = 0;
            for (int i = 0; i < 26; i++)
            {//Compare two Character count arrays and count the difference of each charachter count.
                differenceInCharCount += Math.Abs(countStr1Char[i] - countStr2Char[i]);
            }
            Console.WriteLine(differenceInCharCount);
        }
    }
}
