using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day15 : IDay
    {
        //public string InputPath { get; set; } = "day15Input.txt";
        public string InputPath { get; set; } = "day15TestInput.txt";
        //public string InputPath { get; set; } = "day15TestInput2.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            List<List<char>> map = new();
            Queue<char> instructions = new();

            using (StreamReader f = new StreamReader(InputPath))
            {
                while (!f.EndOfStream
                    && f.ReadLine() is string line && line != "")
                    map.Add(line.ToCharArray().ToList());

                while (!f.EndOfStream)
                    foreach (var item in f.ReadLine()!)
                        instructions.Enqueue(item);
            }

            (int y, int x) robotPos = (0, 0);

            for (int i = 0; i < map.Count; i++)
                for (int j = 0; j < map[i].Count; j++)
                    if (map[i][j] == '@')
                    {
                        robotPos = (i, j);
                        break;
                    }

            Console.WriteLine(robotPos);

        }

        public void PartTwo()
        {
            throw new NotImplementedException();
        }
    }
}
