/*
 PROBLEM STATEMENT: https://www.hackerrank.com/challenges/ctci-ransom-note
 */

using System;
using System.Collections;

namespace HackerRank
{
    public class RansomNote
    {
        static Hashtable magazineWordFrequency = new Hashtable();//Hashtable to store word frequency in magazine.

        static void storeMagazine(string[] magazine)
        {//Store word frequency for magazine.
            foreach (string word in magazine)
            {
                if (magazineWordFrequency.ContainsKey(word))
                {
                    int wordFrequency = (int)magazineWordFrequency[word];
                    magazineWordFrequency[word] = ++wordFrequency;
                }
                else
                {
                    magazineWordFrequency[word] = 1;
                }
            }
        }

        public static void RansomNoteMain()
        {
            string[] tokens_m = Console.ReadLine().Split(' ');
            int m = Convert.ToInt32(tokens_m[0]);
            int n = Convert.ToInt32(tokens_m[1]);
            string[] magazine = Console.ReadLine().Split(' ');
            string[] ransom = Console.ReadLine().Split(' ');
            storeMagazine(magazine);
            bool ransomWordPresent = true;
            foreach (string ransomWord in ransom)
            {
                if (magazineWordFrequency.ContainsKey(ransomWord))
                {//Word is Present in Hashtable
                    int wordFrequency = (int)magazineWordFrequency[ransomWord];
                    if (wordFrequency > 0)
                    {
                        magazineWordFrequency[ransomWord] = --wordFrequency;
                    }
                    else//wordFrequency 0
                    {
                        ransomWordPresent = false;
                        break;
                    }
                }
                else
                {// word is missing in Hashtable
                    ransomWordPresent = false;
                    break;
                }
            }
            if (ransomWordPresent)
                Console.WriteLine("Yes");
            else
                Console.WriteLine("No");
        }
    }
}
