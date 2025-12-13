
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day6 : IDay
    {
        public string InputPath { get; set; } = "day6Input.txt";

        public void PartOne()
        {
            char[][] s = File.ReadAllLines(InputPath).Select(kk => kk.ToCharArray()).ToArray();

            (int y, int x) guardPos = (-1, -1);
            int guardDirection = 0;

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == '^')
                    {
                        guardPos = (i, j);
                        break;
                    }

            // UP, RIGHT, DOWN, LEFT (90 degree to the right at each step)
            List<(int y, int x)> directions = new List<(int y, int x)>() { (-1, 0), (0, 1), (1, 0), (0, -1) };

            HashSet<(int, int)> seenPositions = new();

            while (true)
            {
                seenPositions.Add(guardPos);
                (int y, int x) nextPos = (guardPos.y + directions[guardDirection].y, guardPos.x + directions[guardDirection].x);

                // Running off the map
                if (IsOffTheMap(nextPos, s))
                    break;

                // Turning
                if (s[nextPos.y][nextPos.x] == '#')
                    guardDirection = (guardDirection + 1) % 4;
                else //Moving
                    guardPos = nextPos;
            }

            Console.WriteLine($"The guard will visit {seenPositions.Count} positions before leaving the area!");
        }

        public void PartTwo()
        {
            char[][] s = File.ReadAllLines(InputPath).Select(kk => kk.ToCharArray()).ToArray();

            (int y, int x) guardPos = (-1, -1);
            int guardDirection = 0;

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == '^')
                    {
                        guardPos = (i, j);
                        break;
                    }

            // UP, RIGHT, DOWN, LEFT (90 degree to the right at each step)
            List<(int y, int x)> directions = new List<(int y, int x)>() { (-1, 0), (0, 1), (1, 0), (0, -1) };

            // Basicly part1 with loop detection
            bool GoesInALoop((int y, int x) pos, int dir)
            {
                HashSet<(int y, int x, int dir)> seenConfiguration = new();
                bool IsLooping = false;

                while (true)
                {
                    if (!seenConfiguration.Add((pos.y, pos.x, dir)))
                    {
                        IsLooping = true;
                        break;
                    }
                    (int y, int x) nextPos = (pos.y + directions[dir].y, pos.x + directions[dir].x);

                    // Running off the map
                    if (IsOffTheMap(nextPos, s))
                        break;

                    // Turning
                    if (s[nextPos.y][nextPos.x] == '#')
                        dir = (dir + 1) % 4;
                    else //Moving
                        pos = nextPos;
                }

                return IsLooping;
            }

            HashSet<(int y, int x)> triedObsctructionSpots = new();

            int solution = 0;

            while (true)
            {
                (int y, int x) nextPos = (guardPos.y + directions[guardDirection].y, guardPos.x + directions[guardDirection].x);

                // Running off the map
                if (IsOffTheMap(nextPos, s))
                    break;

                // Turning
                if (s[nextPos.y][nextPos.x] == '#')
                {
                    guardDirection = (guardDirection + 1) % 4;
                    continue;
                }
                
                // Placing obstruction or moving
                if (triedObsctructionSpots.Add(nextPos))
                {
                    // Place then remove obstruction
                    s[nextPos.y][nextPos.x] = '#';
                    if (GoesInALoop(guardPos, guardDirection))
                        solution++;
                    s[nextPos.y][nextPos.x] = ' ';
                }
                guardPos = nextPos;
            }

            Console.WriteLine($"I could choose {solution} different positions for this obstruction!"); //1984
        }

        private bool IsOffTheMap((int y, int x) pos, char[][] map)
        {
            return pos.x < 0
                || pos.y < 0
                || pos.y >= map.Length
                || pos.x >= map[pos.y].Length;
        }
    }
}
