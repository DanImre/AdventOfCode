using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public class Day1 : IDay
    {
        public string InputPath { get; set; } = "day1Input.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            int solution = 0;

            int rotation = 0;
            foreach (string item in s)
            {
                if (item[0] == 'R')
                    rotation += int.Parse(item.Substring(1));
                else
                    rotation -= int.Parse(item.Substring(1));

                rotation %= 100;
                if(rotation == 0)
                    solution++;
            }

            Console.WriteLine($"The actual password to open the door is {solution}");
        }

        public void PartTwo()
        {

        }
    }
}
