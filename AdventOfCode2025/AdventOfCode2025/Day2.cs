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

                    int digitCount = i / 2 - 1;
                    long startingNumber = (long)Math.Pow(10, digitCount);
                    long endingNumber = (long)Math.Pow(10, digitCount + 1);

                    for (long half = startingNumber; half <= endingNumber; half++)
                    {
                        long number = long.Parse(half.ToString() + half.ToString());

                        if (number < startNum)
                            continue;

                        if (number > endNum)
                            break;

                        solution += number;
                    }
                }
            }

            // 19386344315
            Console.WriteLine($"The sum of all the invalid IDs is {solution}");
        }

        public void PartTwo()
        {
            List<string[]> ranges = File.ReadAllLines(InputPath)
                .SelectMany(x => x.Split(','))
                .Select(x => x.Split('-')
                    .Select(x => x.Trim())
                    .ToArray())
                .ToList();

            long solution = 0;

            HashSet<long> foundNumbers = [];
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
                    // Getting possible sequence lengths
                    List<int> ps = PossibleRepeatingLengths(i);
                    foreach (var slen in ps)
                    {
                        int digitCount = i / slen - 1;
                        long startingNumber = (long)Math.Pow(10, digitCount);
                        long endingNumber = (long)Math.Pow(10, digitCount + 1);
                        for (long half = startingNumber; half < endingNumber; half++)
                        {
                            long number = long.Parse(string.Concat(Enumerable.Repeat(half.ToString(), slen)));

                            if (number < startNum)
                                continue;

                            if (number > endNum)
                                break;

                            if (foundNumbers.Add(number))
                                solution += number;
                        }
                    }
                }
            }

            // 34421651192
            Console.WriteLine($"The sum of all the invalid IDs (using the new rules) is {solution}");
        }

        private List<int> PossibleRepeatingLengths(int length)
        {
            List<int> solution = [];

            for (int i = 2; i <= length; i++)
                if (length % i == 0)
                    solution.Add(i);

            return solution;
        }
    }
}
