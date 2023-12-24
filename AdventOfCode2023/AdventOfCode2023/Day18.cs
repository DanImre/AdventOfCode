using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day18
    {
        public Day18()
        {

        }

        public int PartOne() // 35401
        {
            //string[][] s = File.ReadAllLines("day18test.txt").Select(kk => kk.Split(' ')).ToArray();
            string[][] s = File.ReadAllLines("day18.txt").Select(kk => kk.Split(' ')).ToArray();

            int perimeter = 0;

            List<(int x, int y)> coords = new List<(int x, int y)>();
            coords.Add((0, 0));

            int x = 0;
            int y = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int steps = int.Parse(s[i][1]);
                switch (s[i][0])
                {
                    case "U":
                        y += steps;
                        break;
                    case "D":
                        y -= steps;
                        break;
                    case "L":
                        x -= steps;
                        break;
                    default: //'R'
                        x += steps;
                        break;
                }

                perimeter += steps;
                coords.Add((x, y));
            }

            double area = 0;

            //Shoelace formula - Area of a polygon
            for (int i = 0; i < coords.Count - 1; i++)
                area += coords[i].x * coords[i + 1].y - coords[i + 1].x * coords[i].y;

            area = Math.Abs(area + coords[coords.Count - 1].x * coords[0].y - coords[0].x * coords[coords.Count - 1].y);
            area /= 2;

            //Pick's theorem - number of whole integer points in a polygon
            double pointCount = area - perimeter / 2.0 + 1;

            return (int)(perimeter + pointCount);
        }

        public long PartTwo()
        {
            //string[][] s = File.ReadAllLines("day18test.txt").Select(kk => kk.Split(' ')).ToArray();
            string[][] s = File.ReadAllLines("day18.txt").Select(kk => kk.Split(' ')).ToArray();

            long perimeter = 0;

            List<(long x, long y)> coords = new List<(long x, long y)>();
            coords.Add((0, 0));

            long x = 0;
            long y = 0;
            for (int i = 0; i < s.Length; i++)
            {
                long steps = long.Parse(s[i][2].Substring(2,5),System.Globalization.NumberStyles.HexNumber);
                switch (s[i][2][7])
                {
                    case '3':
                        y += steps;
                        break;
                    case '1':
                        y -= steps;
                        break;
                    case '2':
                        x -= steps;
                        break;
                    default: //'0'
                        x += steps;
                        break;
                }

                perimeter += steps;
                coords.Add((x, y));
            }

            long area = 0;

            //Shoelace formula - Area of a polygon
            for (int i = 0; i < coords.Count - 1; i++)
                area += coords[i].x * coords[i + 1].y - coords[i + 1].x * coords[i].y;

            area = Math.Abs(area + coords[coords.Count - 1].x * coords[0].y - coords[0].x * coords[coords.Count - 1].y);
            area /= 2;

            //Pick's theorem - number of whole integer points in a polygon
            long pointCount = area - perimeter / 2 + 1;

            return perimeter + pointCount;
        }
    }
}
