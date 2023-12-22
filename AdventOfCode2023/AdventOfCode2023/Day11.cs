using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day11
    {
        public Day11()
        {

        }

        public long PartOne() //9724940
        {
            //string[] s = File.ReadAllLines("day11test.txt");
            string[] s = File.ReadAllLines("day11.txt");

            List<(int y, int x)> pairs = new List<(int y, int x)>();
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == '#')
                        pairs.Add((i, j));

            //Columns
            int width = pairs.Max(kk => kk.x);
            for (int i = pairs.Min(kk => kk.x) + 1; i < width - 1; i++)
            {
                if (pairs.Any(kk => kk.x == i))
                    continue;

                for (int j = 0; j < pairs.Count; j++)
                    if (pairs[j].x > i)
                        pairs[j] = (pairs[j].y, pairs[j].x + 1);

                ++i;
                ++width;
            }
            //Rows
            int height = pairs.Max(kk => kk.y);
            for (int i = pairs.Min(kk => kk.y) + 1; i < height - 1; i++)
            {
                if (pairs.Any(kk => kk.y == i))
                    continue;

                for (int j = 0; j < pairs.Count; j++)
                    if (pairs[j].y > i)
                        pairs[j] = (pairs[j].y + 1, pairs[j].x);

                ++i;
                ++height;
            }

            long solution = 0;

            for (int i = 0; i < pairs.Count; i++)
                for (int j = i + 1; j < pairs.Count; j++)
                    solution += Math.Abs(pairs[i].y - pairs[j].y) + Math.Abs(pairs[i].x - pairs[j].x);

            return solution;
        }
        public long PartTwo() //wrong: 569053155896
        {
            //string[] s = File.ReadAllLines("day11test.txt"); //82000210
            string[] s = File.ReadAllLines("day11.txt");

            long dist = 1000000;

            List<(long y, long x)> pairs = new List<(long y, long x)>();
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == '#')
                        pairs.Add((i, j));

            //Columns
            long width = s.Length;
            for (long i = 1; i < width - 1; i++)
            {
                if (pairs.Any(kk => kk.x == i))
                    continue;

                for (int j = 0; j < pairs.Count; j++)
                    if (pairs[j].x > i)
                        pairs[j] = (pairs[j].y, pairs[j].x + dist - 1);

                i += dist - 1;
                width += dist - 1;
            }
            //Rows
            long height = s[0].Length;
            for (long i = 1; i < height - 1; i++)
            {
                if (pairs.Any(kk => kk.y == i))
                    continue;

                for (int j = 0; j < pairs.Count; j++)
                    if (pairs[j].y > i)
                        pairs[j] = (pairs[j].y + dist - 1, pairs[j].x);

                i += dist - 1;
                height += dist - 1;
            }

            long solution = 0;

            for (int i = 0; i < pairs.Count; i++)
                for (int j = i + 1; j < pairs.Count; j++)
                    solution += Math.Abs(pairs[i].y - pairs[j].y) + Math.Abs(pairs[i].x - pairs[j].x);

            return solution;
        }
    }
}
