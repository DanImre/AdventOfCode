using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day7 : IDay
    {
        public string InputPath { get; set; } = "day7Input.txt";
        //public string InputPath { get; set; } = "day7TestInput.txt";

        public void PartOne()
        {
            List<long> evaluate(long[] numbers, int index)
            {
                List<long> solution = new() { numbers[index] };
                ++index;

                while (index < numbers.Length)
                {
                    long currentValue = numbers[index];
                    List<long> temp = new();
                    foreach (var item in solution)
                    {
                        temp.Add(item + currentValue);
                        temp.Add(item * currentValue);
                    }

                    index++;
                    solution = temp;
                }

                return solution;
            }

            long solution = 0;

            using (StreamReader f = new StreamReader(InputPath))
            {
                while (!f.EndOfStream)
                {
                    long[] numbers = f.ReadLine()!
                        .Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(long.Parse)
                        .ToArray();

                    List<long> values = evaluate(numbers, 1);

                    if (values.Contains(numbers[0]))
                        solution += numbers[0];
                }

            }

            Console.WriteLine($"Their total calibration result is: {solution}!"); // 10741443549536
        }

        public void PartTwo()
        {
            List<long> evaluate(long[] numbers, int index)
            {
                List<long> solution = new() { numbers[index] };
                ++index;

                while (index < numbers.Length)
                {
                    long currentValue = numbers[index];
                    List<long> temp = new();
                    foreach (var item in solution)
                    {
                        temp.Add(item + currentValue);
                        temp.Add(item * currentValue);
                        temp.Add(long.Parse($"{item}{currentValue}"));
                    }

                    index++;
                    solution = temp;
                }

                return solution;
            }

            long solution = 0;

            using (StreamReader f = new StreamReader(InputPath))
            {
                while (!f.EndOfStream)
                {
                    long[] numbers = f.ReadLine()!
                        .Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(long.Parse)
                        .ToArray();

                    List<long> values = evaluate(numbers, 1);

                    if (values.Contains(numbers[0]))
                        solution += numbers[0];
                }

            }

            Console.WriteLine($"Their total calibration result is: {solution}!"); // 500335179214836
        }
    }
}
