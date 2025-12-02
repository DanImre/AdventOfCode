using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public class Day2 : IDay
    {
        public string InputPath { get; set; } = "day2Input.txt";
        //public string InputPath { get; set; } = "day2InputTest.txt";

        public void PartOne()
        {
            List<string[]> ranges = File.ReadAllLines(InputPath)
                .SelectMany(x => x.Split(','))
                .Select(x => x.Split('-')
                    .Select(x => x.Trim())
                    .ToArray())
                .ToList();

            long solution = 0;

            foreach (var item in ranges)
            {
                int minLength = Math.Min(item[0].Length, item[1].Length);
                int maxLength = Math.Max(item[0].Length, item[1].Length);

                long startNum = long.Parse(item[0]);
                long endNum = long.Parse(item[1]);

                // Invalid range
                if (long.Parse(item[0]) > long.Parse(item[1]))
                    continue;

                // For ranges that have multiple even length number option like 12 - 1234 (has 2 and 4)
                for (int i = minLength; i <= maxLength; i++)
                {
                    if (i % 2 != 0)
                        continue;

                    long startingNumber = Math.Max((long)Math.Pow(10, i / 2 - 1), startNum);
                    long endingNumber = Math.Min((long)Math.Pow(10, i / 2), endNum);
                    for (long half = startingNumber; half < endingNumber; half++)
                    {
                        long number = long.Parse(half.ToString() + half.ToString());
                        solution += number;
                    }
                }
            }

            // 19386344315
            Console.WriteLine($"The sum of all the invalid IDs is {solution}");
        }

        public void PartTwo()
        {

        }
    }
}
