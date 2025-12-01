using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public class Day1 : IDay
    {
        public string InputPath { get; set; } = "day1Input.txt";
        //public string InputPath { get; set; } = "day1InputTest.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            int solution = 0;

            int rotation = 50;
            foreach (string item in s)
            {
                if (item[0] == 'R')
                    rotation += int.Parse(item.Substring(1));
                else
                    rotation -= int.Parse(item.Substring(1));

                rotation %= 100;
                if (rotation < 0)
                    rotation += 100;

                if (rotation == 0)
                    solution++;
            }

            // 1132
            Console.WriteLine($"The actual password to open the door is {solution}");
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            int solution = 0;

            int rotation = 50;
            foreach (string item in s)
            {
                bool rotationStartedAtZero = rotation == 0;
                if (item[0] == 'R')
                    rotation += int.Parse(item.Substring(1));
                else
                    rotation -= int.Parse(item.Substring(1));

                solution += !rotationStartedAtZero && rotation <= 0 ? 1 : 0;
                solution += Math.Abs(rotation / 100);
                rotation %= 100;
                if(rotation < 0)
                    rotation += 100;
            }

            // 6623
            Console.WriteLine($"The password to open the door (with the method 0x434C49434B) is {solution}");
        }
    }
}
