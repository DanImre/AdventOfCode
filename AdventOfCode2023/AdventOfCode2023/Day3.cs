using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day3
    {
        public Day3()
        {

        }

        public int PartOne() //544433
        {
            char[][] s = File.ReadAllLines("day3.txt").Select(kk => kk.ToArray()).ToArray();

            int solution = 0;

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if(s[i][j] != '.' && !char.IsDigit(s[i][j]))
                    {
                        if (i > 0 && j > 0 && char.IsDigit(s[i - 1][j - 1])) //TopLeft
                            solution += GetNumber(i - 1, j - 1);
                        if (i > 0 && char.IsDigit(s[i - 1][j])) //Top
                            solution += GetNumber(i - 1, j);
                        if (i > 0 && j < s[i].Length - 1 && char.IsDigit(s[i - 1][j + 1])) //TopRight
                            solution += GetNumber(i - 1, j + 1);
                        if (j < s[i].Length - 1 && char.IsDigit(s[i][j + 1])) //Right
                            solution += GetNumber(i, j + 1);
                        if (i < s.Length - 1 && j < s[i].Length - 1 && char.IsDigit(s[i + 1][j + 1])) //DownRight
                            solution += GetNumber(i + 1, j + 1);
                        if (i < s.Length - 1 && char.IsDigit(s[i + 1][j])) //Down
                            solution += GetNumber(i + 1, j);
                        if (i < s.Length - 1 && j > 0 && char.IsDigit(s[i + 1][j - 1])) //DownLeft
                            solution += GetNumber(i + 1, j - 1);
                        if (j > 0 && char.IsDigit(s[i][j - 1])) //Left
                            solution += GetNumber(i, j - 1);
                    }

            int GetNumber(int i, int jStart)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = jStart; j >= 0 && char.IsDigit(s[i][j]); j--) //to the left of jStart
                {
                    sb.Insert(0, s[i][j]);
                    s[i][j] = '.';
                }
                for (int j = jStart + 1; j < s[i].Length && char.IsDigit(s[i][j]); j++) //to the right of jStart
                {
                    sb.Append(s[i][j]);
                    s[i][j] = '.';
                }
                return int.Parse(sb.ToString());
            }

            return solution;
        }

        
        public long PartTwo() //76314915
        {
            char[][] s = File.ReadAllLines("day3árpi.txt").Select(kk => kk.ToArray()).ToArray();

            long solution = 0;

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                {
                    int count = 0;
                    long product = 1;
                    if (s[i][j] == '*')
                    {
                        if (i > 0 && j > 0 && char.IsDigit(s[i - 1][j - 1])) //TopLeft
                        {
                            ++count;
                            product *= GetNumber(i - 1, j - 1);
                        }
                        if (i > 0 && char.IsDigit(s[i - 1][j])) //Top
                        {
                            ++count;
                            product *= GetNumber(i - 1, j);
                        }
                        if (i > 0 && j < s[i].Length - 1 && char.IsDigit(s[i - 1][j + 1])) //TopRight
                        {
                            ++count;
                            product *= GetNumber(i - 1, j + 1);
                        }
                        if (j < s[i].Length - 1 && char.IsDigit(s[i][j + 1])) //Right
                        {
                            ++count;
                            product *= GetNumber(i, j + 1);
                        }
                        if (i < s.Length - 1 && j < s[i].Length - 1 && char.IsDigit(s[i + 1][j + 1])) //DownRight
                        {
                            ++count;
                            product *= GetNumber(i + 1, j + 1);
                        }
                        if (i < s.Length - 1 && char.IsDigit(s[i + 1][j])) //Down
                        {
                            ++count;
                            product *= GetNumber(i + 1, j);
                        }
                        if (i < s.Length - 1 && j > 0 && char.IsDigit(s[i + 1][j - 1])) //DownLeft
                        {
                            ++count;
                            product *= GetNumber(i + 1, j - 1);
                        }
                        if (j > 0 && char.IsDigit(s[i][j - 1])) //Left
                        {
                            ++count;
                            product *= GetNumber(i, j - 1);
                        }
                    }
                    if (count == 2)
                        solution += product;
                }

            int GetNumber(int i, int jStart)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = jStart; j >= 0 && char.IsDigit(s[i][j]); j--) //to the left of jStart
                {
                    sb.Insert(0, s[i][j]);
                    s[i][j] = '.';
                }
                for (int j = jStart + 1; j < s[i].Length && char.IsDigit(s[i][j]); j++) //to the right of jStart
                {
                    sb.Append(s[i][j]);
                    s[i][j] = '.';
                }
                return int.Parse(sb.ToString());
            }

            return solution;
        }
    }
}
