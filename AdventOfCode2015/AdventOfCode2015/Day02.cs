using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day02 : IDay
    {
        public string InputPath { get; set; } = "Day2Input.txt";

        public void PartOne()
        {
            //PartOneCompact();
            //return;
            // [l,w,h] list:
            List<List<int>> input = File.ReadAllLines(InputPath)
                .Select(x => x
                    .Split('x')
                    .Select(int.Parse)
                    .ToList())
                .ToList();

            long solution = 0;

            foreach (var item in input)
            {
                int l = item[0];
                int w = item[1];
                int h = item[2];

                solution += 2 * l * w
                    + 2 * w * h
                    + 2 * h * l;

                item.Sort();
                int firstSmallestSide = item[0];
                int secondSmallestSide = item[1];

                int slack = firstSmallestSide * secondSmallestSide;
                solution += slack;
            }

            Console.WriteLine($"THe total square feet of wrapping paper should be: {solution}");
        }

        public void PartOneCompact()
        {
            // [l,w,h] list:
            List<List<int>> input = File.ReadAllLines(InputPath)
                .Select(x => x
                    .Split('x')
                    .Select(int.Parse)
                    .ToList())
                .ToList();

            long solution = 0;
            foreach (var item in input)
            {
                for (int i = 0; i < item.Count; i++)
                    solution += 2 * item[i] * item[i % item.Count];

                item.Sort();
                solution += item[0] * item[1];
            }

            Console.WriteLine($"THe total square feet of wrapping paper should be: {solution}");
        }

        public void PartTwo()
        {
            // [l,w,h] list:
            List<List<int>> input = File.ReadAllLines(InputPath)
                .Select(x => x
                    .Split('x')
                    .Select(int.Parse)
                    .ToList())
                .ToList();

            long solution = 0;

            foreach (var item in input)
            {
                item.Sort();
                int firstSmallestSide = item[0];
                int secondSmallestSide = item[1];

                solution += 2* firstSmallestSide // The shortest distance around its sides
                    + 2 * secondSmallestSide
                    + item[0] * item[1] * item[2]; // The cubic feet of volume of the present
            }

            Console.WriteLine($"They should order a total of {solution} feet of ribbon.");
        }
    }
}
