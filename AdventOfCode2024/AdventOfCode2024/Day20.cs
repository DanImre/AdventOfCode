using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2024
{
    public class Day20 : IDay
    {
        public string InputPath { get; set; } = "day20Input.txt";
        //public string InputPath { get; set; } = "day20TestInput.txt";

        public void PartOne()
        {
            // Idea, get best path, check for 1 gaps that save atleast 100 picoseconds like 415#4
            string[] s = File.ReadAllLines(InputPath);
            (int y, int x) start = (0, 0);
            (int y, int x) end = (0, 0);

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == 'S')
                        start = (i, j);
                    else if (s[i][j] == 'E')
                        end = (i, j);
                }

            (int y, int x)[,] dir = new (int y, int x)[s.Length, s[0].Length];
            HashSet<(int y, int x)> visited = new();

            Queue<(int y, int x)> q = new();
            q.Enqueue(start);

            while (q.Count > 0)
            {
                var curr = q.Dequeue();

                if (curr == end)
                    break;

                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                        if ((i == 0 || j == 0) && i != j)
                        {
                            (int y, int x) next = (curr.y + i, curr.x + j);
                            if (next.y >= 0
                                && next.y < s.Length
                                && next.x >= 0
                                && next.x < s[next.y].Length
                                && s[next.y][next.x] != '#'
                                && visited.Add(next))
                            {
                                q.Enqueue(next);
                                dir[next.y, next.x] = curr;
                            }
                        }
            }

            // has the path and the tiles' distence from the end
            Dictionary<(int y, int x), int> path = new();

            int distFromEnd = 0;
            (int y, int x) backtrack = end;
            while (backtrack != start)
            {
                path.Add(backtrack, distFromEnd++);
                backtrack = dir[backtrack.y, backtrack.x];
            }
            path.Add(start, distFromEnd);


            int picosecondsToSave = 100;
            long solution = 0;

            foreach (var item in path)
            {
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                        if ((i == 0 || j == 0) && i != j)
                        {
                            (int y, int x) checkPos = (item.Key.y + 2 * i, item.Key.x + 2 * j);
                            if (checkPos.y >= 0
                                && checkPos.y < s.Length
                                && checkPos.x >= 0
                                && checkPos.x < s[checkPos.y].Length
                                && s[item.Key.y + i][item.Key.x + j] == '#'
                                && path.ContainsKey(checkPos)
                                && item.Value - path[checkPos] - 2 >= picosecondsToSave)
                                solution++;
                        }
            }

            Console.WriteLine($"There are {solution} cheats that would save atleast {picosecondsToSave} picoseconds"); // 1360
        }

        public void PartTwo()
        {
            // Idea, same: get best path, but now check paths (with max lengths of 10) that reach the other parts of the path that would save atleast 100 picoseconds
            // Problem: Might not need to go back to the best path, could come out at a dead end and still be closer then without cheating
            // Solution: Map every nodes distance from the end

            string[] s = File.ReadAllLines(InputPath);
            (int y, int x) start = (0, 0);
            (int y, int x) end = (0, 0);

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == 'S')
                        start = (i, j);
                    else if (s[i][j] == 'E')
                        end = (i, j);
                }

            (int y, int x)[,] dir = new (int y, int x)[s.Length, s[0].Length];
            HashSet<(int y, int x)> visited = new();

            Queue<(int y, int x)> q = new();
            q.Enqueue(start);

            while (q.Count > 0)
            {
                var curr = q.Dequeue();

                if (curr == end)
                    break;

                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                        if ((i == 0 || j == 0) && i != j)
                        {
                            (int y, int x) next = (curr.y + i, curr.x + j);
                            if (next.y >= 0
                                && next.y < s.Length
                                && next.x >= 0
                                && next.x < s[next.y].Length
                                && s[next.y][next.x] != '#'
                                && visited.Add(next))
                            {
                                q.Enqueue(next);
                                dir[next.y, next.x] = curr;
                            }
                        }
            }

            // has the path and the tiles' distence from the end
            Dictionary<(int y, int x), int> path = new();

            int distFromEnd = 0;
            (int y, int x) backtrack = end;
            while (backtrack != start)
            {
                path.Add(backtrack, distFromEnd++);
                backtrack = dir[backtrack.y, backtrack.x];
            }
            path.Add(start, distFromEnd);


            int picosecondsToSave = 100;
            int maxPicosecondsPerCheat = 20;
            long solution = 0;

            foreach (var item in path)
            {
                visited = new();

                Queue<((int y, int x) pos, int dist)> innerQ = new();
                innerQ.Enqueue((item.Key, 0));

                while (innerQ.Count > 0)
                {
                    var curr = innerQ.Dequeue();

                    if (curr.dist > maxPicosecondsPerCheat || !visited.Add(curr.pos))
                        continue;

                    if (curr.dist > 0 && s[curr.pos.y][curr.pos.x] != '#')
                    {
                        if (path.ContainsKey(curr.pos)
                        && item.Value - path[curr.pos] - curr.dist >= picosecondsToSave)
                            solution++;
                        continue;
                    }

                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if ((i == 0 || j == 0) && i != j)
                            {
                                (int y, int x) next = (curr.pos.y + i, curr.pos.x + j);
                                if (next.y >= 0
                                    && next.y < s.Length
                                    && next.x >= 0
                                    && next.x < s[next.y].Length)
                                    innerQ.Enqueue((next, curr.dist + 1));
                            }
                }
            }

            Console.WriteLine($"There are {solution} cheats that would save atleast {picosecondsToSave} picoseconds"); // 202749 too low
        }
    }
}
