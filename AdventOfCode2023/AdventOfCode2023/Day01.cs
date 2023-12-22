using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day01
    {
        public Day01()
        {

        }

        public int PartOne() //55208
        {
            return File.ReadAllLines("day1.txt").Select(kk => kk.Where(zz => char.IsDigit(zz))).Sum(kk => int.Parse(kk.First().ToString() + kk.Last().ToString()));
        }

        public long PartTwo() //54578
        {
            string[] s = File.ReadAllLines("day1.txt");
            Dictionary<string, int> dic = new Dictionary<string, int>()
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };

            int solution = 0;

            for (int i = 0; i < s.Length; i++)
            {
                //forwards
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (char.IsDigit(s[i][j]))
                    {
                        solution += int.Parse(s[i][j].ToString()) * 10;
                        break;
                    }
                    bool found = false;
                    string curr = "";
                    for (int z = 0; z < 5 && j + z < s[i].Length; z++)
                    {
                        curr += s[i][j + z];
                        if (z < 2)
                            continue;
                        if (dic.ContainsKey(curr))
                        {
                            solution += dic[curr] * 10;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        break;
                }

                //backwards
                for (int j = s[i].Length - 1; j >= 0; j--)
                {
                    if (char.IsDigit(s[i][j]))
                    {
                        solution += int.Parse(s[i][j].ToString());
                        break;
                    }
                    bool found = false;
                    string curr = "";
                    for (int z = 0; z < 5 && j + z < s[i].Length; z++)
                    {
                        curr += s[i][j + z];
                        if (z < 2)
                            continue;
                        if (dic.ContainsKey(curr))
                        {
                            solution += dic[curr];
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        break;
                }
            }

            return solution;
        }
    }
}
