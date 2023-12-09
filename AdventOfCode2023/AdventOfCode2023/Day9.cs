using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day9
    {
        public Day9()
        {

        }

        public long PartOne() //1972648895
        {
            //int[][] s = File.ReadAllLines("day9test.txt").Select(kk => kk.Split(' ').Select(zz => int.Parse(zz)).ToArray()).ToArray();
            int[][] s = File.ReadAllLines("day9.txt").Select(kk => kk.Split(' ').Select(zz => int.Parse(zz)).ToArray()).ToArray();

            long solution = 0;

            foreach (var line in s)
            {
                int iteration = 0;

                while (line.Take(line.Length - iteration).Any(kk => kk != 0))
                {
                    for (int i = 0; i < line.Length - 1 - iteration; i++)
                        line[i] = line[i + 1] - line[i];

                    ++iteration;
                }

                while (iteration >= 0)
                    solution += line[line.Length - 1 - iteration--];
            }

            return solution;
        }
        public long PartTwo() //919
        {
            //int[][] s = File.ReadAllLines("day9test.txt").Select(kk => kk.Split(' ').Select(zz => int.Parse(zz)).ToArray()).ToArray();
            int[][] s = File.ReadAllLines("day9.txt").Select(kk => kk.Split(' ').Select(zz => int.Parse(zz)).ToArray()).ToArray();

            long solution = 0;

            foreach (var line in s)
            {
                int iteration = 0;

                while (line.TakeLast(line.Length - iteration).Any(kk => kk != 0))
                {
                    for (int i = line.Length - 1; i > iteration; i--)
                        line[i] -= line[i - 1];

                    ++iteration;
                }

                long tempSolution = 0;

                while (iteration > 0)
                    tempSolution = line[--iteration] - tempSolution;

                solution += tempSolution;
            }

            return solution;
        }
    }
}
