using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2024
{
    public class Day14 : IDay
    {
        public string InputPath { get; set; } = "day14Input.txt";
        //public string InputPath { get; set; } = "day14TestInput.txt";

        public void PartOne()
        {
            // Idea, isn't this just modulo + the counting the regions?

            List<RestroomRobot> robots = new();
            using (StreamReader f = new StreamReader(InputPath))
            {
                while (!f.EndOfStream)
                {
                    int[] line = f.ReadLine()!.Split(new char[] { '=', ',', ' ', 'p', 'v' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                    robots.Add(new RestroomRobot((line[0], line[1]), (line[2], line[3])));
                }
            }

            string[] s = File.ReadAllLines(InputPath);

            int seconds = 100;
            int moduloX = 101;
            int moduloY = 103;

            foreach (var item in robots)
            {
                int nextX = (item.Pos.x + item.Velocity.x * seconds) % moduloX;
                if (nextX < 0)
                    nextX += moduloX;
                int nextY = (item.Pos.y + item.Velocity.y * seconds) % moduloY;
                if (nextY < 0)
                    nextY += moduloY;
                item.Pos = (nextX, nextY);
            }

            int horizontalDivide = moduloX / 2;
            int verticalDivide = moduloY / 2;

            int topLeft = robots.Count(kk => kk.Pos.x < horizontalDivide && kk.Pos.y < verticalDivide);
            int topRight = robots.Count(kk => kk.Pos.x > horizontalDivide && kk.Pos.y < verticalDivide);
            int bottomLeft = robots.Count(kk => kk.Pos.x < horizontalDivide && kk.Pos.y > verticalDivide);
            int bottomRight = robots.Count(kk => kk.Pos.x > horizontalDivide && kk.Pos.y > verticalDivide);

            int solution = topLeft * topRight * bottomLeft * bottomRight;

            Console.WriteLine($"The safety factor will be {solution} after exactly 100 seconds have elapsed"); //214109808
        }

        public void PartTwo()
        {
            List<RestroomRobot> robots = new();
            using (StreamReader f = new StreamReader(InputPath))
            {
                while (!f.EndOfStream)
                {
                    int[] line = f.ReadLine()!.Split(new char[] { '=', ',', ' ', 'p', 'v' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                    robots.Add(new RestroomRobot((line[0], line[1]), (line[2], line[3])));
                }
            }

            string[] s = File.ReadAllLines(InputPath);

            int moduloX = 101;
            int moduloY = 103;

            double alltimeHighestConnectionRatio = 0;

            for (int iter = 0; iter < 100000; iter++)
            {
                if (iter >= 7600)
                {
                    List<string> renderedMap = new();
                    for (int i = 0; i < moduloY; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < moduloX; j++)
                        {
                            if (robots.Any(kk => kk.Pos == (j, i)))
                                sb.Append('#');
                            else
                                sb.Append(' ');
                        }
                        renderedMap.Add(sb.ToString());
                    }

                    HashSet<(int x, int y)> seen = new HashSet<(int x, int y)>();
                    int highestConnectedNodeCount = 0;
                    for (int i = 0; i < moduloY; i++)
                        for (int j = 0; j < moduloX; j++)
                        {
                            if (seen.Contains((j, i))
                                || renderedMap[i][j] == ' ')
                                continue;

                            int count = 0;
                            Queue<(int x, int y)> q = new();
                            q.Enqueue((j, i));
                            while (q.Count > 0)
                            {
                                var curr = q.Dequeue();

                                if (!seen.Add(curr))
                                    continue;

                                count++;

                                for (int xStep = -1; xStep <= 1; xStep++)
                                    for (int yStep = -1; yStep <= 1; yStep++)
                                    {
                                        if (xStep == 0 && yStep == 0
                                            || curr.y + yStep < 0 || curr.y + yStep >= moduloY
                                            || curr.x + xStep < 0 || curr.x + xStep >= moduloX)
                                            continue;

                                        if (renderedMap[curr.y + yStep][curr.x + xStep] == '#')
                                            q.Enqueue((curr.x + xStep, curr.y + yStep));
                                    }
                            }

                            highestConnectedNodeCount = Math.Max(highestConnectedNodeCount, count);
                        }

                    double currentRatio = highestConnectedNodeCount / (double)robots.Count;
                    alltimeHighestConnectionRatio = Math.Max(alltimeHighestConnectionRatio, currentRatio);

                    Console.WriteLine($"{iter}. iteration skipped | current ratio {currentRatio} | best ratio {alltimeHighestConnectionRatio}");
                    //At least 40% of the robots are connected
                    if (alltimeHighestConnectionRatio > 0.4)
                    {
                        //7688.iteration found a configuration with the ratio of 0.458!!
                        Console.WriteLine($"{iter}. iteration found a configuration with the ratio of {alltimeHighestConnectionRatio}!!");
                        foreach (var item in renderedMap)
                            Console.WriteLine(item);
                        Console.WriteLine("press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

                foreach (var item in robots)
                {
                    int nextX = (item.Pos.x + item.Velocity.x) % moduloX;
                    if (nextX < 0)
                        nextX += moduloX;
                    int nextY = (item.Pos.y + item.Velocity.y) % moduloY;
                    if (nextY < 0)
                        nextY += moduloY;
                    item.Pos = (nextX, nextY);
                }
            }

            int horizontalDivide = moduloX / 2;
            int verticalDivide = moduloY / 2;

            int topLeft = robots.Count(kk => kk.Pos.x < horizontalDivide && kk.Pos.y < verticalDivide);
            int topRight = robots.Count(kk => kk.Pos.x > horizontalDivide && kk.Pos.y < verticalDivide);
            int bottomLeft = robots.Count(kk => kk.Pos.x < horizontalDivide && kk.Pos.y > verticalDivide);
            int bottomRight = robots.Count(kk => kk.Pos.x > horizontalDivide && kk.Pos.y > verticalDivide);

            int solution = topLeft * topRight * bottomLeft * bottomRight;

            // Innitial tip to see the scale (can it be done by hand selection or somethign smarter is needed)
            // 5000 seconds is low

            // Data:
            // until 7000 best ratio is 0.068
            // 7687 with ratio of 0.458
        }
    }
}
