using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day01 : IDay
    {
        public string InputPath { get; set; } = "Day1Input.txt";

        public void PartOne()
        {
            string input = File.ReadAllLines(InputPath)[0];
            Console.WriteLine($"The instructions take Santa the the {input.Count(x => x == '(') - input.Count(x => x == ')')}th floor!"); 
        }

        public void PartTwo()
        {
            string input = File.ReadAllLines(InputPath)[0];

            int floor = 0;
            for (int i = 0; i < input.Length; i++)
                if (input[i] == '(')
                    floor++;
                else if (--floor == -1)
                {
                    Console.WriteLine(i + 1);
                    return;
                }

            Console.WriteLine($"The position of the character that first causes Santa to enter the basement is: {input.Length.ToString()}");
        }
    }
}
