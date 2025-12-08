namespace AdventOfCode2025
{
    public class Day5 : IDay
    {
        public string InputPath { get; set; } = "day5Input.txt";
        //public string InputPath { get; set; } = "day5InputTest.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);
            List<long[]> ranges = s
                .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Split("-")
                    .Select(long.Parse)
                    .ToArray())
                .ToList();

            List<long> productIds = s
                .TakeLast(s.Length - ranges.Count - 1)
                .Select(long.Parse)
                .ToList();

            int solution = productIds.Count(x => ranges.Any(y => y[0] <= x && x <= y[1]));

            // 513
            Console.WriteLine($"The number of ingredient IDs that are fresh is {solution}");
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);
            List<long[]> ranges = s
                .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Split("-")
                    .Select(long.Parse)
                    .ToArray())
                .ToList();

            ranges = ranges.OrderBy(x => x[0]).ToList();
            List<long[]> mergedRanges = [ranges[0]];
            for (int i = 1; i < ranges.Count; i++)
            {
                long[] current = ranges[i];
                // Fully contained
                if (current[1] <= mergedRanges[^1][1])
                    continue;

                // Overlapping the previous range
                if (current[0] <= mergedRanges[^1][1])
                    mergedRanges[^1][1] = current[1];
                // Not overlapping
                else
                    mergedRanges.Add(current);
            }

            // +1 bcs inclusive ranges
            long solution = mergedRanges.Sum(x => x[1] - x[0] + 1);

            // 6623
            Console.WriteLine($"The number of fresh ingredients according to the ID ranges is {solution}");
        }
    }
}
