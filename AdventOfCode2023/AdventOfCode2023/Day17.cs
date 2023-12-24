using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day17
    {
        public Day17()
        {

        }

        public int PartOne() // 1001
        {
            //int[][] s = File.ReadAllLines("day17test.txt").Select(kk => kk.Select(kk => kk - '0').ToArray()).ToArray();
            int[][] s = File.ReadAllLines("day17.txt").Select(kk => kk.Select(kk => kk - '0').ToArray()).ToArray();

            HashSet<(int y, int x, int dirY, int dirX, int moved)> hs = new HashSet<(int y, int x, int dirY, int dirX, int moved)>();

            PriorityQueue<(int y, int x, int dist, int dirY, int dirX, int moved), int> q = new PriorityQueue<(int y, int x, int dist, int dirY, int dirX, int moved), int>();
            q.Enqueue((0, 0, 0, 1, 0, 0), 0);

            List<(int y, int x)> moves = new List<(int y, int x)>() { (1, 0), (0, 1), (-1, 0), (0, -1) };

            while (q.Count != 0)
            {
                var curr = q.Dequeue();

                if (!hs.Add((curr.y, curr.x, curr.dirY, curr.dirX, curr.moved)))
                    continue;

                if (curr.y == s.Length - 1 && curr.x == s[0].Length - 1)
                    return curr.dist;

                if(curr.moved < 3) //Can move along
                {
                    int nextY = curr.y + curr.dirY;
                    int nextX = curr.x + curr.dirX;

                    if (nextX >= 0 && nextX < s[0].Length && nextY >= 0 && nextY < s.Length)
                    {
                        int nextDist = curr.dist + s[nextY][nextX];
                        q.Enqueue((nextY, nextX, nextDist, curr.dirY, curr.dirX, curr.moved + 1), nextDist);
                    }
                }

                foreach (var item in moves)
                {
                    if ((item.y == curr.dirY && item.x == curr.dirX)
                        || (item.y == -curr.dirY && item.x == -curr.dirX))
                        continue;

                    int nextY = curr.y + item.y;
                    int nextX = curr.x + item.x;

                    if (nextX < 0 || nextX >= s[0].Length || nextY < 0 || nextY >= s.Length)
                        continue;

                    int nextDist = curr.dist + s[nextY][nextX];
                    q.Enqueue((nextY, nextX, nextDist, item.y, item.x, 1), nextDist);
                }
            }

            return -1;
        }

        public int PartTwo() // 1197
        {
            //int[][] s = File.ReadAllLines("day17test.txt").Select(kk => kk.Select(kk => kk - '0').ToArray()).ToArray();
            int[][] s = File.ReadAllLines("day17.txt").Select(kk => kk.Select(kk => kk - '0').ToArray()).ToArray();

            HashSet<(int y, int x, int dirY, int dirX, int moved)> hs = new HashSet<(int y, int x, int dirY, int dirX, int moved)>();

            PriorityQueue<(int y, int x, int dist, int dirY, int dirX, int moved), int> q = new PriorityQueue<(int y, int x, int dist, int dirY, int dirX, int moved), int>();
            q.Enqueue((0, 0, 0, 1, 0, 0), 0);
            q.Enqueue((0, 0, 0, 0, 1, 0), 0);

            List<(int y, int x)> moves = new List<(int y, int x)>() { (1, 0), (0, 1), (-1, 0), (0, -1) };

            while (q.Count != 0)
            {
                var curr = q.Dequeue();

                if (!hs.Add((curr.y, curr.x, curr.dirY, curr.dirX, curr.moved)))
                    continue;

                if (curr.y == s.Length - 1 && curr.x == s[0].Length - 1 && curr.moved >= 4)
                    return curr.dist;

                if (curr.moved < 10) //Can move along
                {
                    int nextY = curr.y + curr.dirY;
                    int nextX = curr.x + curr.dirX;

                    if (nextX >= 0 && nextX < s[0].Length && nextY >= 0 && nextY < s.Length)
                    {
                        int nextDist = curr.dist + s[nextY][nextX];
                        q.Enqueue((nextY, nextX, nextDist, curr.dirY, curr.dirX, curr.moved + 1), nextDist);
                    }
                }

                if (curr.moved >= 4)
                    foreach (var item in moves)
                    {
                        if ((item.y == curr.dirY && item.x == curr.dirX)
                            || (item.y == -curr.dirY && item.x == -curr.dirX))
                            continue;

                        int nextY = curr.y + item.y;
                        int nextX = curr.x + item.x;

                        if (nextX < 0 || nextX >= s[0].Length || nextY < 0 || nextY >= s.Length)
                            continue;

                        int nextDist = curr.dist + s[nextY][nextX];
                        q.Enqueue((nextY, nextX, nextDist, item.y, item.x, 1), nextDist);
                    }
            }

            return -1;
        }
    }
}
