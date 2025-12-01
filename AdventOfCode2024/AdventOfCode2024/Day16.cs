using DataStructures;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.RootFinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day16 : IDay
    {
        public string InputPath { get; set; } = "day16Input.txt";
        //public string InputPath { get; set; } = "day16TestInput.txt";
        //public string InputPath { get; set; } = "day16TestInput2.txt";

        public void PartOne()
        {
            // Idea, literally dijkstra (without path)

            string[] s = File.ReadAllLines(InputPath);

            (int y, int x) start = (0, 0);
            (int y, int x) end = (0, 0);

            List<List<Dictionary<Direction, int>>> costs = new();
            for (int i = 0; i < s.Length; i++)
            {
                List<Dictionary<Direction, int>> temp = new();
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == 'S')
                        start = (i, j);
                    if (s[i][j] == 'E')
                        end = (i, j);

                    Dictionary<Direction, int> dict = new()
                    {
                        { Direction.Up, int.MaxValue },
                        { Direction.Down, int.MaxValue },
                        { Direction.Left, int.MaxValue },
                        { Direction.Right, int.MaxValue }
                    };
                    temp.Add(dict);
                }

                costs.Add(temp);
            }

            Queue<((int y, int x) pos, Direction direction, int score)> q = new();
            q.Enqueue((start, Direction.Right, 0));
            while (q.Count > 0)
            {
                var curr = q.Dequeue();

                if (costs[curr.pos.y][curr.pos.x][curr.direction] <= curr.score)
                    continue;

                costs[curr.pos.y][curr.pos.x][curr.direction] = curr.score;

                if (s[curr.pos.y][curr.pos.x] == 'E')
                    continue;

                // Right
                if (s[curr.pos.y][curr.pos.x + 1] != '#')
                    q.Enqueue(((curr.pos.y, curr.pos.x + 1), Direction.Right, curr.score + (curr.direction == Direction.Right ? 1 : 1001)));
                // Left
                if (s[curr.pos.y][curr.pos.x - 1] != '#')
                    q.Enqueue(((curr.pos.y, curr.pos.x - 1), Direction.Left, curr.score + (curr.direction == Direction.Left ? 1 : 1001)));
                // Up
                if (s[curr.pos.y - 1][curr.pos.x] != '#')
                    q.Enqueue(((curr.pos.y - 1, curr.pos.x), Direction.Up, curr.score + (curr.direction == Direction.Up ? 1 : 1001)));
                // Down
                if (s[curr.pos.y + 1][curr.pos.x] != '#')
                    q.Enqueue(((curr.pos.y + 1, curr.pos.x), Direction.Down, curr.score + (curr.direction == Direction.Down ? 1 : 1001)));
            }

            int solution = costs[end.y][end.x].Min(x => x.Value);

            //foreach (var item in costs)
            //{
            //    foreach (var i in item)
            //    {
            //        if(i.Any(kk => kk.Value != int.MaxValue))
            //            Console.Write('.');
            //        else
            //            Console.Write('#');
            //    }
            //    Console.WriteLine();
            //}

            Console.WriteLine($"The lowest score a Reindeer could possibly get is {solution}"); // 160624
        }

        public void PartTwo()
        {
            // Recursive had stack overflow
            string[] s = File.ReadAllLines(InputPath);

            (int y, int x) start = (0, 0);
            (int y, int x) end = (0, 0);

            List<List<Dictionary<Direction, int>>> costs = new();
            for (int i = 0; i < s.Length; i++)
            {
                List<Dictionary<Direction, int>> temp = new();
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == 'S')
                        start = (i, j);
                    if (s[i][j] == 'E')
                        end = (i, j);

                    Dictionary<Direction, int> dict = new()
                    {
                        { Direction.Up, int.MaxValue },
                        { Direction.Down, int.MaxValue },
                        { Direction.Left, int.MaxValue },
                        { Direction.Right, int.MaxValue }
                    };
                    temp.Add(dict);
                }

                costs.Add(temp);
            }

            Queue<((int y, int x) pos, Direction direction, int score)> q = new();
            q.Enqueue((start, Direction.Right, 0));
            while (q.Count > 0)
            {
                var curr = q.Dequeue();

                if (costs[curr.pos.y][curr.pos.x][curr.direction] <= curr.score)
                    continue;

                costs[curr.pos.y][curr.pos.x][curr.direction] = curr.score;

                if (s[curr.pos.y][curr.pos.x] == 'E')
                    continue;

                // Right
                if (s[curr.pos.y][curr.pos.x + 1] != '#')
                    q.Enqueue(((curr.pos.y, curr.pos.x + 1), Direction.Right, curr.score + (curr.direction == Direction.Right ? 1 : 1001)));
                // Left
                if (s[curr.pos.y][curr.pos.x - 1] != '#')
                    q.Enqueue(((curr.pos.y, curr.pos.x - 1), Direction.Left, curr.score + (curr.direction == Direction.Left ? 1 : 1001)));
                // Up
                if (s[curr.pos.y - 1][curr.pos.x] != '#')
                    q.Enqueue(((curr.pos.y - 1, curr.pos.x), Direction.Up, curr.score + (curr.direction == Direction.Up ? 1 : 1001)));
                // Down
                if (s[curr.pos.y + 1][curr.pos.x] != '#')
                    q.Enqueue(((curr.pos.y + 1, curr.pos.x), Direction.Down, curr.score + (curr.direction == Direction.Down ? 1 : 1001)));
            }

            int minCost = costs[end.y][end.x].Min(kk => kk.Value);

            // The real part 2:

            HashSet<(int y, int x)> tiles = new();

            Queue<((int y, int x) pos, Direction direction)> reverseQ = new();
            for (int i = 0; i < 4; i++)
                if (costs[end.y][end.x][(Direction)i] == minCost)
                    reverseQ.Enqueue((end, (Direction)i));

            while (reverseQ.Count > 0)
            {
                var curr = reverseQ.Dequeue();

                tiles.Add(curr.pos);

                if (curr.pos == start)
                    continue;


                int relativeMin = int.MaxValue;
                for (int i = 0; i < 4; i++)
                {
                    Direction dir = (Direction)i;
                    int relativeValue = costs[curr.pos.y][curr.pos.x][dir] - (dir == curr.direction ? 1000 : 0);
                    relativeMin = Math.Min(relativeMin, relativeValue);
                }

                for (int i = 0; i < 4; i++)
                {
                    Direction dir = (Direction)i;
                    int relativeValue = costs[curr.pos.y][curr.pos.x][dir] - (dir == curr.direction ? 1000 : 0);

                    if (relativeValue != relativeMin)
                        continue;

                    reverseQ.Enqueue((GetNext(curr.pos, dir, true), dir));
                }
            }

            //for (int i = 0; i < s.Length; i++)
            //{
            //    for (int j = 0; j < s[i].Length; j++)
            //    {
            //        if (tiles.Contains((i, j)))
            //        {
            //            Console.ForegroundColor = ConsoleColor.Green;
            //            Console.Write('O');
            //            Console.ForegroundColor = ConsoleColor.Gray;
            //        }
            //        else if (costs[i][j].Min(kk => kk.Value) != int.MaxValue)
            //            Console.Write('.');
            //        else
            //            Console.Write('#');
            //    }
            //    Console.WriteLine();
            //}

            Console.WriteLine($"There are {tiles.Count} tiles that are part of at least one of the best paths through the maze."); // 692
        }

        private (int y, int x) GetNext((int y, int x) pos, Direction dir, bool reverse)
        {
            (int y, int x) next = (0, 0);
            switch (dir)
            {
                case Direction.Up:
                    if (!reverse)
                        next = (pos.y - 1, pos.x);
                    else
                        next = (pos.y + 1, pos.x);
                    break;
                case Direction.Down:
                    if (!reverse)
                        next = (pos.y + 1, pos.x);
                    else
                        next = (pos.y - 1, pos.x);
                    break;
                case Direction.Left:
                    if (!reverse)
                        next = (pos.y, pos.x - 1);
                    else
                        next = (pos.y, pos.x + 1);
                    break;
                case Direction.Right:
                    if (!reverse)
                        next = (pos.y, pos.x + 1);
                    else
                        next = (pos.y, pos.x - 1);
                    break;
            }
            return next;
        }
    }
}
