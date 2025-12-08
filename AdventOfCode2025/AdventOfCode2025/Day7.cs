namespace AdventOfCode2025
{
    public class Day7 : IDay
    {
        public string InputPath { get; set; } = "day7Input.txt";
        //public string InputPath { get; set; } = "day7InputTest.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            HashSet<(int x, int y)> visited = new();
            Queue<(int x, int y)> q = new();
            for (int i = 0; i < s.Length && q.Count == 0; i++)
                for (int j = 0; j < s[i].Length && q.Count == 0; j++)
                    if (s[i][j] == 'S')
                        q.Enqueue((j, i));

            int solution = 0;
            while (q.Count != 0)
            {
                var curr = q.Dequeue();
                if (curr.y >= s.Length
                    || !visited.Add(curr))
                    continue;

                if (s[curr.y][curr.x] != '^')
                {
                    q.Enqueue((curr.x, curr.y + 1));
                    continue;
                }

                // s[curr.y][curr.x] == '^' // Splitting
                ++solution;
                // Left 
                if (curr.x - 1 >= 0)
                    q.Enqueue((curr.x - 1, curr.y - 1));

                // Right
                if (curr.x + 1 < s[curr.y].Length)
                    q.Enqueue((curr.x + 1, curr.y - 1));
            }

            // 1600
            Console.WriteLine($"The beam will be split {solution} times");
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            (int x, int y) startPos = (-1, -1);
            for (int i = 0; i < s.Length && startPos.x == -1; i++)
                for (int j = 0; j < s[i].Length && startPos.x == -1; j++)
                    if (s[i][j] == 'S')
                        startPos = (j, i);

            Dictionary<(int x, int y), long> memo = new();

            long recursiveSolution((int x, int y) curr)
            {
                // Already memoized
                if(memo.ContainsKey(curr))
                    return memo[curr];

                if (curr.y >= s.Length)
                    return 1;

                // Just straight
                if (s[curr.y][curr.x] != '^')
                {
                    memo.Add(curr, recursiveSolution((curr.x, curr.y + 1)));
                    return memo[curr];
                }

                // Splitting
                // Left 
                long tempSolution = 0;
                if (curr.x - 1 >= 0)
                    tempSolution += recursiveSolution((curr.x - 1, curr.y - 1));

                // Right
                if (curr.x + 1 < s[curr.y].Length)
                    tempSolution += recursiveSolution((curr.x + 1, curr.y - 1));

                memo.Add(curr, tempSolution);
                return tempSolution;
            }

            long solution = recursiveSolution(startPos);
            
            // 6623
            Console.WriteLine($"In total there would be {solution} different timelines.");
        }

        public void NaivePartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            Stack<(int x, int y)> stack = new();
            for (int i = 0; i < s.Length && stack.Count == 0; i++)
                for (int j = 0; j < s[i].Length && stack.Count == 0; j++)
                    if (s[i][j] == 'S')
                        stack.Push((j, i));

            int solution = 0;
            while (stack.Count != 0)
            {
                var curr = stack.Pop();
                if (curr.y >= s.Length)
                {
                    ++solution;
                    continue;
                }

                if (s[curr.y][curr.x] != '^')
                {
                    stack.Push((curr.x, curr.y + 1));
                    continue;
                }

                // s[curr.y][curr.x] == '^' // Splitting
                // Left 
                if (curr.x - 1 >= 0)
                    stack.Push((curr.x - 1, curr.y - 1));

                // Right
                if (curr.x + 1 < s[curr.y].Length)
                    stack.Push((curr.x + 1, curr.y - 1));
            }

            // 6623
            Console.WriteLine($"In total there would be {solution} different timelines.");
        }
    }
}
