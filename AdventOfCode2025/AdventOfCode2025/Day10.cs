using Elsheimy.Components.Linears;

namespace AdventOfCode2025
{
    public class Day10 : IDay
    {
        //public string InputPath { get; set; } = "day10Input.txt";
        public string InputPath { get; set; } = "day10InputTest.txt";

        public void PartOne()
        {
            (int correctAnswer, int[] arr)[] s = File.ReadAllLines(InputPath)
                .Select(x =>
                {
                    string[] split = x.Split(" ").SkipLast(1).ToArray();
                    string leftSide = split[0].Trim(['[', ']']);
                    int solution = 0;
                    for (int i = 0; i < leftSide.Length; i++)
                        if (leftSide[i] == '#')
                            solution |= 1 << i;

                    int[] rightSide = split
                        .Skip(1)
                        .Select(x => x.Trim(['(', ')'])
                            .Split(",")
                            .Select(y => 1 << int.Parse(y))
                            .Aggregate((a, b) => a ^ b))
                        .ToArray();

                    return (solution, rightSide);
                })
                .ToArray();

            int solution = 0;

            int recursiveSolution(int row, int curr, int count, int index)
            {
                if ((curr ^ s[row].correctAnswer) == 0)
                    return count;

                if (index == s[row].arr.Length)
                    return int.MaxValue;

                int skipping = recursiveSolution(row, curr, count, index + 1);
                int pressing = recursiveSolution(row, curr ^ s[row].arr[index], count + 1, index + 1);

                return Math.Min(skipping, pressing);
            }

            for (int i = 0; i < s.Length; i++)
                solution += recursiveSolution(i, 0, 0, 0);

            // 469
            Console.WriteLine($"The fewest button presses required to correctly configure the indicator lights on all of the machines is {solution}");
        }

        public void PartTwo()
        {
            (int[] correctAnswer, int[][] arr)[] s = File.ReadAllLines(InputPath)
                .Select(x =>
                {
                    string[] split = x.Split(" ").Skip(1).ToArray();

                    int[][] leftSide = split
                        .SkipLast(1)
                        .Select(x => x.Trim(['(', ')'])
                            .Split(",")
                            .Select(int.Parse)
                            .ToArray())
                        .ToArray();

                    int[] rightSide = split[^1]
                        .Trim(['{', '}'])
                        .Split(",")
                        .Select(int.Parse)
                        .ToArray();

                    return (rightSide, leftSide);
                })
                .ToArray();

            for (int i = 0; i < s.Length; i++)
            {
                var row = s[i];
                int rowCount = row.correctAnswer.Length;
                int columnCount = row.arr.Length + 1;
                Matrix m = new(rowCount, columnCount);

                for (int j = 0; j < row.arr.Length; j++)
                {
                    for (int l = 0; l < row.arr[j].Length; l++)
                        m[row.arr[j][l], j] = 1;
                }

                for (int j = 0; j < rowCount; j++)
                    m[j, columnCount - 1] = row.correctAnswer[j];

                Matrix reduced = m.Reduce(MatrixReductionForm.ReducedRowEchelonForm);

                double solutionSum = 0;
                List<Func<double[], bool>> validations = new();
                for (int l = 0; l < rowCount; l++)
                {
                    solutionSum += reduced[l, columnCount - 1];
                    double[] currRow = reduced.GetRowVectors()[l].InnerArray;
                    validations.Add((double[] input) => currRow.Zip(input).SkipLast(1).Sum(x => x.First * x.Second) == currRow[^1]);
                }

                List<(int colIndex, double sum)> freeVariables = [];
                for (int j = 0; j < columnCount - 1; j++)
                {
                    int count = 0;
                    double freeVarCount = 0;
                    for (int l = 0; l < rowCount; l++)
                        if (reduced[l, j] != 0)
                        {
                            freeVarCount += -reduced[l, j];
                            count++;
                        }

                    if (count != 1)
                        freeVariables.Add((j, freeVarCount + 1));
                }

                // S = sum(solutions) + free variable counts
            }

            return;

            // 469
            //Console.WriteLine($"The fewest button presses required to correctly configure the joltage level counters on all of the machines is {solution}");
        }
    }
}
