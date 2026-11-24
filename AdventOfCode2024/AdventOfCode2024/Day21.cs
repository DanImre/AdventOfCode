using System.Net.Http.Headers;

namespace AdventOfCode2024
{
    public class Day21 : IDay
    {
        private int NumberOfDirectionalRobots = 2;

        /// <summary>
        /// Solution roadmap:
        /// 1. Create 2 functions so that:
        ///     1 translates the moves needed from a numeric input to moves needed from a directional input (like 4 -> 9 is '>>^A' or '^>>A', repeating is always preferred)
        ///     2 translate the moves from a directional input to an other (like '<' -> '^' is '>^A' or '^>A')
        /// Then call these functions recursively from the bottom up,
        /// because we need the shortest possible answer we need to do multiple paralell sequences where the shortest one is not apparent
        /// </summary>
        public void PartOne(string inputPath)
        {
            char[,] numericMap = new char[4, 3]
            {
                { '7', '8', '9' },
                { '4', '5', '6' },
                { '1', '2', '3' },
                { '_', '0', 'A' }
            };

            Dictionary<char, (int, int)> numericDict = new();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 3; j++)
                    numericDict.Add(numericMap[i, j], (i, j));

            char[,] directionMap = new char[2, 3]
            {
                { '_', '^', 'A' },
                { '<', 'v', '>' }
            };

            Dictionary<char, (int, int)> directionalDict = new();
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 3; j++)
                    directionalDict.Add(directionMap[i, j], (i, j));

            List<string> NumericToDirectional(char startNode, char endNode)
            {
                if (startNode == endNode)
                    return ["A"];

                (int startY, int startX) = numericDict[startNode];
                (int endY, int endX) = numericDict[endNode];

                int horizontalVal = startX - endX;
                int verticalVal = startY - endY;

                string horizontalPart = string.Empty;

                if (horizontalVal < 0)
                    horizontalPart = new string('>', Math.Abs(horizontalVal));
                else if (horizontalVal > 0)
                    horizontalPart = new string('<', horizontalVal);

                string verticalPart = string.Empty;

                if (verticalVal < 0)
                    verticalPart = new string('v', Math.Abs(verticalVal));
                else if (verticalVal > 0)
                    verticalPart = new string('^', verticalVal);

                if (string.IsNullOrEmpty(verticalPart))
                    return [horizontalPart + "A"];

                if (string.IsNullOrEmpty(horizontalPart))
                    return [verticalPart + "A"];

                if (endY == 3 && startX == 0)
                    return [horizontalPart + verticalPart + "A"];
                if (startY == 3 && endX == 0)
                    return [verticalPart + horizontalPart + "A"];

                return [horizontalPart + verticalPart + "A", verticalPart + horizontalPart + "A"];
            }

            //List<string> DirectionalToDirectional(char startNode, char endNode)
            string DirectionalToDirectional(char startNode, char endNode)
            {
                if (startNode == endNode)
                    return "A";

                (int startY, int startX) = directionalDict[startNode];
                (int endY, int endX) = directionalDict[endNode];

                int horizontalVal = startX - endX;
                int verticalVal = startY - endY;

                string horizontalPart = string.Empty;

                if (horizontalVal < 0)
                    horizontalPart = new string('>', Math.Abs(horizontalVal));
                else if (horizontalVal > 0)
                    horizontalPart = new string('<', horizontalVal);

                string verticalPart = string.Empty;

                if (verticalVal < 0)
                    verticalPart = new string('v', Math.Abs(verticalVal));
                else if (verticalVal > 0)
                    verticalPart = new string('^', verticalVal);

                if (string.IsNullOrEmpty(verticalPart))
                    return horizontalPart + "A";

                if (string.IsNullOrEmpty(horizontalPart))
                    return verticalPart + "A";

                if (endY == 1 && endX == 0 && startY == 0)
                    return verticalPart + horizontalPart + "A";

                if (startY == 1 && startX == 0 && endY == 0)
                    return horizontalPart + verticalPart + "A";

                return verticalPart + horizontalPart + "A";
                //return [horizontalPart + verticalPart + "A", verticalPart + horizontalPart + "A"];
            }

            long solution = 0;

            string[] s = File.ReadAllLines(inputPath);
            foreach (var item in s)
            {
                List<string> sol = [""];

                char prevPoint = 'A';
                foreach (var character in item)
                {
                    List<string> char_sol = NumericToDirectional(prevPoint, character);
                    sol = [.. sol.SelectMany(x => char_sol, (x, y) => (Item1: x, Item2: y)).Select(x => x.Item1 + x.Item2)];
                    prevPoint = character;
                }

                int minLength = sol.Min(x => x.Length);
                sol = [.. sol.Where(x => x.Length == minLength)];

                Dictionary<char, string> memo = [];

                for (int i = 0; i < 2; i++)
                {
                    var solCopy = sol.ToList();
                    sol = [];

                    foreach (var seq in solCopy)
                    {
                        List<string> innerSol = [""];
                        prevPoint = 'A';
                        foreach (var character in seq)
                        {
                            //List<string> char_sol = DirectionalToDirectional(prevPoint, character);
                            //innerSol = [.. innerSol.SelectMany(x => char_sol, (x, y) => (Item1: x, Item2: y)).Select(x => x.Item1 + x.Item2)];
                            string char_sol = DirectionalToDirectional(prevPoint, character);
                            innerSol = [.. innerSol.Select(x => x + char_sol)];
                            prevPoint = character;
                        }

                        sol.AddRange(innerSol);
                    }

                    minLength = sol.Min(x => x.Length);
                    var Acount = sol.Min(x => x.Count(y => y == 'A'));
                    sol = [.. sol.Where(x => x.Length == minLength)];
                }

                minLength = sol.Min(x => x.Length);
                long itemAsNumber = long.Parse(new string(item.Where(x => char.IsNumber(x)).ToArray()));
                long complexity = minLength * itemAsNumber;

                Console.WriteLine($"{item}: {minLength} * {itemAsNumber} => {complexity}");
                solution += complexity;
            }

            // For the test it should be: 126384
            Console.WriteLine($"The sum of the complexities of the five codes on my list is: {solution}!");
        }

        // First part was pure luck, we do need to try out every path
        public void PartTwo(string inputPath)
        {
            char[,] numericMap = new char[4, 3]
            {
                { '7', '8', '9' },
                { '4', '5', '6' },
                { '1', '2', '3' },
                { '_', '0', 'A' }
            };

            Dictionary<char, (int, int)> numericDict = new();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 3; j++)
                    numericDict.Add(numericMap[i, j], (i, j));

            char[,] directionMap = new char[2, 3]
            {
                { '_', '^', 'A' },
                { '<', 'v', '>' }
            };

            Dictionary<char, (int, int)> directionalDict = new();
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 3; j++)
                    directionalDict.Add(directionMap[i, j], (i, j));

            List<string> NumericToDirectional(char startNode, char endNode)
            {
                if (startNode == endNode)
                    return ["A"];

                (int startY, int startX) = numericDict[startNode];
                (int endY, int endX) = numericDict[endNode];

                int horizontalVal = startX - endX;
                int verticalVal = startY - endY;

                string horizontalPart = string.Empty;

                if (horizontalVal < 0)
                    horizontalPart = new string('>', Math.Abs(horizontalVal));
                else if (horizontalVal > 0)
                    horizontalPart = new string('<', horizontalVal);

                string verticalPart = string.Empty;

                if (verticalVal < 0)
                    verticalPart = new string('v', Math.Abs(verticalVal));
                else if (verticalVal > 0)
                    verticalPart = new string('^', verticalVal);

                if (string.IsNullOrEmpty(verticalPart))
                    return [horizontalPart + "A"];

                if (string.IsNullOrEmpty(horizontalPart))
                    return [verticalPart + "A"];

                if (endY == 3 && startX == 0)
                    return [horizontalPart + verticalPart + "A"];
                if (startY == 3 && endX == 0)
                    return [verticalPart + horizontalPart + "A"];

                return [horizontalPart + verticalPart + "A", verticalPart + horizontalPart + "A"];
            }

            List<string> DirectionalToDirectional(char startNode, char endNode)
            {
                if (startNode == endNode)
                    return ["A"];

                (int startY, int startX) = directionalDict[startNode];
                (int endY, int endX) = directionalDict[endNode];

                int horizontalVal = startX - endX;
                int verticalVal = startY - endY;

                string horizontalPart = string.Empty;

                if (horizontalVal < 0)
                    horizontalPart = new string('>', Math.Abs(horizontalVal));
                else if (horizontalVal > 0)
                    horizontalPart = new string('<', horizontalVal);

                string verticalPart = string.Empty;

                if (verticalVal < 0)
                    verticalPart = new string('v', Math.Abs(verticalVal));
                else if (verticalVal > 0)
                    verticalPart = new string('^', verticalVal);

                if (string.IsNullOrEmpty(verticalPart))
                    return [horizontalPart + "A"];

                if (string.IsNullOrEmpty(horizontalPart))
                    return [verticalPart + "A"];

                if (endY == 1 && endX == 0 && startY == 0)
                    return [verticalPart + horizontalPart + "A"];

                if (startY == 1 && startX == 0 && endY == 0)
                    return [horizontalPart + verticalPart + "A"];

                return [horizontalPart + verticalPart + "A", verticalPart + horizontalPart + "A"];
            }

            Dictionary<(char from, char to, int d), long> memo = [];

            long Seq(char from, char to, int depth)
            {
                if (depth == 0)
                    return 1;

                if (memo.TryGetValue((from, to, depth), out long memoVal))
                    return memoVal;

                List<string> arr = DirectionalToDirectional(from, to);
                long bestSum = long.MaxValue;
                foreach (var item in arr)
                {
                    long tempSum = Seq('A', item[0], depth - 1);
                    for (int i = 0; i < item.Length - 1; i++)
                        tempSum += Seq(item[i], item[i + 1], depth - 1);

                    bestSum = Math.Min(bestSum, tempSum);
                }

                memo.Add((from, to, depth), bestSum);
                return bestSum;
            }

            long solution = 0;
            string[] s = File.ReadAllLines(inputPath);
            foreach (var item in s)
            {
                List<string> sol = [""];

                char prevPoint = 'A';
                foreach (var character in item)
                {
                    List<string> char_sol = NumericToDirectional(prevPoint, character);
                    sol = [.. sol.SelectMany(x => char_sol, (x, y) => (Item1: x, Item2: y)).Select(x => x.Item1 + x.Item2)];
                    prevPoint = character;
                }

                int depth = 25;
                long itemAsNumber = long.Parse(new string([.. item.Where(char.IsNumber)]));

                long bestCurrSol = long.MaxValue;
                long complexity = long.MaxValue;
                foreach (var curr in sol)
                {

                    long currSol = Seq('A', curr[0], depth);
                    for (int i = 0; i < curr.Length - 1; i++)
                        currSol += Seq(curr[i], curr[i + 1], depth);


                    complexity = Math.Min(complexity, currSol * itemAsNumber);
                    bestCurrSol = Math.Min(bestCurrSol, currSol);
                }

                Console.WriteLine($"{item}: {bestCurrSol} * {itemAsNumber} => {complexity}");

                solution += complexity;
            }

            // 473033448486482 too high
            // 484012536848010 way too high
            // 463811373093070 too high
            // 252473394928452 FINALLY
            Console.WriteLine($"The sum of the complexities of the five codes on my list is: {solution}!");
        }
    }
}
