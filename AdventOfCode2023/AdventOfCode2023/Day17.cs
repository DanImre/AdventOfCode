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

        public int PartOne() // 1017 too high
        {
            int[][] s = File.ReadAllLines("day17test.txt").Select(kk => kk.Select(kk => kk - '0').ToArray()).ToArray();
            //int[][] s = File.ReadAllLines("day17.txt").Select(kk => kk.Select(kk => kk - '0').ToArray()).ToArray();

            HashSet<(int y, int x, int dirY, int dirX, int movesLeftBeforeTurn)> hs = new HashSet<(int y, int x, int dirY, int dirX, int movesLeftBeforeTurn)>();

            PriorityQueue<(int y, int x, int dist, int dirY, int dirX, int movesLeftBeforeTurn), int> q = new PriorityQueue<(int y, int x, int dist, int dirY, int dirX, int movesLeftBeforeTurn), int>();
            q.Enqueue((0, 1, s[0][1], 0, 1, 2), s[0][1]);
            q.Enqueue((1, 0, s[1][0], 1, 0, 2), s[1][0]);

            List<(int y, int x)> moves = new List<(int y, int x)>() { (1, 0), (0, 1), (-1, 0), (0, -1) };


            int[][] dist = new int[s.Length][];
            for (int i = 0; i < s.Length; i++)
            {
                dist[i] = new int[s[0].Length];
                Array.Fill(dist[i], int.MaxValue);
            }


            while (q.Count != 0)
            {
                var curr = q.Dequeue();

                if (!hs.Add((curr.y, curr.x, curr.dirY, curr.dirX, curr.movesLeftBeforeTurn)))
                    continue;


                dist[curr.y][curr.x] = Math.Min(dist[curr.y][curr.x], curr.dist);

                if (curr.y == s.Length - 1 && curr.x == s[0].Length - 1)
                {
                    //Console.WriteLine(string.Join("\n", dist.Select(kk => string.Join("\t", kk.Select(zz => zz == int.MaxValue ? "NA" : zz.ToString())))));
                    //Console.WriteLine();

                    Console.WriteLine(curr.dist);

                    //return curr.dist;
                }

                if(curr.movesLeftBeforeTurn >= 0) //Can move along
                {
                    int nextY = curr.y + curr.dirY;
                    int nextX = curr.x + curr.dirX;

                    if (nextX < 0 || nextX >= s[0].Length || nextY < 0 || nextY >= s.Length)
                        continue;

                    int nextDist = curr.dist + s[nextY][nextX];
                    q.Enqueue((nextY, nextX, nextDist, curr.dirY, curr.dirX, curr.movesLeftBeforeTurn - 1), nextDist);
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
                    q.Enqueue((nextY, nextX, nextDist, item.y, item.x, 2), nextDist);
                }
            }

            return -1;
        }

        public int PartTwo()
        {
            return 0;
        }
    }
}
