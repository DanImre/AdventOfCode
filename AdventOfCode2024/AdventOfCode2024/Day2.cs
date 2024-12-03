using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day2 : IDay
    {
        public string InputPath { get; set; } = "day2Input.txt";

        public void PartOne()
        {
            int[][] reports = File.ReadAllLines(InputPath).Select(kk => kk.Split(' ').Select(int.Parse).ToArray()).ToArray();

            int solution = reports.Count(report =>
            {
                if (report.Length <= 1)
                    return true;

                int diff = report[1] - report[0];
                bool increasing = diff > 0;

                for (int i = 0; i < report.Length - 1; i++)
                {
                    diff = report[i + 1] - report[i];
                    if (increasing != (diff > 0))
                        return false;

                    diff = Math.Abs(diff);
                    if (diff == 0 || diff > 3)
                        return false;
                }

                return true;
            });

            Console.WriteLine($"{solution} reports are safe.");
        }

        public void PartTwo()
        {
            int[][] reports = File.ReadAllLines(InputPath).Select(kk => kk.Split(' ').Select(int.Parse).ToArray()).ToArray();

            int solution = reports.Count(report =>
            {
                bool IsCorrectSequence(int currentIndex, int nextIndex, bool canSkip, bool increasing)
                {
                    if (nextIndex >= report.Length) // got to the end, this means currentIndex is the last considered level
                        return true;

                    if (currentIndex < 0) // skipped the first one -> start from the second one
                        return IsCorrectSequence(1, 2, canSkip, increasing);

                    int diff = report[nextIndex] - report[currentIndex];
                    if (increasing != (diff > 0)
                    || diff == 0 || Math.Abs(diff) > 3)
                        return canSkip
                        && (IsCorrectSequence(currentIndex, nextIndex + 1, false, increasing) // skipping the nextIndex level
                        || IsCorrectSequence(currentIndex - 1, nextIndex, false, increasing)); // skipping the currentIndex level

                    return IsCorrectSequence(nextIndex, nextIndex + 1, canSkip, increasing); // Correct pair, go next
                }

                // leaving out the first or second could change if it's increasing or not.
                return IsCorrectSequence(0, 1, true, true) // check for increasing
                || IsCorrectSequence(0, 1, true, false); // check for decreasing
            });

            Console.WriteLine($"{solution} reports are safe."); //404

            //Final tought: If the sequences were larger memoizing wouldn't be a bad idea
        }
    }
}
