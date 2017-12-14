using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    public class PokerHand
    {
        public static int[][] totalValuesPlayers = new int[2][];
        public static int[] valueCountPlayers = new int[2];
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
            }
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
                            Console.WriteLine("Player 1");
                            break;
                        }
                        if (totalValuesPlayers[1][i] == 4)
                        {
                            Console.WriteLine("Player 2");
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
                                    Console.WriteLine("Player 1");
                                    break;
                                }
                                else if (totalValuesPlayers[1][j] == 2)
                                {
                                    Console.WriteLine("Player 2");
                                    break;
                                }
                            }
                            break;
                        }

                        if (totalValuesPlayers[0][i] == 3 && totalValuesPlayers[1][i] != 3)
                        {
                            Console.WriteLine("Player 1");
                            break;
                        }
                        else if (totalValuesPlayers[1][i] == 3 && totalValuesPlayers[0][i] != 3)
                        {
                            Console.WriteLine("Player 2");
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
                            Console.WriteLine("Player 1");
                            break;
                        }
                        else if (totalValuesPlayers[1][i] == 3)
                        {
                            Console.WriteLine("Player 2");
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
                            Console.WriteLine("Player 1");
                            break;
                        }
                        else if (totalValuesPlayers[1][i] == 2)
                        {
                            Console.WriteLine("Player 2");
                            break;
                        }
                    }
                    break;
            }
        }

        public static void getRankByValue()
        {
            for (int i = 14; i > 0; i--)
            {
                if (totalValuesPlayers[0][i] > totalValuesPlayers[1][i])
                {
                    Console.WriteLine("Player 1");
                    break;
                }
                else if (totalValuesPlayers[0][i] < totalValuesPlayers[1][i])
                {
                    Console.WriteLine("Player 2");
                    break;
                }
            }
        }
        static public void PokerHandMain()
        {
            int testCase = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < testCase; i++)
            {
                string playersHand = Console.ReadLine();
                totalValuesPlayers = new int[2][];
                valueCountPlayers = new int[2];
                Rank player1Rank = calculateRank(playersHand.Substring(0, 14));
                Rank player2Rank = calculateRank(playersHand.Substring(15, 14));
                if (player1Rank > player2Rank)
                    Console.WriteLine("Player 1");
                else if (player1Rank < player2Rank)
                    Console.WriteLine("Player 2");

                if (player1Rank == player2Rank)
                {
                    calcuateHighestInSameRank(player1Rank, playersHand);
                }
            }
        }
    }
}
