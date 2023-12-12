using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day04
    {
        public Day04()
        {

        }

        public int PartOne() //23750
        {
            string[] s = File.ReadAllLines("day4.txt");

            int solution = 0;

            foreach (var line in s.Select(kk => kk.Split(':')[1]))
            {
                int[][] temp = line.Split("|").
                    Select(kk => kk.Split(' ', StringSplitOptions.RemoveEmptyEntries).
                    Select(kk => int.Parse(kk)).
                    ToArray()).ToArray();

                HashSet<int> left = new HashSet<int>(temp[0]);
                int score = 1;
                foreach (var item in temp[1])
                    if(left.Contains(item))
                        score <<= 1;

                solution += score >> 1;
            }

            return solution;
        }

        public long PartTwo() //13261850
        {
            string[] s = File.ReadAllLines("day4.txt");

            List<int> scores = new List<int>();

            foreach (var line in s.Select(kk => kk.Split(':')[1]))
            {
                int[][] temp = line.Split("|").
                    Select(kk => kk.Split(' ', StringSplitOptions.RemoveEmptyEntries).
                    Select(kk => int.Parse(kk)).
                    ToArray()).ToArray();

                HashSet<int> left = new HashSet<int>(temp[0]);
                int score = 0;
                foreach (var item in temp[1])
                    if (left.Contains(item))
                        score += 1;

                scores.Add(score);
            }

            long solution = 0;

            long[] amounts = new long[s.Length];

            for (int i = 0; i < scores.Count; i++)
            {
                amounts[i] += 1;
                for (int j = i + 1; j <= scores[i] + i && j < amounts.Length; j++)
                    amounts[j] += amounts[i];

                solution += amounts[i];
            }

            return solution;
        }

    }
}
