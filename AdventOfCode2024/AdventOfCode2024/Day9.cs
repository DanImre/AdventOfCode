using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day9 : IDay
    {
        public string InputPath { get; set; } = "day9Input.txt";
        //public string InputPath { get; set; } = "day9TestInput.txt";

        public void PartOne()
        {
            string s = File.ReadAllLines(InputPath).First();
            List<int> unraveled = new List<int>();
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i] - '0'; j++)
                    if (i % 2 == 0)
                        unraveled.Add(i / 2);
                    else
                        unraveled.Add(-1);

            int start = 0;
            int end = unraveled.Count - 1;
            while (true)
            {
                while (unraveled[start] != -1)
                    ++start;

                while (unraveled[end] == -1)
                    --end;

                if (start >= end)
                    break;

                while (unraveled[start] == -1
                    && unraveled[end] != -1)
                {
                    unraveled[start++] = unraveled[end];
                    unraveled[end--] = -1;
                }
            }

            // Checksum
            long solution = 0;
            for (int i = 0; i <= end; i++)
                if (unraveled[i] != -1)
                    solution += unraveled[i] * i;

            Console.WriteLine($"The checksum of the resulting filesystem is: {solution}"); // 6310675819476
        }

        public void PartTwo()
        {
            string s = File.ReadAllLines(InputPath).First();
            List<int> unraveled = new List<int>();
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i] - '0'; j++)
                    if (i % 2 == 0)
                        unraveled.Add(i / 2);
                    else
                        unraveled.Add(-1);

            int end = unraveled.Count - 1;
            while (end > 0)
            {
                while (unraveled[end] == -1)
                    --end;

                int lengthOfTheBlock = 1;
                while (end - lengthOfTheBlock >= 0 && unraveled[end - lengthOfTheBlock] == unraveled[end])
                    ++lengthOfTheBlock;

                int start = 0;
                bool foundEnoughSpace = false;
                while (true)
                {
                    while (unraveled[start] != -1)
                        ++start;

                    if (start >= end)
                        break;

                    int lengthOfFreeSpace = 1;
                    while (start + lengthOfFreeSpace < unraveled.Count && unraveled[start + lengthOfFreeSpace] == -1)
                        ++lengthOfFreeSpace;

                    if (lengthOfFreeSpace >= lengthOfTheBlock)
                    {
                        foundEnoughSpace = true;
                        for (int i = 0; i < lengthOfTheBlock; i++)
                        {
                            unraveled[start++] = unraveled[end];
                            unraveled[end--] = -1;
                        }
                        break;
                    }
                    if (!foundEnoughSpace)
                        start += lengthOfFreeSpace;
                }
                if (!foundEnoughSpace)
                    end -= lengthOfTheBlock;
            }

            // Checksum
            long solution = 0;
            for (int i = 0; i < unraveled.Count; i++)
                if (unraveled[i] != -1)
                    solution += unraveled[i] * i;

            Console.WriteLine($"The checksum of the resulting filesystem is: {solution}"); // 6335972980679
        }
    }
}
