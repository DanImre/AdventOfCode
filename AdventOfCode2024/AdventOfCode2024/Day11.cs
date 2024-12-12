using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LinkedListNode = DataStructures.LinkedListNode<long>;

namespace AdventOfCode2024
{
    public class Day11 : IDay
    {
        public string InputPath { get; set; } = "day11Input.txt";
        //public string InputPath { get; set; } = "day11TestInput.txt";

        public void PartOne()
        {
            string s = File.ReadAllLines(InputPath).First();

            LinkedListNode head = new LinkedListNode(-1);

            LinkedListNode? curr = head;
            foreach (var item in s.Split(' ').Select(int.Parse))
            {
                curr.Next = new LinkedListNode(item);
                curr.Next.Prev = curr;
                curr = curr.Next;
            }

            for (int i = 0; i < 25; i++)
            {
                curr = head.Next;

                while (curr != null)
                {
                    if (curr.Value == 0)
                        curr.Value = 1;
                    else if (curr.Value.ToString() is string stringValue && stringValue.Length % 2 == 0)
                    {
                        LinkedListNode prev = curr.Prev!;
                        prev.Next = new LinkedListNode(int.Parse(stringValue.Substring(0, stringValue.Length / 2)));
                        prev.Next.Prev = prev;

                        prev = prev.Next;
                        prev.Next = curr;
                        curr.Prev = prev;
                        curr.Value = int.Parse(stringValue.Substring(stringValue.Length / 2));
                    }
                    else
                        curr.Value *= 2024;

                    curr = curr.Next;
                }
            }

            int solution = 0;
            curr = head.Next;
            while (curr != null)
            {
                ++solution;
                curr = curr.Next;
            }

            Console.WriteLine($"There will be a total of {solution} rocks after 25 blinks."); // 193607
        }

        public void PartTwo()
        {
            string s = File.ReadAllLines(InputPath).First();

            Dictionary<long, long> amountOfRocks = new();

            foreach (var item in s.Split(' ').Select(long.Parse))
            {
                if (!amountOfRocks.ContainsKey(item))
                    amountOfRocks.Add(item, 1);
                else
                    amountOfRocks[item] += 1;
            }


            for (int i = 0; i < 75; i++)
            {
                Dictionary<long, long> newRocks = new();

                foreach (var item in amountOfRocks.Keys)
                {
                    if (item == 0)
                    {
                        if (!newRocks.ContainsKey(1))
                            newRocks.Add(1, amountOfRocks[item]);
                        else
                            newRocks[1] += amountOfRocks[item];
                    }
                    else if (item.ToString() is string stringValue && stringValue.Length % 2 == 0)
                    {
                        long newItem = long.Parse(stringValue.Substring(0, stringValue.Length / 2));
                        if (!newRocks.ContainsKey(newItem))
                            newRocks.Add(newItem, amountOfRocks[item]);
                        else
                            newRocks[newItem] += amountOfRocks[item];

                        newItem = long.Parse(stringValue.Substring(stringValue.Length / 2));
                        if (!newRocks.ContainsKey(newItem))
                            newRocks.Add(newItem, amountOfRocks[item]);
                        else
                            newRocks[newItem] += amountOfRocks[item];
                    }
                    else
                    {
                        long newItem = item * 2024;
                        if (!newRocks.ContainsKey(newItem))
                            newRocks.Add(newItem, amountOfRocks[item]);
                        else
                            newRocks[newItem] += amountOfRocks[item];
                    }
                }

                amountOfRocks = newRocks;
            }

            long solution = amountOfRocks.Sum(kk => kk.Value);

            Console.WriteLine($"There will be a total of {solution} rocks after 75 blinks."); // 229557103025807
        }
    }
}
