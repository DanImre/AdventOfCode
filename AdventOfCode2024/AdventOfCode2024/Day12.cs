using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day12 : IDay
    {
        public string InputPath { get; set; } = "day12Input.txt";
        //public string InputPath { get; set; } = "day12TestInput.txt";
        //public string InputPath { get; set; } = "day12TestInput2.txt";
        //public string InputPath { get; set; } = "day12TestInput3.txt";

        private enum Wall
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            int solution = 0;


            HashSet<(int y, int x)> visited = new();

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (visited.Contains((i, j)))
                        continue;

                    HashSet<(int y, int x)> localVisited = new();
                    int sideCount = 0;

                    Queue<(int y, int x)> q = new();
                    q.Enqueue((i, j));

                    while (q.Count > 0)
                    {
                        var curr = q.Dequeue();

                        if (!localVisited.Add(curr))
                            continue;

                        if (curr.y > 0 && s[curr.y - 1][curr.x] == s[curr.y][curr.x])
                            q.Enqueue((curr.y - 1, curr.x));
                        else
                            ++sideCount;

                        if (curr.y < s.Length - 1 && s[curr.y + 1][curr.x] == s[curr.y][curr.x])
                            q.Enqueue((curr.y + 1, curr.x));
                        else
                            ++sideCount;

                        if (curr.x > 0 && s[curr.y][curr.x - 1] == s[curr.y][curr.x])
                            q.Enqueue((curr.y, curr.x - 1));
                        else
                            ++sideCount;

                        if (curr.x < s[curr.y].Length - 1 && s[curr.y][curr.x + 1] == s[curr.y][curr.x])
                            q.Enqueue((curr.y, curr.x + 1));
                        else
                            ++sideCount;
                    }

                    solution += sideCount * localVisited.Count;

                    foreach (var item in localVisited)
                        visited.Add(item);
                }

            Console.WriteLine($"The total price of fencing all regions is {solution}"); //1461806
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            int solution = 0;

            HashSet<(int y, int x)> visited = new();

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (visited.Contains((i, j)))
                        continue;

                    HashSet<(int y, int x, Wall wall)> countedWalls = new();

                    HashSet<(int y, int x)> localVisited = new();
                    Queue<(int y, int x)> q = new();
                    q.Enqueue((i, j));

                    while (q.Count > 0)
                    {
                        var curr = q.Dequeue();

                        if (!localVisited.Add(curr))
                            continue;

                        if (curr.y > 0 && s[curr.y - 1][curr.x] == s[curr.y][curr.x])
                            q.Enqueue((curr.y - 1, curr.x));
                        else
                            countedWalls.Add((curr.y, curr.x, Wall.UP));

                        if (curr.y < s.Length - 1 && s[curr.y + 1][curr.x] == s[curr.y][curr.x])
                            q.Enqueue((curr.y + 1, curr.x));
                        else
                            countedWalls.Add((curr.y, curr.x, Wall.DOWN));

                        if (curr.x > 0 && s[curr.y][curr.x - 1] == s[curr.y][curr.x])
                            q.Enqueue((curr.y, curr.x - 1));
                        else
                            countedWalls.Add((curr.y, curr.x, Wall.LEFT));

                        if (curr.x < s[curr.y].Length - 1 && s[curr.y][curr.x + 1] == s[curr.y][curr.x])
                            q.Enqueue((curr.y, curr.x + 1));
                        else
                            countedWalls.Add((curr.y, curr.x, Wall.RIGHT));
                    }

                    foreach (var item in localVisited)
                        visited.Add(item);

                    void removeHorizontalOrVertical(int currY, int currX, bool horizontal, Wall wall)
                    {
                        int xStep = horizontal ? 1 : 0;
                        int yStep = horizontal ? 0 : 1;

                        // forward
                        int x = currX + xStep;
                        int y = currY + yStep;
                        while (x < s[currY].Length
                            && y < s.Length
                            && s[y][x] == s[currY][currX]
                            && countedWalls.Remove((y, x, wall)))
                        {
                            x += xStep;
                            y += yStep;
                        }

                        // backwards
                        x = currX - xStep;
                        y = currY - yStep;
                        while (x >= 0
                            && y >= 0
                            && s[y][x] == s[currY][currX]
                            && countedWalls.Remove((y, x, wall)))
                        {
                            x -= xStep;
                            y -= yStep;
                        }
                    }


                    // Fuse walls -> remove connecting ones
                    foreach (var curr in countedWalls.ToList())
                    {
                        if (!countedWalls.Contains(curr))
                            continue;

                        if (curr.wall == Wall.UP || curr.wall == Wall.DOWN)
                            removeHorizontalOrVertical(curr.y, curr.x, true, curr.wall);
                        else
                            removeHorizontalOrVertical(curr.y, curr.x, false, curr.wall);
                    }

                    solution += countedWalls.Count * localVisited.Count;
                }

            Console.WriteLine($"The new total price of fencing all regions {solution}"); //887932
		}
    }
}
