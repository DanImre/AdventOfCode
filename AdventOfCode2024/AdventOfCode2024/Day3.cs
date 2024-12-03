using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day3 : IDay
    {
        public string InputPath { get; set; } = "day3Input.txt";

        public void PartOne()
        {
            string input = string.Join("",File.ReadAllLines(InputPath));

            int solution = 0;

            MatchCollection matches = Regex.Matches(input, @"mul\([0-9]{1,3},[0-9]{1,3}\)");
            foreach (Match m in matches)
            {
                MatchCollection innerMatches = Regex.Matches(m.Value, @"[0-9]+");

                solution += int.Parse(innerMatches[0].Value) * int.Parse(innerMatches[1].Value);
            }

            Console.WriteLine($"Sum of the result of the multiplications is: {solution}"); // 189527826
        }

        public void PartTwo()
        {
            string input = string.Join("", File.ReadAllLines(InputPath));

            int solution = 0;

            MatchCollection doOrDontMaches = Regex.Matches(input, @"do\(\)|don't\(\)");

            MatchCollection mulMatches = Regex.Matches(input, @"mul\([0-9]{1,3},[0-9]{1,3}\)");

            // Did not need sorting
            //List<Match> doOrDontMachesList = doOrDontMaches.ToList();
            //List<Match> mulMatchesList = mulMatches.ToList();
            //doOrDontMachesList.Sort((a,b) => a.Index.CompareTo(b.Index));
            //mulMatchesList.Sort((a, b) => a.Index.CompareTo(b.Index));

            bool mulEnabled = true;
            int doIndex = 0;

            foreach (Match mulMatch in mulMatches)
            {
                while(doIndex < doOrDontMaches.Count && doOrDontMaches[doIndex].Index <= mulMatch.Index)
                {
                    mulEnabled = doOrDontMaches[doIndex].Value == "do()";
                    doIndex += 1;
                }

                if (!mulEnabled)
                    continue;

                MatchCollection innerMatches = Regex.Matches(mulMatch.Value, @"[0-9]+");

                solution += int.Parse(innerMatches[0].Value) * int.Parse(innerMatches[1].Value);
            }

            Console.WriteLine($"Sum of the result of the enabled multiplications is: {solution}"); // 63013756
        }
    }
}
