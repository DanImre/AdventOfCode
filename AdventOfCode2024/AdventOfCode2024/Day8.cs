using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day8 : IDay
    {
        public string InputPath { get; set; } = "day8Input.txt";

        public void PartOne()
        {
            char[][] s = File.ReadAllLines(InputPath).Select(kk => kk.ToCharArray()).ToArray();

            Dictionary<char, List<(int y, int x)>> antennaPositions = new();
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == '.')
                        continue;

                    if (!antennaPositions.ContainsKey(s[i][j]))
                        antennaPositions.Add(s[i][j], new List<(int y, int x)>());

                    antennaPositions[s[i][j]].Add((i, j));
                }

            HashSet<(int y, int x)> locations = new();

            foreach (var item in antennaPositions)
            {
                for (int i = 0; i < item.Value.Count; i++)
                    for (int j = i + 1; j < item.Value.Count; j++)
                    {
                        (int y, int x) diff = (item.Value[i].y - item.Value[j].y, item.Value[i].x - item.Value[j].x);
                        (int y, int x) antinodeOne = (item.Value[i].y + diff.y, item.Value[i].x + diff.x);
                        (int y, int x) antinodeTwo = (item.Value[j].y - diff.y, item.Value[j].x - diff.x);

                        if (!IsOffTheMap(antinodeOne, s))
                            locations.Add(antinodeOne);
                        if (!IsOffTheMap(antinodeTwo, s))
                            locations.Add(antinodeTwo);
                    }
            }

            Console.WriteLine($"There are {locations.Count} unique locations within the bounds of the map that contain an antinode!"); // 259
        }

        public void PartTwo()
        {
            char[][] s = File.ReadAllLines(InputPath).Select(kk => kk.ToCharArray()).ToArray();

            Dictionary<char, List<(int y, int x)>> antennaPositions = new();
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == '.')
                        continue;

                    if (!antennaPositions.ContainsKey(s[i][j]))
                        antennaPositions.Add(s[i][j], new List<(int y, int x)>());

                    antennaPositions[s[i][j]].Add((i, j));
                }

            HashSet<(int y, int x)> locations = new();

            foreach (var item in antennaPositions)
            {
                for (int i = 0; i < item.Value.Count; i++)
                    for (int j = i + 1; j < item.Value.Count; j++)
                    {
                        (int y, int x) diff = (item.Value[i].y - item.Value[j].y, item.Value[i].x - item.Value[j].x);

                        // From antenna j towards i
                        (int y, int x) nextPos = (item.Value[j].y + diff.y, item.Value[j].x + diff.x);
                        while (!IsOffTheMap(nextPos, s))
                        {
                            locations.Add(nextPos);
                            nextPos = (nextPos.y + diff.y, nextPos.x + diff.x);
                        }

                        // From antenna i towards j
                        nextPos = (item.Value[i].y - diff.y, item.Value[i].x - diff.x);
                        while (!IsOffTheMap(nextPos, s))
                        {
                            locations.Add(nextPos);
                            nextPos = (nextPos.y - diff.y, nextPos.x - diff.x);
                        }
                    }
            }

            Console.WriteLine($"There are {locations.Count} unique locations within the bounds of the map that contain an antinode!"); // 927
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
