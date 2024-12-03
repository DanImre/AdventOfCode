using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day1 : IDay
    {
        public string InputPath { get; set; } = "day1Input.txt";

        public void PartOne()
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>();
            using (StreamReader f = new StreamReader(InputPath))
            {
                while (!f.EndOfStream)
                {
                    int[] split = f.ReadLine()!.Split("   ").Select(int.Parse).ToArray();
                    left.Add(split[0]);
                    right.Add(split[1]);
                }
            }

            left.Sort();
            right.Sort();

            int solution = 0;
            for (int i = 0; i < left.Count; i++)
                solution += Math.Abs(right[i] - left[i]);

            Console.WriteLine($"The total distance between my lists is: {solution}!"); // 936063
        }

        public void PartTwo()
        {
            Dictionary<int, int> left = new Dictionary<int, int>();
            List<int> right = new List<int>();
            using (StreamReader f = new StreamReader(InputPath))
            {
                while (!f.EndOfStream)
                {
                    int[] split = f.ReadLine()!.Split("   ").Select(int.Parse).ToArray();
                    left.Add(split[0], 0);
                    right.Add(split[1]);
                }
            }

            foreach (var item in right)
                if (left.ContainsKey(item))
                    left[item] += 1;

            int solution = left.Sum(kk => kk.Key * kk.Value);
            Console.WriteLine($"The total distance between my lists is: {solution}!"); // 23150395
        }
    }
}
