using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    public class PokerHand
    {
        public static int[][] totalValuesPlayers = new int[2][];
        public static List<List<int>> handlValuesPlayers = new List<List<int>>();
        public static int[] valueCountPlayers = new int[2];
        public static StreamWriter writetext = new StreamWriter("‪PockerHandOutPut.txt");
        public enum Rank
        {
            RoyalFlush = 10,
            StraightFlush = 9,
            FourOfAKind = 8,
            FullHouse = 7,
            Flush = 6,
            Straight = 5,
            ThreeOfKind = 4,
            TwoPairs = 3,
            OnePair = 2,
            HighestCard = 1
        }

        public enum Value
        {
            T = 10,
            J = 11,
            Q = 12,
            K = 13,
            A = 14
        }

        public static Rank calculateRank(string playerHand)
        {
            int[] totalValues = new int[15];
            List<int> handValue = new List<int>();
            string[] cards = playerHand.Split(' ');
            bool isSameSuit = true;
            char firstSuit = cards[0][1];
            int valueCount = 0;
            int highestCard = 0;
            bool isDistinctValues = true;
            int highestValueCount = 0;
            int pairCount = 0;
            foreach (string card in cards)
            {
                if (isSameSuit && card[1] != firstSuit)
                {
                    isSameSuit = false;
                }
                int value = 0;
                if (!int.TryParse(card[0].ToString(), out value))
                {
                    switch (card[0])
                    {
                        case 'T':
                            value = (int)Value.T;
                            break;
                        case 'J':
                            value = (int)Value.J;
                            break;
                        case 'Q':
                            value = (int)Value.Q;
                            break;
                        case 'K':
                            value = (int)Value.K;
                            break;
                        case 'A':
                            value = (int)Value.A;
                            break;
                    }
                }
                if (totalValues[value] > 0)
                    isDistinctValues = false;
                totalValues[value]++;
                if (highestValueCount < totalValues[value])
                    highestValueCount = totalValues[value];
                if (totalValues[value] == 2)
                    pairCount++;
                else if (totalValues[value] == 3)
                    pairCount--;
                if (highestCard < value)
                    highestCard = value;
                valueCount += value;
                handValue.Add(value);
            }
            handlValuesPlayers.Add(handValue.OrderByDescending(qry => qry).ToList());
            if (totalValuesPlayers[0] == null)
                totalValuesPlayers[0] = totalValues;
            else
                totalValuesPlayers[1] = totalValues;
            if (valueCountPlayers[0] == 0)
                valueCountPlayers[0] = valueCount;
            else
                valueCountPlayers[1] = valueCount;
            if (isSameSuit && valueCount == 60)
                return Rank.RoyalFlush;
            if (isSameSuit && isDistinctValues && (highestCard + highestCard - 1 + highestCard - 2 + highestCard - 3 + highestCard - 4 == valueCount))
                return Rank.StraightFlush;
            if (highestValueCount == 4)
                return Rank.FourOfAKind;
            if (highestValueCount == 3 && pairCount == 1)
            {
                return Rank.FullHouse;
            }
            if (isSameSuit)
                return Rank.Flush;
            if (isDistinctValues && (highestCard + highestCard - 1 + highestCard - 2 + highestCard - 3 + highestCard - 4 == valueCount))
                return Rank.Straight;
            if (highestValueCount == 3)
                return Rank.ThreeOfKind;
            if (pairCount == 2)
                return Rank.TwoPairs;
            if (pairCount == 1)
                return Rank.OnePair;

            return Rank.HighestCard;
        }

        public static void calcuateHighestInSameRank(Rank rank, string playersHand)
        {
            switch (rank)
            {
                case Rank.StraightFlush:
                case Rank.Straight:
                case Rank.Flush:
                case Rank.HighestCard:
                    getRankByValue();
                    break;
                case Rank.FourOfAKind:
                    for (int i = 14; i > 0; i--)
                    {
                        if (totalValuesPlayers[0][i] == 4 && totalValuesPlayers[1][i] == 4)
                        {
                            getRankByValue();
                            break;
                        }

                        if (totalValuesPlayers[0][i] == 4)
                        {
                            WriteToFile("P1"); //Console.WriteLine("Player 1");
                            break;
                        }
                        if (totalValuesPlayers[1][i] == 4)
                        {
                            WriteToFile("P2"); //Console.WriteLine("Player 2");
                            break;
                        }
                    }
                    break;
                case Rank.FullHouse:
                    for (int i = 14; i > 0; i--)
                    {
                        if (totalValuesPlayers[0][i] == 3 && totalValuesPlayers[1][i] == 3)
                        {
                            for (int j = 14; j > 0; j--)
                            {
                                if (totalValuesPlayers[0][j] == 2)
                                {
                                    WriteToFile("P1"); //Console.WriteLine("Player 1");
                                    break;
                                }
                                else if (totalValuesPlayers[1][j] == 2)
                                {
                                    WriteToFile("P2"); //Console.WriteLine("Player 2");
                                    break;
                                }
                            }
                            break;
                        }

                        if (totalValuesPlayers[0][i] == 3 && totalValuesPlayers[1][i] != 3)
                        {
                            WriteToFile("P1"); //Console.WriteLine("Player 1");
                            break;
                        }
                        else if (totalValuesPlayers[1][i] == 3 && totalValuesPlayers[0][i] != 3)
                        {
                            WriteToFile("P2"); //Console.WriteLine("Player 2");
                            break;
                        }
                    }

                    break;
                case Rank.ThreeOfKind:
                    for (int i = 14; i > 0; i--)
                    {
                        if (totalValuesPlayers[0][i] == 3 && totalValuesPlayers[1][i] == 3)
                        {
                            getRankByValue();
                            break;
                        }
                        if (totalValuesPlayers[0][i] == 3)
                        {
                            WriteToFile("P1"); //Console.WriteLine("Player 1");
                            break;
                        }
                        else if (totalValuesPlayers[1][i] == 3)
                        {
                            WriteToFile("P2"); //Console.WriteLine("Player 2");
                            break;
                        }
                    }
                    break;
                case Rank.TwoPairs:
                case Rank.OnePair:
                    for (int i = 14; i > 0; i--)
                    {
                        if (totalValuesPlayers[0][i] == 2 && totalValuesPlayers[1][i] == 2)
                        {
                            getRankByValue();
                            break;
                        }
                        if (totalValuesPlayers[0][i] == 2)
                        {
                            WriteToFile("P1"); //Console.WriteLine("Player 1");
                            break;
                        }
                        else if (totalValuesPlayers[1][i] == 2)
                        {
                            WriteToFile("P2"); //Console.WriteLine("Player 2");
                            break;
                        }
                    }
                    break;
            }
        }

        public static void getRankByValue()
        {
            //handlValuesPlayers
            for (int i = 14; i > 0; i--)
            {
                if (totalValuesPlayers[0][i] > totalValuesPlayers[1][i])
                {
                    //Console.WriteLine("Player 1");
                    WriteToFile("P1");
                    break;
                }
                else if (totalValuesPlayers[0][i] < totalValuesPlayers[1][i])
                {
                    //Console.WriteLine("Player 2");
                    WriteToFile("P2");
                    break;
                }
            }
        }

        public static void WriteToFile(string output)
        {
            writetext.WriteLine(output);
            writetext.Flush();
        }
        static public void PokerHandMain()
        {

            int testCase = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < testCase; i++)
            {
                string playersHand = Console.ReadLine();
                totalValuesPlayers = new int[2][];
                valueCountPlayers = new int[2];
                handlValuesPlayers = new List<List<int>>();
                Rank player1Rank = calculateRank(playersHand.Substring(0, 14));
                Rank player2Rank = calculateRank(playersHand.Substring(15, 14));
                if (player1Rank > player2Rank)
                    //Console.WriteLine("Player 1");
                    WriteToFile("P1");
                else if (player1Rank < player2Rank)
                    WriteToFile("P2");//Console.WriteLine("Player 2");

                if (player1Rank == player2Rank)
                {
                    calcuateHighestInSameRank(player1Rank, playersHand);
                }
            }
        }
    }

    public class PockerHandTry2
    {
        public static StreamWriter writetext = new StreamWriter("‪PockerHandOutPut.txt");
        public class HandRankValue
        {
            public Rank rank;
            public int[] values = new int[5];
        }
        public enum Rank
        {
            RoyalFlush = 10,
            StraightFlush = 9,
            FourOfAKind = 8,
            FullHouse = 7,
            Flush = 6,
            Straight = 5,
            ThreeOfKind = 4,
            TwoPairs = 3,
            OnePair = 2,
            HighestCard = 1
        }

        public enum Value
        {
            T = 10,
            J = 11,
            Q = 12,
            K = 13,
            A = 14
        }
        public static List<int[]> straightValues = new List<int[]>();
        private static void fillStaraightValues()
        {
            for (int i = 14; i > 5; i--)
            {
                straightValues.Add(new int[5] { i, i - 1, i - 2, i - 3, i - 4 });
            }
            straightValues.Add(new int[5] { 14, 5, 4, 3, 2 });
        }

        private static bool checkStraight(int[] handValue)
        {
            foreach (int[] straight in straightValues)
            {
                if (handValue.Intersect(straight).Count() == 5)
                    return true;
            }
            return false;
        }

        public static HandRankValue calculateRank(string playerHand)
        {
            int[] totalValues = new int[15];
            List<int> handValue = new List<int>();
            string[] cards = playerHand.Split(' ');
            bool isSameSuit = true;
            char firstSuit = cards[0][1];
            int valueCount = 0;
            int highestCard = 0;
            int highestValueCount = 0;
            int pairCount = 0;
            Rank output = Rank.HighestCard;
            HandRankValue outputHandRankVal = new HandRankValue();
            foreach (string card in cards)
            {
                if (isSameSuit && card[1] != firstSuit)
                {
                    isSameSuit = false;
                }
                int value = 0;
                if (!int.TryParse(card[0].ToString(), out value))
                {
                    switch (card[0])
                    {
                        case 'T':
                            value = (int)Value.T;
                            break;
                        case 'J':
                            value = (int)Value.J;
                            break;
                        case 'Q':
                            value = (int)Value.Q;
                            break;
                        case 'K':
                            value = (int)Value.K;
                            break;
                        case 'A':
                            value = (int)Value.A;
                            break;
                    }
                }
                totalValues[value]++;
                if (highestValueCount < totalValues[value])
                    highestValueCount = totalValues[value];
                if (totalValues[value] == 2)
                    pairCount++;
                else if (totalValues[value] == 3)
                    pairCount--;
                if (highestCard < value)
                    highestCard = value;
                valueCount += value;
                handValue.Add(value);
            }
            if (isSameSuit)
            {
                output = Rank.Flush;
                if (checkStraight(handValue.ToArray()))
                {
                    output = Rank.StraightFlush;
                }
                if (valueCount == 60)
                {
                    output = Rank.RoyalFlush;
                }
            }
            else if (checkStraight(handValue.ToArray()))
            {
                output = Rank.Straight;
            }

            if (highestValueCount == 4)
                output = Rank.FourOfAKind;
            else if (highestValueCount == 3 && pairCount == 1)
                output = Rank.FullHouse;
            else if (highestValueCount == 3)
                output = Rank.ThreeOfKind;
            else if (pairCount == 2)
                output = Rank.TwoPairs;
            else if (pairCount == 1)
                output = Rank.OnePair;

            outputHandRankVal.rank = output;
            outputHandRankVal.values = (handValue.GroupBy(qry => qry).OrderByDescending(qry => qry.Key).OrderByDescending(x => x.Count())
                                            .Select(group => new
                                            {
                                                group.Key
                                            }).Select(qry => qry.Key)).ToArray();//handValue.ToArray();
            return outputHandRankVal;
        }

        public static void compareHandRankValue(HandRankValue player1Rank, HandRankValue player2Rank)
        {
            if ((int)player1Rank.rank > (int)player2Rank.rank)
                //Console.WriteLine("Player 1");
                WriteToFile("P1");
            else if ((int)player1Rank.rank < (int)player2Rank.rank)
                //Console.WriteLine("Player 2");
                WriteToFile("P2");
            else // Rank Same
            {
                int countValues = player1Rank.values.Count();
                for (int i = 0; i < countValues; i++)
                {
                    if (player1Rank.values[i] > player2Rank.values[i])
                    {
                        Console.WriteLine("Player 1");
                        WriteToFile("P1");
                        break;
                    }
                    else if (player1Rank.values[i] < player2Rank.values[i])
                    {
                        Console.WriteLine("Player 2");
                        WriteToFile("P2");
                        break;
                    }
                }
            }
        }
        public static void WriteToFile(string output)
        {
            writetext.WriteLine(output);
            writetext.Flush();
        }
        static public void PokerHandMain()
        {
            fillStaraightValues();
            int testCase = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < testCase; i++)
            {
                string playersHand = Console.ReadLine();
                HandRankValue player1Rank = calculateRank(playersHand.Substring(0, 14));
                HandRankValue player2Rank = calculateRank(playersHand.Substring(15, 14));
                compareHandRankValue(player1Rank, player2Rank);
            }
        }
    }

    class PockerHandSolution
    {
        public enum HandRank
        {
            //RoyalFlush = 10,
            StraightFlush = 9,
            FourOfAKind = 8,
            FullHouse = 7,
            Flush = 6,
            Straight = 5,
            ThreeOfKind = 4,
            TwoPair = 3,
            OnePair = 2,
            HighCard = 1
        }

        public enum Value
        {
            T = 10,
            J = 11,
            Q = 12,
            K = 13,
            A = 14
        }
        static bool isValidHand(string[] cards)
        {
            HashSet<string> cardsSet = new HashSet<string>();
            List<char> validSuits = new List<char> { 'h', 'c', 'd', 's' };
            foreach (string card in cards)
            {
                if (!validSuits.Contains(card[1]))
                {
                    return false;
                }
                if (cardsSet.Contains(card))
                    return false;
                else
                    cardsSet.Add(card);
            }
            return true;
        }

        public static List<int[]> straightValues = new List<int[]>();
        private static void fillStaraightValues()
        {
            for (int i = 14; i > 5; i--)
            {
                straightValues.Add(new int[5] { i, i - 1, i - 2, i - 3, i - 4 });
            }
            straightValues.Add(new int[5] { 14, 5, 4, 3, 2 });
        }

        private static bool checkStraight(int[] handValue)
        {
            foreach (int[] straight in straightValues)
            {
                if (handValue.Intersect(straight).Count() == 5)
                    return true;
            }
            return false;
        }

        public static HandRank calculateRank(string[] cards)
        {
            int[] totalValues = new int[15];
            List<int> handValue = new List<int>();
            bool isSameSuit = true; // Check for flush
            char firstSuit = cards[0][1]; // Set Default flush
            int highestValueCount = 0;
            int pairCount = 0;
            HandRank output = HandRank.HighCard;
            foreach (string card in cards)
            {
                if (isSameSuit && card[1] != firstSuit)
                {
                    isSameSuit = false;
                }
                int value = 0;
                if (!int.TryParse(card[0].ToString(), out value))
                {
                    switch (card[0])
                    {
                        case 'T':
                            value = (int)Value.T;
                            break;
                        case 'J':
                            value = (int)Value.J;
                            break;
                        case 'Q':
                            value = (int)Value.Q;
                            break;
                        case 'K':
                            value = (int)Value.K;
                            break;
                        case 'A':
                            value = (int)Value.A;
                            break;
                    }
                }
                totalValues[value]++;
                if (highestValueCount < totalValues[value])
                    highestValueCount = totalValues[value];
                if (totalValues[value] == 2)
                    pairCount++;
                else if (totalValues[value] == 3)
                    pairCount--;
                handValue.Add(value);
            }
            if (isSameSuit)
            {
                output = HandRank.Flush;
                if (checkStraight(handValue.ToArray()))
                {
                    output = HandRank.StraightFlush;
                }
            }
            else if (checkStraight(handValue.ToArray()))
            {
                output = HandRank.Straight;
            }

            if (highestValueCount == 4)
                output = HandRank.FourOfAKind;
            else if (highestValueCount == 3 && pairCount == 1)
                output = HandRank.FullHouse;
            else if (highestValueCount == 3)
                output = HandRank.ThreeOfKind;
            else if (pairCount == 2)
                output = HandRank.TwoPair;
            else if (pairCount == 1)
                output = HandRank.OnePair;

            return output;
        }

        public static void PockerHandSolutionMain()
        {
            string playersHand = Console.ReadLine();
            string[] cards = playersHand.Replace(",", "").Split(' ');
            if (cards.Count() != 5 || !isValidHand(cards))
            {
                Console.WriteLine("invalid input");
                return;
            }
            fillStaraightValues();
            HandRank rankOutput = calculateRank(cards);
            switch (rankOutput)
            {
                case HandRank.StraightFlush:
                    Console.WriteLine("straight flush");
                    break;
                case HandRank.FourOfAKind:
                    Console.WriteLine("four of a kind");
                    break;
                case HandRank.FullHouse:
                    Console.WriteLine("full house");
                    break;
                case HandRank.Flush:
                    Console.WriteLine("flush");
                    break;
                case HandRank.Straight:
                    Console.WriteLine("straight");
                    break;
                case HandRank.ThreeOfKind:
                    Console.WriteLine("three of a kind");
                    break;
                case HandRank.TwoPair:
                    Console.WriteLine("two pair");
                    break;
                case HandRank.OnePair:
                    Console.WriteLine("one pair");
                    break;
                case HandRank.HighCard:
                    Console.WriteLine("high card");
                    break;
            }
        }
    }


    class Solution
    {
        public enum HandRank
        {
            //RoyalFlush = 10,
            StraightFlush = 9,
            FourOfAKind = 8,
            FullHouse = 7,
            Flush = 6,
            Straight = 5,
            ThreeOfKind = 4,
            TwoPair = 3,
            OnePair = 2,
            HighCard = 1
        }

        public enum Value
        {
            T = 10,
            J = 11,
            Q = 12,
            K = 13,
            A = 14
        }

        public static List<int[]> straightValues = new List<int[]>();

        private static void fillStraightValues()
        {
            for (int i = 14; i > 5; i--)
            {
                straightValues.Add(new int[5] { i, i - 1, i - 2, i - 3, i - 4 });
            }
            straightValues.Add(new int[5] { 14, 5, 4, 3, 2 });
        }

        static bool isValidHand(string[] cards)
        {
            HashSet<string> cardsSet = new HashSet<string>();
            List<char> validSuits = new List<char> { 'h', 'c', 'd', 's' };
            foreach (string card in cards)
            {
                if (!validSuits.Contains(card[1]))
                    return false;
                if (cardsSet.Contains(card))
                    return false;
                else
                    cardsSet.Add(card);
            }
            return true;
        }

        private static bool checkStraight(int[] handValue)
        {
            foreach (int[] straight in straightValues)
            {
                if (handValue.Intersect(straight).Count() == 5)
                    return true;
            }
            return false;
        }

        public static HandRank calculateRank(string[] cards)
        {
            int[] occurrenceOfValue = new int[15];//Store occurrence of each Value
            List<int> handValue = new List<int>();
            bool isSameSuit = true; // To Check for flush
            char firstSuit = cards[0][1]; // Set Default card suite
            int highestOccurrenceValue = 0;
            int pairCount = 0; //Count number of pairs
            HandRank output = HandRank.HighCard;
            foreach (string card in cards)
            {
                if (isSameSuit && card[1] != firstSuit)
                {
                    isSameSuit = false;
                }
                int value = 0;
                if (!int.TryParse(card[0].ToString(), out value))
                {
                    switch (card[0])
                    {
                        case 'T':
                            value = (int)Value.T;
                            break;
                        case 'J':
                            value = (int)Value.J;
                            break;
                        case 'Q':
                            value = (int)Value.Q;
                            break;
                        case 'K':
                            value = (int)Value.K;
                            break;
                        case 'A':
                            value = (int)Value.A;
                            break;
                    }
                }
                occurrenceOfValue[value]++;
                if (highestOccurrenceValue < occurrenceOfValue[value])
                    highestOccurrenceValue = occurrenceOfValue[value];
                if (occurrenceOfValue[value] == 2)
                    pairCount++;
                else if (occurrenceOfValue[value] == 3)
                    pairCount--;
                handValue.Add(value);
            }

            if (isSameSuit)
            {
                output = HandRank.Flush;
                if (checkStraight(handValue.ToArray()))
                {
                    output = HandRank.StraightFlush;
                }
            }
            else if (checkStraight(handValue.ToArray()))
            {
                output = HandRank.Straight;
            }

            if (highestOccurrenceValue == 4)
                output = HandRank.FourOfAKind;
            else if (highestOccurrenceValue == 3 && pairCount == 1)
                output = HandRank.FullHouse;
            else if (highestOccurrenceValue == 3)
                output = HandRank.ThreeOfKind;
            else if (pairCount == 2)
                output = HandRank.TwoPair;
            else if (pairCount == 1)
                output = HandRank.OnePair;

            return output;
        }

        static void Main(string[] args)
        {

            string playersHand = Console.ReadLine();
            string[] cards = playersHand.Replace(",", "").Split(' ');
            if (cards.Count() != 5 || !isValidHand(cards))
            {
                Console.WriteLine("invalid input");
                return;
            }
            fillStraightValues();
            HandRank rankOutput = calculateRank(cards);
            switch (rankOutput)
            {
                case HandRank.StraightFlush:
                    Console.WriteLine("straight flush");
                    break;
                case HandRank.FourOfAKind:
                    Console.WriteLine("four of a kind");
                    break;
                case HandRank.FullHouse:
                    Console.WriteLine("full house");
                    break;
                case HandRank.Flush:
                    Console.WriteLine("flush");
                    break;
                case HandRank.Straight:
                    Console.WriteLine("straight");
                    break;
                case HandRank.ThreeOfKind:
                    Console.WriteLine("three of a kind");
                    break;
                case HandRank.TwoPair:
                    Console.WriteLine("two pair");
                    break;
                case HandRank.OnePair:
                    Console.WriteLine("one pair");
                    break;
                case HandRank.HighCard:
                    Console.WriteLine("high card");
                    break;
            }
        }
    }
}
