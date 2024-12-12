using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day10 : IDay
    {
        public string InputPath { get; set; } = "day10Input.txt";
        //public string InputPath { get; set; } = "day10TestInput.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            int solution = 0;
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == '0')
                    {
                        HashSet<(int y, int x)> foundDestinations = new HashSet<(int y, int x)>();
                        Queue<(int y, int x)> q = new Queue<(int y, int x)>();
                        q.Enqueue((i, j));

                        while (q.Count > 0)
                        {
                            var curr = q.Dequeue();

                            int currValue = s[curr.y][curr.x] - '0';
                            if (currValue == 9)
                            {
                                foundDestinations.Add(curr);
                                continue;
                            }

                            if (curr.y > 0 && s[curr.y - 1][curr.x] - '0' == currValue + 1)
                                q.Enqueue((curr.y - 1, curr.x));
                            if (curr.y < s.Length - 1 && s[curr.y + 1][curr.x] - '0' == currValue + 1)
                                q.Enqueue((curr.y + 1, curr.x));
                            if (curr.x > 0 && s[curr.y][curr.x - 1] - '0' == currValue + 1)
                                q.Enqueue((curr.y, curr.x - 1));
                            if (curr.x < s[curr.y].Length - 1 && s[curr.y][curr.x + 1] - '0' == currValue + 1)
                                q.Enqueue((curr.y, curr.x + 1));
                        }

                        solution += foundDestinations.Count;
                    }

            Console.WriteLine($"Sum of the scores of all trailheads is: {solution}"); // 468
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            Queue<(int y, int x)> q = new Queue<(int y, int x)>();

            int solution = 0;
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == '0')
                        q.Enqueue((i, j));

            while (q.Count > 0)
            {
                var curr = q.Dequeue();

                int currValue = s[curr.y][curr.x] - '0';
                if (currValue == 9)
                {
                    solution += 1;
                    continue;
                }

                if (curr.y > 0 && s[curr.y - 1][curr.x] - '0' == currValue + 1)
                    q.Enqueue((curr.y - 1, curr.x));
                if (curr.y < s.Length - 1 && s[curr.y + 1][curr.x] - '0' == currValue + 1)
                    q.Enqueue((curr.y + 1, curr.x));
                if (curr.x > 0 && s[curr.y][curr.x - 1] - '0' == currValue + 1)
                    q.Enqueue((curr.y, curr.x - 1));
                if (curr.x < s[curr.y].Length - 1 && s[curr.y][curr.x + 1] - '0' == currValue + 1)
                    q.Enqueue((curr.y, curr.x + 1));
            }

            Console.WriteLine($"Sum of the scores of all trailheads is: {solution}"); // 468 too high
        }
    }
}
