using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public class Day6 : IDay
    {
        public string InputPath { get; set; } = "day6Input.txt";
        //public string InputPath { get; set; } = "day6InputTest.txt";

        private Dictionary<string, Func<double, double, double>> operatorDict = new()
        {
            { "+", (x,y) => x + y },
            { "*", (x,y) => x * y},
            { "-", (x,y) => x - y},
            { "/", (x,y) => x / y},
        };

        public void PartOne()
        {
            string[][] s = File.ReadAllLines(InputPath).Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToArray();
            int n = s.Length;
            int m = s[0].Length;

            double solution = 0;
            for (int j = 0; j < m; j++)
            {
                string opStr = s[n - 1][j];
                Func<double, double, double> op = operatorDict[opStr];

                double problemSolution = double.Parse(s[0][j]);
                for (int i = 1; i < n - 1; i++)
                    problemSolution = op(problemSolution, double.Parse(s[i][j]));

                solution += problemSolution;
            }

            // 8108520669952
            Console.WriteLine($"The grand total found by adding together all of the answers to the individual problems is {solution}");
        }

        public void PartTwo()
        {
            char[][] s = File.ReadAllLines(InputPath)
                .Select(x => x
                    .ToCharArray()
                    .Prepend(' ')
                    .ToArray())
                .ToArray();

            List<(List<double> numbers, string op)> parsedNumbers = [];

            int m = s[0].Length;
            List<double> tempNumbers = [];
            for (int i = m - 1; i >= 0; i--)
            {
                string columnsAsString = string.Join("", s.Select(x => x[i]).Where(char.IsNumber));
                if (columnsAsString == "")
                {
                    parsedNumbers.Add((tempNumbers, s[^1][i + 1].ToString()));
                    tempNumbers = [];
                    continue;
                }

                tempNumbers.Add(double.Parse(columnsAsString));
            }

            // The columns are now in a row
            int n = parsedNumbers.Count;
            double solution = 0;
            for (int i = 0; i < n; i++)
            {
                Func<double, double, double> op = operatorDict[parsedNumbers[i].op];

                double problemSolution = parsedNumbers[i].numbers[0];
                foreach (var item in parsedNumbers[i].numbers.Skip(1))
                    problemSolution = op(problemSolution, item);
                    
                solution += problemSolution;
            }

            // 11708563470209
            Console.WriteLine($"The grand total found by adding together all of the answers to the individual problems is {solution}");
        }
    }
}
