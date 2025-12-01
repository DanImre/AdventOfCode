using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day19 : IDay
    {
        public string InputPath { get; set; } = "day19Input.txt";
        //public string InputPath { get; set; } = "day19TestInput.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            string[] availablePatterns = s[0].Split(", ").ToArray();

            int solution = 0;

            for (int i = 2; i < s.Length; i++)
            {
                HashSet<string> seen = new();
                Queue<string> q = new();
                q.Enqueue(s[i]);

                while (q.Count > 0)
                {
                    string curr = q.Dequeue();

                    if (!seen.Add(curr))
                        continue;

                    if (curr == "")
                        break;

                    foreach (var pattern in availablePatterns)
                    {
                        bool usable = pattern.Length <= curr.Length;
                        for (int j = 0; j < pattern.Length && usable; j++)
                            usable &= pattern[j] == curr[j];

                        if (usable)
                            q.Enqueue(curr.Substring(pattern.Length));
                    }
                }

                if (seen.Contains(""))
                    solution++;
            }

            Console.WriteLine($"{solution} designs are possible."); // 308
        }

        public void PartTwo()
        {
            // Idea: Same but with a dictionary -> move the same patterns at once
            string[] s = File.ReadAllLines(InputPath);

            string[] availablePatterns = s[0].Split(", ").ToArray();

            long solution = 0;

            for (int i = 2; i < s.Length; i++)
            {
                Dictionary<string, long> dict = new();
                dict.Add(s[i], 1);

                while (dict.Count > 0)
                {
                    Dictionary<string, long> newDict = new();
                    foreach (var item in dict)
                    {
                        if (item.Key == "")
                        {
                            solution += item.Value;
                            continue;
                        }

                        foreach (var pattern in availablePatterns)
                        {
                            bool usable = pattern.Length <= item.Key.Length;
                            for (int j = 0; j < pattern.Length && usable; j++)
                                usable &= pattern[j] == item.Key[j];

                            if (!usable)
                                continue;

                            string newKey = item.Key.Substring(pattern.Length);

                            if(!newDict.ContainsKey(newKey))
                                newDict.Add(newKey, item.Value);
                            else
                                newDict[newKey] += item.Value;
                        }
                    }

                    dict = newDict;
                }
            }

            Console.WriteLine($"{solution} designs are possible."); // 768901863194 too low 662726441391898
        }
    }
}
