using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day03 : IDay
    {
        private class Day03Position
        {
            public int x { get; set; }
            public int y { get; set; }

            public Day03Position(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        public string InputPath { get; set; } = "Day3Input.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            HashSet<(int x, int y)> hs = new HashSet<(int x, int y)>();
            (int x, int y) currentPosition = (0, 0);
            foreach (var item in s.SelectMany(x => x))
                switch (item)
                {
                    case '^':
                        currentPosition.y++;
                        hs.Add(currentPosition);
                        break;
                    case 'v':
                        currentPosition.y--;
                        hs.Add(currentPosition);
                        break;
                    case '>':
                        currentPosition.x++;
                        hs.Add(currentPosition);
                        break;
                    case '<':
                        currentPosition.x--;
                        hs.Add(currentPosition);
                        break;
                    default:
                        break;
                }

            Console.WriteLine($"A total of {hs.Count} houses receive a gift.");
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            HashSet<(int x, int y)> hs = new HashSet<(int x, int y)>();

            Day03Position santasCurrentPosition = new Day03Position(0, 0);
            Day03Position roboSantasCurrentPosition = new Day03Position(0, 0);
            bool santasTurn = true;

            foreach (var item in s.SelectMany(x => x))
            {
                Day03Position curr = santasTurn ? santasCurrentPosition : roboSantasCurrentPosition;
                switch (item)
                {
                    case '^':
                        curr.y++;
                        hs.Add((curr.x, curr.y));
                        break;
                    case 'v':
                        curr.y--;
                        hs.Add((curr.x, curr.y));
                        break;
                    case '>':
                        curr.x++;
                        hs.Add((curr.x, curr.y));
                        break;
                    case '<':
                        curr.x--;
                        hs.Add((curr.x, curr.y));
                        break;
                    default:
                        break;
                }

                santasTurn = !santasTurn;
            }

            Console.WriteLine($"A total of {hs.Count} houses receive a gift.");
        }
    }
}
