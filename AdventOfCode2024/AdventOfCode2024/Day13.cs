using DataStructures;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day13 : IDay
    {
        public string InputPath { get; set; } = "day13Input.txt";
        //public string InputPath { get; set; } = "day13TestInput.txt";

        public void PartOne()
        {
            // Idea, this is just a graph with weights if 1 and 3 BFS (Breadth-first search) + visited (Basicly Dijkstra)
            // Checked the input, numbers are not that big and There are only 'positive steps'

            // + 1 Idea: Heap, ordered with the distance to the prize, so we don't create as many useless branches
            // (we can let go after they passed the prize in the x or y axes, can't come back)
            // Would be great if the task was to just find the price, not the minimum cost


            int[][] s = File.ReadAllLines(InputPath)
                .Select(kk => kk.Split(new char[] { ',', ' ', ':', 'Y', 'X', '=' }, StringSplitOptions.RemoveEmptyEntries)
                    .TakeLast(2)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();

            List<ClawMachine> clawMachines = new();

            for (int i = 0; i < s.Length; i += 4)
            {
                int[] AStep = s[i];
                int[] BStep = s[i + 1];
                int[] Prize = s[i + 2];

                clawMachines.Add(new ClawMachine((AStep[0], AStep[1]), (BStep[0], BStep[1]), (Prize[0], Prize[1])));
            }

            int solution = 0;

            foreach (var currentClawMachine in clawMachines)
            {
                Dictionary<(int x, int y), int> visited = new();
                Queue<(int x, int y, int cost)> q = new();
                q.Enqueue((0, 0, 0));
                while (q.Count > 0)
                {
                    var curr = q.Dequeue();
                    var currPos = (curr.x, curr.y);

                    if (currentClawMachine.IsBeyondPrize(currPos)
                        || visited.ContainsKey(currPos) && visited[currPos] <= curr.cost)
                        continue;

                    visited[currPos] = curr.cost;

                    q.Enqueue(currentClawMachine.StepWith('A', curr));
                    q.Enqueue(currentClawMachine.StepWith('B', curr));
                }

                if (visited.ContainsKey(currentClawMachine.Prize))
                    solution += visited[currentClawMachine.Prize];
            }

            Console.WriteLine($"The fewest tokens you would have to spend to win all possible prizes is {solution}"); // 31623
        }

        public void PartTwo()
        {
            // Idea, create a vector that which we can multiply to get the prize's position
            // Best I can think of is to try up to like 10-20 iterations, choose the best one
            // Didn't work, even with 2000 iterations, no vectors aligned exactly with the prize

            // Idea, these are the equations
            //      a*<c1> + b<c2> = <c3>
            //      a*<c4> + b<c5> = <c6>
            //      'a' is the number of times we press a
            //      'b is the number of times we press b
            //      <c1-6> are the known constants from the txt (<c1> is the AStep.x for example)
            // This means there is only 1 solution (*)
            // (*) If there were multiple solutions, 1 vector would have to be a multiple of the other, in which case if a < 3*b then we use b only otherwise a only
            // In the input this never accours tho

            int[][] s = File.ReadAllLines(InputPath)
                .Select(kk => kk.Split(new char[] { ',', ' ', ':', 'Y', 'X', '=' }, StringSplitOptions.RemoveEmptyEntries)
                    .TakeLast(2)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();

            List<ClawMachine> clawMachines = new();

            for (int i = 0; i < s.Length; i += 4)
            {
                int[] AStep = s[i];
                int[] BStep = s[i + 1];
                int[] Prize = s[i + 2];

                clawMachines.Add(new ClawMachine((AStep[0], AStep[1]), (BStep[0], BStep[1]), (Prize[0], Prize[1])));
            }

            long ADDTOPRIZECORS = 10000000000000;
            long solution = 0;

            foreach (var currentClawMachine in clawMachines)
            {
                var A = Matrix<double>.Build.DenseOfArray(
                    new double[,] {
                        { currentClawMachine.AStep.x, currentClawMachine.BStep.x },
                        { currentClawMachine.AStep.y, currentClawMachine.BStep.y }
                    });

                //double tryoutX = (currentClawMachine.Prize.x + ADDTOPRIZECORS) / 100.0;
                //double tryoutY = (currentClawMachine.Prize.y + ADDTOPRIZECORS) / 100.0;

                //var A = Matrix<double>.Build.DenseOfArray(
                //    new double[,] {
                //        { tryoutX, tryoutX },
                //        { tryoutY, tryoutY }
                //    });
                // Looks like it gives NaN if there are multiple solution
                // Checked the on the input, no NaN values

                var b = Vector<double>.Build.Dense(new double[] { currentClawMachine.Prize.x + ADDTOPRIZECORS, currentClawMachine.Prize.y + ADDTOPRIZECORS });
                var x = A.Solve(b);

                double aRound = Math.Round(x[0], 3);
                double bRound = Math.Round(x[1], 3);

                if (aRound % 1 != 0
                    || bRound % 1 != 0)
                    continue;

                solution += (long)(aRound * 3 + bRound);
            }

            Console.WriteLine($"The fewest tokens you would have to spend to win all possible prizes is {solution}"); //93209116744825
        }
    }
}
