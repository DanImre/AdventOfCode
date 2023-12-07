using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day7
    {
        public Day7()
        {

        }

        public class Hand : IComparable<Hand>
        {
            public int[] Cards { get; private set; }
            public int Worth { get; private set; }
            public Hand(int[] cards, int worth)
            {
                Cards = cards;
                Worth = worth;
            }

            public int CompareTo(Hand? other)
            {
                if (other == null)
                    throw new ArgumentException("Can't compare type null with type Hand");
                else if (other is not Hand)
                    throw new ArgumentException("Can't compare type '" + other.GetType() + "' with type 'Hand'");

                for (int i = 0; i < 5; i++)
                {
                    if (this.Cards[i] > other.Cards[i])
                        return -1;
                    if (this.Cards[i] < other.Cards[i])
                        return 1;
                }

                return 0;
            }
        }

        public long PartOne()
        {
            //string[] s = File.ReadAllLines("day7test.txt");
            string[] s = File.ReadAllLines("day7.txt");

            List<Hand>[] handTypes = new List<Hand>[7];
            for (int i = 0; i < 7; i++)
                handTypes[i] = new List<Hand>();

            foreach (var item in s)
            {
                string[] temp = item.Split(" ").ToArray();

                int[] cards = temp[0].Select(kk => getValue(kk)).ToArray();
                int[] distinctCards = cards.Distinct().ToArray();

                Hand hand = new Hand(cards, int.Parse(temp[1]));

                //Five of a kind
                if (distinctCards.Length == 1)
                    handTypes[0].Add(hand);
                //Four of a kind
                else if (distinctCards.Length == 2 
                    && distinctCards.Any(kk => cards.Count(zz => zz == kk) == 4))
                    handTypes[1].Add(hand);
                //Full house
                else if (distinctCards.Length == 2
                    && distinctCards.All(kk => cards.Count(zz => zz == kk) >= 2))
                    handTypes[2].Add(hand);
                //Three of a kind
                else if (distinctCards.Length == 3
                    && distinctCards.Any(kk => cards.Count(zz => zz == kk) == 3))
                    handTypes[3].Add(hand);
                //Two pair
                else if (distinctCards.Length == 3)
                    //&& distinctCards.Count(kk => cards.Count(zz => zz == kk) == 2) == 2)
                    handTypes[4].Add(hand);
                //pair
                else if (distinctCards.Length == 4)
                    //&& distinctCards.Any(kk => cards.Count(zz => zz == kk) == 2))
                    handTypes[5].Add(hand);
                else
                    handTypes[6].Add(hand);
            }

            long solution = 0;

            int n = s.Length;

            for (int i = 0; i < 7; i++)
            {
                handTypes[i].Sort();
                foreach (var item in handTypes[i])
                    solution += item.Worth * n--;
            }

            return solution;
        }

        private int getValue(char c)
        {
            if ('2' <= c && c <= '9')
                return c - '0';

            switch (c)
            {
                case 'T':
                    return 10;
                case 'J':
                    return 11;
                case 'Q':
                    return 12;
                case 'K':
                    return 13;
                default: //A
                    return 14;
            }
        }
        public long PartTwo() //249764857 too high
        {
            //string[] s = File.ReadAllLines("day7test.txt");
            string[] s = File.ReadAllLines("day7.txt");

            List<Hand>[] handTypes = new List<Hand>[7];
            for (int i = 0; i < 7; i++)
                handTypes[i] = new List<Hand>();

            foreach (var item in s)
            {
                string[] temp = item.Split(" ").ToArray();

                int[] cards = temp[0].Select(kk => getValueForPartTwo(kk)).ToArray();
                int[] distinctCards = cards.Distinct().ToArray();

                Hand hand = new Hand(cards, int.Parse(temp[1]));

                //Five of a kind
                if (distinctCards.Length == 1
                    || distinctCards.Length == 2 && distinctCards.Contains(0))
                    handTypes[0].Add(hand);
                //Four of a kind
                else if (distinctCards.Length <= 3
                    && distinctCards.Any(kk => cards.Count(zz => zz == kk || zz == 0) == 4))
                    handTypes[1].Add(hand);
                //Full house

                //If there are 3 jokers and 2 other cards its a four of a kind,
                //If there are 2 jokers with [3 cards with 2 types] then that has to be a four of a kind
                
                //If there is 1 joker with [4 cards with 2 type] and there are 2 of each type its a full house, otherwise its a four of a kind
                else if ((distinctCards.Length == 2
                    && distinctCards.All(kk => cards.Count(zz => zz == kk) >= 2))
                    || (distinctCards.Length == 3 
                    && distinctCards.Contains(0)
                    && distinctCards.Where(kk => kk != 0).All(kk => cards.Count(zz => zz == kk) == 2))) 
                    handTypes[2].Add(hand);
                //Three of a kind
                else if ((distinctCards.Length == 3
                    && distinctCards.Any(kk => cards.Count(zz => zz == kk) == 3))
                    || (distinctCards.Length == 4
                    && distinctCards.Any(kk => cards.Count(zz => zz == kk || zz == 0) == 3)))
                    handTypes[3].Add(hand);
                //Two pair
                else if (distinctCards.Length == 3
                    //&& distinctCards.Count(kk => cards.Count(zz => zz == kk) == 2) == 2)
                    || (distinctCards.Length == 4 && distinctCards.Contains(0)))
                    handTypes[4].Add(hand);
                //pair
                else if (distinctCards.Length == 4
                    //&& distinctCards.Any(kk => cards.Count(zz => zz == kk) == 2))
                    || distinctCards.Contains(0))
                    handTypes[5].Add(hand);
                else
                    handTypes[6].Add(hand);
            }

            long solution = 0;

            int n = s.Length;

            for (int i = 0; i < 7; i++)
            {
                handTypes[i].Sort();
                foreach (var item in handTypes[i])
                    solution += item.Worth * n--;
            }

            return solution;
        }

        private int getValueForPartTwo(char c)
        {
            if ('2' <= c && c <= '9')
                return c - '0';

            switch (c)
            {
                case 'T':
                    return 10;
                case 'J':
                    return 0; //Least valuable card
                case 'Q':
                    return 12;
                case 'K':
                    return 13;
                default: //A
                    return 14;
            }
        }
    }
}
