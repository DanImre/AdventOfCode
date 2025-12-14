namespace AdventOfCode2024
{
    public class Day20 : IDay
    {
        public string InputPath { get; set; } = "day20Input.txt";
        //public string InputPath { get; set; } = "day20TestInput.txt";

        private List<(int y, int x)> steps = [(1, 0), (0, 1), (-1, 0), (0, -1)];

        public void PartOne()
        {
            // Idea, get best path, check for 1 gaps that save atleast 100 picoseconds like 415#4
            string[] s = File.ReadAllLines(InputPath);
            (int y, int x) start = (0, 0);
            (int y, int x) end = (0, 0);

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == 'S')
                        start = (i, j);
                    else if (s[i][j] == 'E')
                        end = (i, j);
                }

            (int y, int x)[,] dir = new (int y, int x)[s.Length, s[0].Length];
            HashSet<(int y, int x)> visited = new();

            Queue<(int y, int x)> q = new();
            q.Enqueue(start);

            while (q.Count > 0)
            {
                var curr = q.Dequeue();

                if (curr == end)
                    break;

                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                        if ((i == 0 || j == 0) && i != j)
                        {
                            (int y, int x) next = (curr.y + i, curr.x + j);
                            if (next.y >= 0
                                && next.y < s.Length
                                && next.x >= 0
                                && next.x < s[next.y].Length
                                && s[next.y][next.x] != '#'
                                && visited.Add(next))
                            {
                                q.Enqueue(next);
                                dir[next.y, next.x] = curr;
                            }
                        }
            }

            // has the path and the tiles' distence from the end
            Dictionary<(int y, int x), int> path = new();

            int distFromEnd = 0;
            (int y, int x) backtrack = end;
            while (backtrack != start)
            {
                path.Add(backtrack, distFromEnd++);
                backtrack = dir[backtrack.y, backtrack.x];
            }
            path.Add(start, distFromEnd);


            int picosecondsToSave = 100;
            long solution = 0;

            foreach (var item in path)
            {
                for (int i = -1; i <= 1; i++)
                    for (int j = -1; j <= 1; j++)
                        if ((i == 0 || j == 0) && i != j)
                        {
                            (int y, int x) checkPos = (item.Key.y + 2 * i, item.Key.x + 2 * j);
                            if (checkPos.y >= 0
                                && checkPos.y < s.Length
                                && checkPos.x >= 0
                                && checkPos.x < s[checkPos.y].Length
                                && s[item.Key.y + i][item.Key.x + j] == '#'
                                && path.ContainsKey(checkPos)
                                && item.Value - path[checkPos] - 2 >= picosecondsToSave)
                                solution++;
                        }
            }

            // 1360
            Console.WriteLine($"There are {solution} cheats that would save atleast {picosecondsToSave} picoseconds");
        }

        public void PartTwo()
        {
            // Idea, same: get best path, but now check paths (with max lengths of 20) that reach the other parts of the path that would save atleast 100 picoseconds
            // Problem: Might not need to go back to the best path, could come out at a dead end and still be closer then without cheating
            // Solution: Map every node's distance from the end

            string[] s = File.ReadAllLines(InputPath);
            (int y, int x) start = (0, 0);
            (int y, int x) end = (0, 0);

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == 'S')
                        start = (i, j);
                    else if (s[i][j] == 'E')
                        end = (i, j);
                }

            // the point's distance from the END point
            Dictionary<(int y, int x), int> distanceFromEnd = new();

            Queue<(int y, int x, int dist)> q = new();
            q.Enqueue((end.y, end.x, 0));

            while (q.Count > 0)
            {
                var curr = q.Dequeue();
                if (s[curr.y][curr.x] == '#')
                    continue;

                if (distanceFromEnd.ContainsKey((curr.y, curr.x)))
                    continue;

                distanceFromEnd[(curr.y, curr.x)] = curr.dist;

                foreach (var item in steps)
                {
                    (int y, int x) next = (curr.y + item.y, curr.x + item.x);
                    if (next.y >= 0
                        && next.y < s.Length
                        && next.x >= 0
                        && next.x < s[next.y].Length)
                    {
                        q.Enqueue((next.y, next.x, curr.dist + 1));
                    }
                }
            }

            int picosecondsToSave = 100;
            int maxPicosecondsPerCheat = 20;
            int solution = 0;

            int bestPathWithoutShortcuts = distanceFromEnd[start];

            foreach (var item in distanceFromEnd.Keys)
            {
                int distanceFromStartToItem = distanceFromEnd[start] - distanceFromEnd[item];

                for (int i = -maxPicosecondsPerCheat; i <= maxPicosecondsPerCheat; i++)
                    for (int j = -maxPicosecondsPerCheat; j <= maxPicosecondsPerCheat; j++)
                    {
                        int absI = Math.Abs(i);
                        int absJ = Math.Abs(j);
                        if (absI + absJ > maxPicosecondsPerCheat)
                            continue;

                        int nextY = item.y + i;
                        int nextX = item.x + j;
                        if (nextY < 0
                            || nextY >= s.Length
                            || nextX < 0
                            || nextX >= s[nextY].Length)
                            continue;

                        if (s[nextY][nextX] == '#')
                            continue;

                        int totalPathLength = distanceFromStartToItem;
                        totalPathLength += absI + absJ;
                        totalPathLength += distanceFromEnd[(nextY, nextX)];

                        if (bestPathWithoutShortcuts - totalPathLength >= picosecondsToSave)
                            ++solution;
                    }
            }

            // On the test it should be 285

            // 1005476
            Console.WriteLine($"There are {solution} cheats that would save atleast {picosecondsToSave} picoseconds!");
        }
    }
}
