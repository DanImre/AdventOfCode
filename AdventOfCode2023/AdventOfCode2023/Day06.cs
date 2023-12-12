using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day06
    {


        public Day06()
        {

        }
        public long PartOne()
        {
            //string[] s = File.ReadAllLines("day6test.txt");
            string[] s = File.ReadAllLines("day6.txt");

            long[] times = s[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(kk => long.Parse(kk)).ToArray();
            long[] distances = s[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(kk => long.Parse(kk)).ToArray();

            long solution = 1;

            for (int i = 0; i < times.Length; i++)
            {
                long tempSolution = 0;
                for (int j = 0; j < times[i]; j++)
                {
                    if ((times[i] - j) * j > distances[i])
                        ++tempSolution;
                }

                solution *= tempSolution;
            }

            return solution;
        }
        public long PartTwo()
        {
            //string[] s = File.ReadAllLines("day6test.txt");
            string[] s = File.ReadAllLines("day6.txt");

            long time = long.Parse(string.Join("",s[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)));
            long distance = long.Parse(string.Join("", s[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries)));

            //Searching for the 'start point'
            long start = 0;
            long end = time;

            while (start < end)
            {
                long mid = start + (end - start) / 2;

                long d = mid * (time - mid);
                if (d < distance)
                    start = mid + 1;
                else
                    end = mid;
            }

            return time - 2 * start + 1;
        }
    }
}
