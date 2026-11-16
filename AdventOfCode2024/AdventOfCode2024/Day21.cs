namespace AdventOfCode2024
{
    public class Day21 : IDay
    {
        public string InputPath { get; set; } = "day21Input.txt";
        //public string InputPath { get; set; } = "day21InputTest.txt";

        /// <summary>
        /// Solution roadmap:
        /// 1. Create 2 functions so that:
        ///     1 translates the moves needed from a numeric input to moves needed from a directional input (like 4 -> 9 is '>>^A' or '^>>A', repeating is always preferred)
        ///     2 translate the moves from a directional input to an other (like '<' -> '^' is '>^A' or '^>A')
        /// Then call these functions recursively from the bottom up,
        /// because we need the shortest possible answer we need to do multiple paralell sequences where the shortest one is not apparent
        /// </summary>
        public void PartOne()
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
                if(startY == 3 && endX == 0)
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

            long solution = 0;

            string[] s = File.ReadAllLines(InputPath);
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

                //Console.WriteLine(item + " :");
                //foreach (var aaaa in sol)
                //    Console.WriteLine("\t" + aaaa);

                //Console.WriteLine();
                //Console.WriteLine("----------------------------------------");
                //Console.WriteLine();

                var solCopy = sol.ToList();
                sol = [];

                foreach (var seq in solCopy)
                {
                    List<string> innerSol = [""];
                    prevPoint = 'A';
                    foreach (var character in seq)
                    {
                        List<string> char_sol = DirectionalToDirectional(prevPoint, character);
                        innerSol = [.. innerSol.SelectMany(x => char_sol, (x, y) => (Item1: x, Item2: y)).Select(x => x.Item1 + x.Item2)];
                        prevPoint = character;
                    }

                    sol.AddRange(innerSol);
                }

                minLength = sol.Min(x => x.Length);
                sol = [.. sol.Where(x => x.Length == minLength).Distinct()];

                solCopy = sol.ToList();
                sol = [];

                foreach (var seq in solCopy)
                {
                    List<string> innerSol = [""];
                    prevPoint = 'A';
                    foreach (var character in seq)
                    {
                        List<string> char_sol = DirectionalToDirectional(prevPoint, character);
                        innerSol = [.. innerSol.SelectMany(x => char_sol, (x, y) => (Item1: x, Item2: y)).Select(x => x.Item1 + x.Item2)];
                        prevPoint = character;
                    }

                    sol.AddRange(innerSol);
                }

                minLength = sol.Min(x => x.Length);
                long itemAsNumber = long.Parse(new string(item.Where(x => char.IsNumber(x)).ToArray()));
                long complexity = minLength * itemAsNumber;

                Console.WriteLine($"{item}: {minLength} * {itemAsNumber} => {complexity}");
                solution += complexity;
            }

            //Console.WriteLine("Numeric to directional tests:");
            //Console.WriteLine("'4', '9'   ->     " + string.Join(" |||| ", NumericToDirectional('4', '9')));
            //Console.WriteLine("'A', '7'   ->     " + string.Join(" |||| ", NumericToDirectional('A', '7')));
            //Console.WriteLine("'0', '5'   ->     " + string.Join(" |||| ", NumericToDirectional('0', '5')));
            //Console.WriteLine("Directiona to directional tests:");
            //Console.WriteLine("'<', '^'   ->     " + string.Join(" |||| ", DirectionalToDirectional('<', '^')));
            //Console.WriteLine("'<', '>'   ->     " + string.Join(" |||| ", DirectionalToDirectional('<', '>')));
            //Console.WriteLine("'v', '^'   ->     " + string.Join(" |||| ", DirectionalToDirectional('v', '^')));
            //Console.WriteLine("'<', 'A'   ->     " + string.Join(" |||| ", DirectionalToDirectional('<', 'A')));
            //Console.WriteLine("Combined tests:");
            //Console.WriteLine("'2', '9'   ->     " + string.Join(" |||| ", DirectionalToDirectional('A', 'A')));

            

            // For the test it should be: 126384
            Console.WriteLine($"the sum of the complexities of the five codes on my list is: {solution}!");
        }

        public void PartTwo()
        {
            int solution = 0;
            Console.WriteLine($"The total distance between my lists is: {solution}!");
        }
    }
}
