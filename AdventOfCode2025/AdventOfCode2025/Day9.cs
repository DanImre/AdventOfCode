using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using DataStructures;

namespace AdventOfCode2025
{
    public class Day9 : IDay
    {
        public string InputPath { get; set; } = "day9Input.txt";
        //public string InputPath { get; set; } = "day9InputTest.txt";

        public void PartOne()
        {
            long[][] s = File.ReadAllLines(InputPath)
                .Select(x => x.Split(",")
                    .Select(long.Parse)
                    .ToArray())
                .ToArray();

            long solution = 1;

            for (int i = 0; i < s.Length; i++)
                for (int j = i + 1; j < s.Length; j++)
                    solution = Math.Max(solution, (Math.Abs(s[i][0] - s[j][0]) + 1) * (Math.Abs(s[i][1] - s[j][1]) + 1));

            // 4760959496
            Console.WriteLine($"The largest area of any rectangle you can make is {solution}");
        }

        public void PartTwo()
        {
            long[][] s = File.ReadAllLines(InputPath)
                .Select(x => x.Split(",")
                    .Select(long.Parse)
                    .ToArray())
                .ToArray();

            List<Day9Line> lines = new();

            long solution = 1;

            for (int i = 1; i < s.Length; i++)
                lines.Add(new Day9Line(s[i - 1], s[i]));

            // closing the loop
            lines.Add(new Day9Line(s[^1], s[0]));

            for (int i = 0; i < s.Length; i++)
                for (int j = i + 1; j < s.Length; j++)
                {
                    if (lines.Any(x => x.IntersectsWithRect(s[i], s[j])))
                        continue;

                    long area = (Math.Abs(s[i][0] - s[j][0]) + 1) * (Math.Abs(s[i][1] - s[j][1]) + 1);
                    solution = Math.Max(solution, area);
                }

            // 1343576598
            Console.WriteLine($"The largest area of any rectangle you can make is {solution}");
        }
    }
}
