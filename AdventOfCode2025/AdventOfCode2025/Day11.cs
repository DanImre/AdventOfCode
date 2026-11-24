using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public class Day11 : IDay
    {
        public void PartOne(string inputPath)
        {
            Dictionary<string, string[]> s = File.ReadAllLines(inputPath)
                    .Select(x => x.Split(":"))
                    .ToDictionary(
                        x => x[0], 
                        x => x[1]
                            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .ToArray());

            Dictionary<string, int> memo = new Dictionary<string, int>()
            {
                { "out", 1 }
            };
            int recursiveSolution(string key)
            {
                if(memo.ContainsKey(key))
                    return memo[key];

                int sum = 0;
                foreach (string item in s[key])
                    sum += recursiveSolution(item);

                memo[key] = sum;
                return sum;
            }

            int solution = recursiveSolution("you");

            // 1132
            Console.WriteLine($"There are {solution} different paths from 'you' to 'out'.");
        }

        public void PartTwo(string inputPath)
        {
            Dictionary<string, string[]> s = File.ReadAllLines(inputPath)
                    .Select(x => x.Split(":"))
                    .ToDictionary(
                        x => x[0],
                        x => x[1]
                            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .ToArray());

            Dictionary<(string, bool, bool), long> memo = [];
            long recursiveSolution(string key, bool hitDAC, bool hitFFT)
            {
                if(key == "out")
                    return hitDAC && hitFFT ? 1 : 0;

                if(memo.ContainsKey((key, hitDAC, hitFFT)))
                    return memo[(key, hitDAC, hitFFT)];

                long sum = 0;
                foreach (string item in s[key])
                {
                    bool currHitDAC = hitDAC || item == "dac";
                    bool currHitFFT = hitFFT || item == "fft";
                    sum += recursiveSolution(item, currHitDAC, currHitFFT);
                }

                memo[(key, hitDAC, hitFFT)] = sum;
                return sum;
            }

            long solution = recursiveSolution("svr", false, false);

            // 545394698933400
            Console.WriteLine($"There are {solution} different paths from 'srv' to 'out' that visit both 'dac' and 'fft'.");
        }
    }
}
