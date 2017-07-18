/*
 PROBLEM STATEMENT : https://www.hackerrank.com/contests/w34/challenges/once-in-a-tram/submissions/code/1302377052
 */

using System;

namespace HackerRank
{
    public class OnceInATram
    {
        static string onceInATram(int x)
        {//Remeber we can only increment the numbers we cant decrement as we have to find the next number having same sum
            // Complete this function
            int[] ticketDigits = new int[6];//int[] to store each digit seperately in array.
            int index = 5;
            int firstThreeSum = 0;
            int lastThreeSum = 0;
            string nextLuckyNumber = "";// To store the final output as a string
            while (x > 0)
            {//Store the int in int[] and compute firstThreeSum & lastThreeSum.
                ticketDigits[index] = x % 10;
                if (index < 3)
                    firstThreeSum = firstThreeSum + ticketDigits[index]; //Calculate sum of first 3 digits
                else
                    lastThreeSum = lastThreeSum + ticketDigits[index]; //Calculate sum of last 3 digits
                x = x / 10;
                index--;
            }
            if (firstThreeSum > lastThreeSum)
            {// If sum of first 3 digits is greater than sum of last 3 digits. Then keep on incrementing the last 3 digits starting from the last digit.
                //Stop when both sums are equal.
                while (ticketDigits[3] + ticketDigits[4] + ticketDigits[5] < firstThreeSum)
                {//Loop until sum of last 3 digits is less than sum of first three.
                    if (ticketDigits[5] < 9)
                        ticketDigits[5]++; //Increment last digit until it reaches 9.
                    else if (ticketDigits[4] < 9)
                        ticketDigits[4]++; // Increment second last digit until it reaches 9.
                    else //if(ticketNo[3]<9)
                        ticketDigits[3]++; //Increment third last digit until it reaches 9.
                }
            }
            else // (firstThreeSum <= lastThreeSum)
            {//If sum of first 3 digits is less than or equal to sum of last 3 digits. Then make the last digit 0 and increment the second last digit.
                //Continue until the sum of last 3 numbers is less than sum of first three numbers.
                int makeZeroIndex = 5;
                while (ticketDigits[3] + ticketDigits[4] + ticketDigits[5] >= firstThreeSum)
                {// Loop until sum of last 3 digits is greater than or equal to sum of last three digits.
                    if (ticketDigits[makeZeroIndex] > 0 && makeZeroIndex > 2)
                    {//If Last digit is greater than 0 make last digit 0 and increment second last digit. This will make total sum less e.g. for 678 sum = 21 and for 680 sum = 14. 
                        
                        ticketDigits[makeZeroIndex] = 0;
                        while (ticketDigits[makeZeroIndex-1] == 9 && makeZeroIndex > 1)
                        {//Handle 9 digit case. If Digit is 9 then make it 0 and increment its previous digit so obtail less sum than current.
                            ticketDigits[makeZeroIndex - 1] = 0;
                            makeZeroIndex--;
                        }
                        ticketDigits[makeZeroIndex - 1]++;
                    }
                    makeZeroIndex--;
                }
                //If sum of first  3 numbers is greater than last three numbers then add the difference to last 3 digits to make sum equal.
                int difference = (ticketDigits[0] + ticketDigits[1] + ticketDigits[2]) - (ticketDigits[3] + ticketDigits[4] + ticketDigits[5]);
                int differenceIndex = 5;
                while (difference > 0)
                {// Add the difference to lastThreeSum to make it equal to firstThreeSum.
                    ticketDigits[differenceIndex] = difference % 10;
                    difference = difference / 10;
                }
            }
            

            foreach (int i in ticketDigits) //Loop through array and concatinate the digits to print.
                nextLuckyNumber = nextLuckyNumber + i;

            return nextLuckyNumber;
        }

        public static void OnceInATramMain()
        {
            int x = Convert.ToInt32(Console.ReadLine());
            string result = onceInATram(x);
            Console.WriteLine(result);
        }
    }
}
