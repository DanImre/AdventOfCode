using System.Linq;

namespace AdventOfCode2025
{
    public class Day8 : IDay
    {
        public string InputPath { get; set; } = "day8Input.txt";
        //public string InputPath { get; set; } = "day8InputTest.txt";

        public void PartOne()
        {
            double[][] s = File.ReadAllLines(InputPath)
                .Select(x => x.Split(",")
                    .Select(double.Parse)
                    .ToArray())
                .ToArray();

            Dictionary<string, int> circuits = [];
            int index = 0;
            foreach (double[] item in s)
                circuits.Add(string.Join(',', item), index++);

            List<(double[] arr1, double[] arr2, double dist)> orderedDistancePairs = s.SelectMany(x => s, (arr1, arr2) => (arr1, arr2)).Where(x => x.arr1 != x.arr2)
                .Select(x => (
                    x.arr1,
                    x.arr2,
                    Math.Sqrt(Math.Pow(x.arr1[0] - x.arr2[0], 2) +
                        Math.Pow(x.arr1[1] - x.arr2[1], 2) +
                        Math.Pow(x.arr1[2] - x.arr2[2], 2))))
                .DistinctBy(x => x.Item3) //There are no equal distances
                .OrderBy(x => x.Item3)
                .Take(1000)
                .ToList();

            foreach (var item in orderedDistancePairs)
            {
                int arr1Circuit = circuits[string.Join(',', item.arr1)];
                int arr2Circuit = circuits[string.Join(',', item.arr2)];

                if (arr1Circuit == arr2Circuit)
                    continue;

                foreach (var circuitDesc in circuits)
                    if (circuitDesc.Value == arr2Circuit)
                        circuits[circuitDesc.Key] = arr1Circuit;
            }

            int solution = circuits.GroupBy(x => x.Value).Select(x => x.Count()).OrderByDescending(x => x).Take(3).Aggregate((a, b) => a * b);

            // 78894156
            Console.WriteLine($"If you multiply together the sizes of the three largest circuits you get: {solution}");
        }
        
        public void PartTwo()
        {
            double[][] s = File.ReadAllLines(InputPath)
                .Select(x => x.Split(",")
                    .Select(double.Parse)
                    .ToArray())
                .ToArray();

            Dictionary<string, int> circuits = [];
            int index = 0;
            foreach (double[] item in s)
                circuits.Add(string.Join(',', item), index++);

            List<(double[] arr1, double[] arr2, double dist)> orderedDistancePairs = s.SelectMany(x => s, (arr1, arr2) => (arr1, arr2)).Where(x => x.arr1 != x.arr2)
                .Select(x => (
                    x.arr1,
                    x.arr2,
                    Math.Sqrt(Math.Pow(x.arr1[0] - x.arr2[0], 2) +
                        Math.Pow(x.arr1[1] - x.arr2[1], 2) +
                        Math.Pow(x.arr1[2] - x.arr2[2], 2))))
                .DistinctBy(x => x.Item3) //There are no equal distances
                .OrderBy(x => x.Item3)
                .ToList();

            double solution = 0;

            int neededRealConnections = s.Length - 1; // n-1
            int realConnectionsMade = 0;
            foreach (var item in orderedDistancePairs)
            {
                int arr1Circuit = circuits[string.Join(',', item.arr1)];
                int arr2Circuit = circuits[string.Join(',', item.arr2)];

                if (arr1Circuit == arr2Circuit)
                    continue;

                realConnectionsMade++;
                if(realConnectionsMade == neededRealConnections)
                {
                    solution = item.arr1[0] * item.arr2[0];
                    break;
                }

                foreach (var circuitDesc in circuits)
                    if (circuitDesc.Value == arr2Circuit)
                        circuits[circuitDesc.Key] = arr1Circuit;
            }

            // 1287 too low
            Console.WriteLine($"If you multiply together the X coordinates of the last two junction boxes you need to connect you get: {solution}");
        }
    }
}
