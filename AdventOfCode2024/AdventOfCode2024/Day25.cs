using System.Net.Http.Headers;
using System.Xml.Linq;

namespace AdventOfCode2024
{
    public class Day25 : IDay
    {
        public string InputPath { get; set; } = "day25Input.txt";
        //public string InputPath { get; set; } = "day25TestInput.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllText(InputPath).Replace("\r", "").Split("\n\n");

            List<(int[] heights, int availableSpace)> locks = [];
            List<(int[] heights, int neededSpace)> keys = [];

            foreach (var block in s)
            {
                if (block[0] == '#') // Lock
                {
                    string[] rows = [.. block.Split("\n", StringSplitOptions.RemoveEmptyEntries).Skip(1)];
                    int[] columnCounts = Enumerable.Range(0, rows[0].Length)
                      .Select(c => rows.Count(row => row[c] == '#'))
                      .ToArray();

                    locks.Add((columnCounts, rows.Length - 1));
                }
                else // Key
                {
                    string[] rows = [.. block.Split("\n", StringSplitOptions.RemoveEmptyEntries).Reverse().Skip(1)];
                    int[] columnCounts = Enumerable.Range(0, rows[0].Length)
                      .Select(c => rows.Count(row => row[c] == '#'))
                      .ToArray();

                    keys.Add((columnCounts, rows.Length - 1));
                }
            }

            int solution = 0;
            foreach (var l in locks)
                foreach (var k in keys.Where(k => k.neededSpace <= l.availableSpace && k.heights.Length == l.heights.Length))
                {
                    if (Enumerable.Range(0, l.heights.Length).All(i => l.heights[i] + k.heights[i] <= l.availableSpace))
                        solution++;
                }

            // For the test it should be: 3
            // 3089 too low
            // 11109 too high
            // 11109
            // 3088
            Console.WriteLine($"There are {solution} unique lock/key pairs fit together without overlapping in any column!");
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            long solution = 0;
            // For the test it should be: 2024
            // 48508229772400
            Console.WriteLine($"You get '{solution}' if you sort the names of the eight wires involved in a swap and then join those names with commas!");
        }
    }
}
