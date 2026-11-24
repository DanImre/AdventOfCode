using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day05 : IDay
    {
        public void PartOne(string inputPath)
        {
            string[] s = File.ReadAllLines(inputPath);
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            HashSet<string> bannedSubstrings = new HashSet<string> { "ab", "cd", "pq", "xy" };

            int solution = 0;

            foreach (var item in s)
            {
                int vowelCount = item.Count(vowels.Contains);
                if (vowelCount < 3)
                    continue;

                bool hasDoubleLetter = false;
                bool hasBannedWord = false;
                for (int i = 0; i < item.Length - 1; i++)
                {
                    hasDoubleLetter |= item[i] == item[i + 1];
                    if (bannedSubstrings.Contains(item.Substring(i, 2)))
                    {
                        hasBannedWord = true;
                        break;
                    }
                }

                if (!hasBannedWord && hasDoubleLetter)
                    solution++;
            }

            Console.WriteLine($"There are {solution} nice strings.");
        }

        public void PartTwo(string inputPath)
        {
            string[] s = File.ReadAllLines(inputPath);

            int solution = 0;
            foreach (var row in s)
            {
                if (row.Length <= 3)
                    continue;

                Dictionary<string, int> pairLocation = [];
                bool passesFirst = false;
                bool passesSecond = false;
                for (int i = 0; i < row.Length - 1; i++)
                {
                    // First check
                    if (!passesFirst)
                    {
                        var subS = row.Substring(i, 2);
                        if (pairLocation.TryGetValue(subS, out int val))
                        {
                            if (i - val >= 2)
                                passesFirst = true;
                        }
                        else
                            pairLocation.Add(subS, i);
                    }

                    // Second check
                    if (!passesSecond
                        && i < row.Length - 2
                        && row[i + 2] == row[i])
                    {
                        passesSecond = true;
                    }

                    if (passesFirst && passesSecond)
                    {
                        ++solution;
                        break;
                    }
                }
            }

            Console.WriteLine($"There are {solution} nice strings under the new rules.");
        }
    }
}
