using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day4 : IDay
    {
        public string InputPath { get; set; } = "day4Input.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            string word = "XMAS";

            List<(int y, int x)> moves = new List<(int y, int x)>();
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                    if (i != 0 || j != 0)
                        moves.Add((i, j));


            int CountXmasWordsOriginatingFromHere(int y, int x)
            {
                // Needs to start with an X
                if (s[y][x] != 'X')
                    return 0;

                int solution = 0;

                // See every possible direction
                foreach (var move in moves)
                {
                    // If it would move of the map
                    if (y + 3 * move.y < 0
                       || y + 3 * move.y >= s.Length
                       || x + 3 * move.x < 0
                       || x + 3 * move.x >= s[y].Length)
                        continue;

                    bool isCorrect = true;
                    for (int i = 1; i < 4 && isCorrect; i++)
                        isCorrect &= s[y + i * move.y][x + i * move.x] == word[i];

                    if (isCorrect)
                        solution += 1;
                }

                return solution;
            }

            int solution = 0;

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    solution += CountXmasWordsOriginatingFromHere(i, j);

            Console.WriteLine($"The word XMAS appears a total of {solution} times!"); //2493
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            bool IsAnXmasCenter(int y, int x)
            {
                // The center needs to be an 'A' and it needs one space above, bellow, left and right to form an x
                if (s[y][x] != 'A'
                    || y == 0
                    || x == 0
                    || y == s.Length - 1
                    || x == s[y].Length - 1)
                    return false;

                // MUST BE
                // top left to bottom right
                // OR
                // bottom right to top left
                string fromTopLeftToBottomRight = s[y - 1][x - 1] + "A" + s[y + 1][x + 1];
                if (fromTopLeftToBottomRight != "MAS"
                    && fromTopLeftToBottomRight != "SAM")
                    return false;

                // MUST BE
                // top right to bottom left
                // OR
                // bottom left to top right
                string fromTopRightToBottomLeft = s[y - 1][x + 1] + "A" + s[y + 1][x - 1];
                if (fromTopRightToBottomLeft != "MAS"
                    && fromTopRightToBottomLeft != "SAM")
                    return false;


                return true;
            }

            int solution = 0;

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (IsAnXmasCenter(i, j))
                        solution += 1;

            Console.WriteLine($"There are a total of {solution} X-MAS appearances!"); //1890
        }
    }
}
