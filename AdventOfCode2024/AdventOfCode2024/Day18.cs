using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day18 : IDay
    {
        public string InputPath { get; set; } = "day18Input.txt";
        //public string InputPath { get; set; } = "day18TestInput.txt";

        public void PartOne()
        {
            int wideness = 70;
            int[,] map = new int[wideness + 1, wideness + 1];
            using (StreamReader f = new StreamReader(InputPath))
            {
                for (int i = 0; i < 1024; i++)
                {
                    int[] coords = f.ReadLine()!.Split(',').Select(int.Parse).ToArray();
                    map[coords[1], coords[0]] = 1;
                }
            }

            HashSet<(int y, int x)> visited = new();
            Queue<((int y, int x) pos, int dist)> q = new();

            q.Enqueue(((0, 0), 0));

            while (q.Count > 0)
            {
                var curr = q.Dequeue();

                if (!visited.Add(curr.pos))
                    continue;

                if (curr.pos == (wideness, wideness))
                {
                    Console.WriteLine($"After 1024 bytes, the minimum number of steps needed to reach the exit is {curr.dist}"); // 308
                    break;
                }

                if (curr.pos.y > 0 && map[curr.pos.y - 1, curr.pos.x] == 0)
                    q.Enqueue(((curr.pos.y - 1, curr.pos.x), curr.dist + 1));

                if (curr.pos.y < wideness && map[curr.pos.y + 1, curr.pos.x] == 0)
                    q.Enqueue(((curr.pos.y + 1, curr.pos.x), curr.dist + 1));

                if (curr.pos.x > 0 && map[curr.pos.y, curr.pos.x - 1] == 0)
                    q.Enqueue(((curr.pos.y, curr.pos.x - 1), curr.dist + 1));

                if (curr.pos.x < wideness && map[curr.pos.y, curr.pos.x + 1] == 0)
                    q.Enqueue(((curr.pos.y, curr.pos.x + 1), curr.dist + 1));
            }
        }

        public void PartTwo()
        {
            int wideness = 70;
            int[,] map = new int[wideness + 1, wideness + 1];
            Queue<(int y, int x)> bytes = new();
            using (StreamReader f = new StreamReader(InputPath))
            {
                for (int i = 0; i < 1024; i++)
                {
                    int[] coords = f.ReadLine()!.Split(',').Select(int.Parse).ToArray();
                    map[coords[1], coords[0]] = 1;
                }
                while (!f.EndOfStream)
                {
                    int[] coords = f.ReadLine()!.Split(',').Select(int.Parse).ToArray();
                    bytes.Enqueue((coords[1], coords[0]));
                }
            }

            HashSet<(int y, int x)>? GetBestPath()
            {
                (int y, int x)[,] dir = new (int y, int x)[wideness + 1, wideness + 1];

                HashSet<(int y, int x)> visited = new HashSet<(int y, int x)>();

                Queue<(int y, int x)> q = new();
                q.Enqueue((0, 0));

                while (q.Count > 0)
                {
                    var curr = q.Dequeue();

                    if (curr == (wideness, wideness))
                        break;

                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if((i == 0 || j == 0) && i != j)
                            {
                                (int y, int x) nextPos = (curr.y + i, curr.x + j);

                                if (nextPos.y >= 0
                                    && nextPos.x >= 0
                                    && nextPos.y <= wideness
                                    && nextPos.x <= wideness
                                    && map[nextPos.y, nextPos.x] == 0
                                    && visited.Add(nextPos))
                                {
                                    dir[nextPos.y, nextPos.x] = curr;
                                    q.Enqueue(nextPos);
                                }
                            }
                }

                if (!visited.Contains((wideness, wideness)))
                    return null;

                HashSet<(int y, int x)> path = new();

                (int y, int x) backtrackPos = (wideness, wideness);
                while (backtrackPos != (0,0))
                {
                    path.Add(backtrackPos);
                    backtrackPos = dir[backtrackPos.y, backtrackPos.x];
                }
                path.Add(backtrackPos);

                return path;
            }

            //1024 bytes have fallen
            HashSet<(int y, int x)>? bestPath = GetBestPath();

            while (bytes.Count > 0)
            {
                var curr = bytes.Dequeue();

                map[curr.y, curr.x] = 1;

                if (!bestPath!.Contains(curr))
                    continue;

                bestPath = GetBestPath();
                if(bestPath == null)
                {
                    Console.WriteLine($"The blocking bytes cordinates are: {curr.x},{curr.y}"); // 46,28
                    break;
                }
            }
        }
    }
}
