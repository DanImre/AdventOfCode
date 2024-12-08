using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day5 : IDay
    {
        public string InputPath { get; set; } = "day5Input.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            Dictionary<string, int> compareFuncValues = new Dictionary<string, int>();

            int solution = 0;

            using (StreamReader f = new StreamReader(InputPath))
            {
                string? line = f.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    compareFuncValues.Add(line!, -1);
                    compareFuncValues.Add(string.Join("|", line!.Split('|').Reverse()), 1);
                    line = f.ReadLine();
                }

                while (!f.EndOfStream)
                {
                    line = f.ReadLine();

                    string[] numbers = line!.Split(',').ToArray();

                    bool correct = true;

                    for (int i = 0; i < numbers.Length - 1 && correct; i++)
                    {
                        string key = $"{numbers[i]}|{numbers[i + 1]}";
                        correct = compareFuncValues.ContainsKey(key) ? compareFuncValues[key] != -1 : true;
                    }

                    if (correct)
                        solution += int.Parse(numbers[numbers.Length / 2]);
                }
            }

            Console.WriteLine($"The sum of the middle page numbers from the correctly-ordered updates is: {solution}"); // 5275
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            Dictionary<string, int> compareFuncValues = new Dictionary<string, int>();

            int solution = 0;

            using (StreamReader f = new StreamReader(InputPath))
            {
                string? line = f.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    compareFuncValues.Add(line!, -1);
                    compareFuncValues.Add(string.Join("|", line!.Split('|').Reverse()), 1);
                    line = f.ReadLine();
                }

                while (!f.EndOfStream)
                {
                    line = f.ReadLine();

                    List<string> numbers = line!.Split(',').ToList();

                    numbers.Sort((a, b) => compareFuncValues.GetValueOrDefault($"{a}|{b}", 0));

                    if(string.Join(",",numbers) != line)
                        solution += int.Parse(numbers[numbers.Count / 2]);
                }
            }

            Console.WriteLine($"The sum of the incorrectly ordered updates's middle page numbers after correctly ordering them is: {solution}"); // 6191
        }
    }
}
