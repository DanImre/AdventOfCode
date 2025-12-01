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
        public string InputPath { get; set; } = "Day5Input.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
            HashSet<string> bannedSubstrings = new HashSet<string> { "ab", "cd", "pq", "xy" };

            int solution = 0;

            foreach (var item in s)
            {
                int vowelCount = item.Count(vowels.Contains);
                if(vowelCount < 3)
                    continue;

                bool hasDoubleLetter = false;
                bool hasBannedWord = false;
                for (int i = 0; i < item.Length-1; i++)
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

        public void PartTwo()
        {
            Dictionary<string, object> asd = new Dictionary<string, object>()
            {
                { "asd", 123 },
                { "qwe", "hello" }
            };

            var asd_obj = new { asd = 123, qwe = "hello" };

            Console.WriteLine(JsonSerializer.Serialize(asd));
            Console.WriteLine(JsonSerializer.Serialize(asd_obj));

            //Console.WriteLine($"\nThe lowest positive number to produce a hash with 6 starting zeros is: {solution}.");
        }
    }
}
