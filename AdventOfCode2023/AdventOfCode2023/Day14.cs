using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day14
    {
        public int PartOne()
        {
            //char[][] s = File.ReadAllLines("day14test.txt").Select(kk => kk.ToArray()).ToArray();
            char[][] s = File.ReadAllLines("day14.txt").Select(kk => kk.ToArray()).ToArray();

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[0].Length; j++)
                {
                    int temp = i;
                    while (temp > 0 && s[temp][j] == 'O' && s[temp - 1][j] == '.')
                    {
                        s[temp][j] = '.';
                        s[--temp][j] = 'O';
                    }
                }

            int solution = 0;
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == 'O')
                        solution += s.Length - i;

            return solution;
        }

        public int PartTwo()
        {
            //char[][] s = File.ReadAllLines("day14test.txt").Select(kk => kk.ToArray()).ToArray();
            char[][] s = File.ReadAllLines("day14.txt").Select(kk => kk.ToArray()).ToArray();

            Dictionary<string, int> dp = new Dictionary<string, int>();
            for (int z = 0; z < 1000000000; z++)
            {
                string key = string.Join("", s.Select(kk => string.Join("", kk)));

                if (dp.ContainsKey(key))
                {
                    int cycleLength = z - dp[key];
                    while (z + cycleLength < 1000000000)
                        z += cycleLength;
                }
                else
                    dp.Add(key, z);

                //North
                for (int i = 0; i < s.Length; i++)
                    for (int j = 0; j < s[0].Length; j++)
                    {
                        int temp = i;
                        while (temp > 0 && s[temp][j] == 'O' && s[temp - 1][j] == '.')
                        {
                            s[temp][j] = '.';
                            s[--temp][j] = 'O';
                        }
                    }

                //West
                for (int i = 0; i < s.Length; i++)
                    for (int j = 0; j < s[0].Length; j++)
                    {
                        int temp = j;
                        while (temp > 0 && s[i][temp] == 'O' && s[i][temp - 1] == '.')
                        {
                            s[i][temp] = '.';
                            s[i][--temp] = 'O';
                        }
                    }

                //South
                for (int i = s.Length - 1; i >= 0; i--)
                    for (int j = 0; j < s[0].Length; j++)
                    {
                        int temp = i;
                        while (temp < s.Length - 1 && s[temp][j] == 'O' && s[temp + 1][j] == '.')
                        {
                            s[temp][j] = '.';
                            s[++temp][j] = 'O';
                        }
                    }

                //East
                for (int i = 0; i < s.Length; i++)
                    for (int j = s[0].Length - 1; j >= 0; j--)
                    {
                        int temp = j;
                        while (temp < s[0].Length - 1 && s[i][temp] == 'O' && s[i][temp + 1] == '.')
                        {
                            s[i][temp] = '.';
                            s[i][++temp] = 'O';
                        }
                    }
            }

            int solution = 0;
            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == 'O')
                        solution += s.Length - i;

            return solution;
        }
    }
}
