using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public class Day4 : IDay
    {
        public string InputPath { get; set; } = "day4Input.txt";
        //public string InputPath { get; set; } = "day4InputTest.txt";

        private List<(int x, int y)> directions = new()
        {
            (0, 1),
            (1, 0),
            (0, -1),
            (-1, 0),
            (1, 1),
            (1, -1),
            (-1, 1),
            (-1, -1)
        };

        public void PartOne()
        {
            char[][] s = File.ReadAllLines(InputPath).Select(x => x.ToArray()).ToArray();

            List<(int x, int y)> countedLocations = new();

            int solution = 0;
            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] != '@')
                        continue;

                    int adjacentCount = 0;
                    foreach (var item in directions)
                    {
                        int newX = j + item.x;
                        int newY = i + item.y;
                        if (newX < 0
                            || newY < 0
                            || newY >= s.Length
                            || newX >= s[newY].Length
                            || s[newY][newX] != '@')
                            continue;

                        adjacentCount++;
                    }

                    if (adjacentCount < 4)
                        solution++;
                }
            }

            // 1376
            Console.WriteLine($"{solution} rolls of paper can be accessed by a forklift");
        }

        public void PartTwo()
        {
            char[][] s = File.ReadAllLines(InputPath).Select(x => x.ToArray()).ToArray();
            int solution = 0;

            bool stillGoing = true;
            while (stillGoing)
            {
                stillGoing = false;
                List<(int x, int y)> toChange = new();
                for (int i = 0; i < s.Length; i++)
                    for (int j = 0; j < s[i].Length; j++)
                    {
                        if (s[i][j] != '@')
                            continue;

                        int adjacentCount = 0;
                        foreach (var item in directions)
                        {
                            int newX = j + item.x;
                            int newY = i + item.y;
                            if (newX < 0
                                || newY < 0
                                || newY >= s.Length
                                || newX >= s[newY].Length
                                || s[newY][newX] != '@')
                                continue;

                            adjacentCount++;
                        }

                        if (adjacentCount >= 4)
                            continue;

                        toChange.Add((j, i));
                        solution++;
                        stillGoing = true;
                    }

                foreach (var item in toChange)
                    s[item.y][item.x] = 'x';
            }

            // 8587
            Console.WriteLine($"{solution} rolls of paper in total can be removed by the Elves and their forklifts");
        }
    }
}
