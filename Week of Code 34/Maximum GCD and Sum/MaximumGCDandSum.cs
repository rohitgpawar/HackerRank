/*
 PROBLEM STATEMENT: https://www.hackerrank.com/contests/w34/challenges/maximum-gcd-and-sum
 */
/*
LEARNING: Concept of GCD and most efficient way to solve it.
To find MAX GCD of a pair solve it by brute force. Assume that answer lies between 1 and max number in the pair and iteratively find the highest 
amongst those.
    */
using System;
using System.Linq;

namespace HackerRank
{
    public class MaximumGcdAndSum
    {
        static int maximumGcdAndSum(int[] A, int[] B)
        {
            //Find the maximum number amongst both arrays. Max GCD can me equal to or less than this max number.
            int maxOfA = A.Max();
            int maxOfB = B.Max();
            int maxPossibleGcd = maxOfA > maxOfB ? maxOfA : maxOfB;
            //To Count the occurance of digits in the both Arrays
            bool[] countNumbersInA = new bool[1000001];
            bool[] countNumbersInB = new bool[1000001];
            int maxGcdPairSum = 0; // Holds Sum of digits having actualMaxGCD
            int actualMaxGcd = 0; // Holds actualMaxGCD value.
            for (int i = 0; i < A.Length; i++)
            {//Count the occurance of digits in the both Arrays
                countNumbersInA[A[i]] = true;
                countNumbersInB[B[i]] = true;
            }

            for (int assumedGcd = 1; assumedGcd <= maxPossibleGcd; assumedGcd++)
            {//Iterate from 1 to maxNumber in the pairs because maxGCD lies amongst these numbers.
                bool assumedGcdMultipleInA = false; // to check if i divides any number contained in A
                bool assumedGcdMultipleInB = false; // to check if i divides any number contined in B
                int GcdMaxA = 0; //Holds max number that is divided by i and contained by A.
                int GcdMaxB = 0; // Holds max number that is divided by i and contained by B.
                for (int multipleOfassumedGcd = assumedGcd; multipleOfassumedGcd <= maxPossibleGcd; multipleOfassumedGcd += assumedGcd)
                {// Iterate from i and increment by i so that we reach numbers that are multiple of 'i'. i.e. 'i' divides all these numbers.
                    if (countNumbersInA[multipleOfassumedGcd])
                    {//This number is contained in A. So set assumedGcdMultipleInA to true as we have reached a number in A that was multiple of 'i'.
                        assumedGcdMultipleInA = true;
                        GcdMaxA = multipleOfassumedGcd;
                    }
                    if (countNumbersInB[multipleOfassumedGcd])
                    {//This number is contained in B. so set assumedGcdMultipleInB to true.
                        GcdMaxB = multipleOfassumedGcd;
                        assumedGcdMultipleInB = true;
                    }
                }
                if (assumedGcdMultipleInA && assumedGcdMultipleInB && assumedGcd > actualMaxGcd)
                {//IF assumedGcd has multiples both in A and in B and it is greater than actualMaxGCD then set maxGcdPairSum and update actualMaxGCD.
                    maxGcdPairSum = GcdMaxA + GcdMaxB;
                    actualMaxGcd = assumedGcd;
                }
            }
            return maxGcdPairSum; // return Max Sum.
        }

        public static void MaximumGcdAndSumMain()
        {
            int n = Convert.ToInt32(Console.ReadLine());
            string[] A_temp = Console.ReadLine().Split(' ');
            int[] A = Array.ConvertAll(A_temp, Int32.Parse);
            string[] B_temp = Console.ReadLine().Split(' ');
            int[] B = Array.ConvertAll(B_temp, Int32.Parse);
            int res = maximumGcdAndSum(A, B);
            Console.WriteLine(res);
        }
    }
}
