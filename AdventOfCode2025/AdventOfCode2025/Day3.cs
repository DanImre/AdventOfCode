using System.Linq;

namespace AdventOfCode2025
{
    public class Day3 : IDay
    {
        public string InputPath { get; set; } = "day3Input.txt";
        //public string InputPath { get; set; } = "day3InputTest.txt";

        public void PartOne()
        {
            List<List<int>> s = File.ReadAllLines(InputPath)
                    .Select(x => x.ToArray()
                        .Select(y => y - '0')
                        .ToList())
                    .ToList();

            int solution = 0;

            foreach (var line in s)
            {
                // In the task description: "you need to turn on exactly two batteries" so there are at least two batteries in each line
                int max = line[0];
                int secondMax = -1;

                for (int i = 1; i < line.Count - 1; i++)
                {
                    if (line[i] > max)
                    {
                        max = line[i];
                        secondMax = -1;
                        continue;
                    }

                    secondMax = Math.Max(line[i], secondMax);
                }

                secondMax = Math.Max(secondMax, line[^1]);

                solution += 10 * max + secondMax;
            }

            // 17443
            Console.WriteLine($"The total output joltage is {solution}");
        }

        public void PartTwo()
        {
            List<List<int>> s = File.ReadAllLines(InputPath)
                .Select(x => x.ToArray()
                    .Select(y => y - '0')
                    .ToList())
                .ToList();

            long solution = 0;
            int m = 12;

            foreach (var line in s)
            {
                int[] maxArray = new int[m];
                maxArray[0] = line[0];

                for (int i = 1; i < line.Count; i++)
                {
                    int indexesLeft = line.Count - i;

                    int index = Math.Max(m - indexesLeft, 0);
                    while (index < m && line[i] <= maxArray[index])
                        ++index;

                    if (index >= m)
                        continue;

                    maxArray[index] = line[i];

                    while (++index < m)
                        maxArray[index] = 0;
                }

                solution += long.Parse(string.Join("", maxArray.Select(x => x.ToString())));
            }

            // 172167155440541
            Console.WriteLine($"The total output joltage is {solution}");
        }
    }
}
