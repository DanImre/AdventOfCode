using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public class Day12 : IDay
    {
        public string InputPath { get; set; } = "day12Input.txt";
        //public string InputPath { get; set; } = "day12InputTest.txt";

        // There are only OBVIOUS outcomes for this one... either you can fit all the presents or you can't.
        public void PartOne()
        {
            string s = File.ReadAllText(InputPath);

            List<int> shapes = s
                .Split(":")
                .Select(x => x.Count(y => y == '#'))
                .Where(x => x > 0)
                .ToList();

            List<(int[] matrixSize, int[] shapeCount)> sacenarios = s
                .Split("\n")
                .Skip(shapes.Count * 5)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x =>
                {
                    string[] splitted = x.Split(":", StringSplitOptions.TrimEntries);
                    int[] leftSide = splitted[0]
                        .Split('x', StringSplitOptions.TrimEntries)
                        .Select(int.Parse)
                        .ToArray();

                    int[] rightSide = splitted[1]
                        .Split(' ', StringSplitOptions.TrimEntries)
                        .Select(int.Parse)
                        .ToArray();
                    return (leftSide, rightSide);
                })
                .ToList();

            int solution = 0;
            foreach (var item in sacenarios)
            {
                int avaiableSpace = item.matrixSize[0] * item.matrixSize[1];
                int minimalSpaceReq = 0;
                for (int i = 0; i < item.shapeCount.Length; i++)
                    minimalSpaceReq += shapes[i] * item.shapeCount[i]; 

                if (avaiableSpace < minimalSpaceReq)
                    continue;

                int maximalSpaceReq = item.shapeCount.Sum();
                avaiableSpace = (item.matrixSize[0] / 3) * (item.matrixSize[1] / 3);
                if (avaiableSpace >= maximalSpaceReq)
                    solution++;
            }

            // 579 too low
            Console.WriteLine($"The are {solution} regions that can fit all of the presents listed.");
        }

        public void PartTwo()
        {
            Console.WriteLine("There was no part 2 :( The solution for the first one is: 579");
        }
    }
}
