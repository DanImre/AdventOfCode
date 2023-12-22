using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day12
    {
        public Day12()
        {

        }

        public int PartOne() //7090
        {
            //string[] s = File.ReadAllLines("day12test.txt");
            string[] s = File.ReadAllLines("day12.txt");

            int solution = 0;

            foreach (var item in s)
            {
                string[] temp = item.Split(' ');
                solution += RecursiveSolutionForPartOne(temp[0].ToCharArray(), 0, temp[1].Split(',').Select(kk => int.Parse(kk)).ToArray());
            }

            return solution;
        }

        public int RecursiveSolutionForPartOne(char[] line, int currIndex, int[] numbers)
        {
            if(currIndex == line.Length)
            {
                int section = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != '#')
                        continue;

                    int index = i + 1;
                    while (index < line.Length && line[index] == '#')
                        ++index;

                    if (section >= numbers.Length || index - i != numbers[section])
                        return 0;

                    ++section;
                    i = index;
                }

                if (section != numbers.Length)
                    return 0;

                return 1;
            }

            int solution = RecursiveSolutionForPartOne(line, currIndex + 1, numbers); // Didn't change anything
            if (line[currIndex] == '?')
            {
                line[currIndex] = '#';
                solution += RecursiveSolutionForPartOne(line, currIndex + 1, numbers); // changed
                line[currIndex] = '?';
            }

            return solution;
        }

        public long PartTwo() //6792010726878
        {
            //string[] s = File.ReadAllLines("day12test.txt");
            string[] s = File.ReadAllLines("day12.txt");

            long solution = 0;

            foreach (var item in s)
            {
                string[] temp = item.Split(' ');
                string pattern0 = temp[0];
                string pattern1 = temp[1];
                for (int j = 0; j < 4; j++)
                {
                    temp[0] += "?" + pattern0;
                    temp[1] += "," + pattern1;
                }

                int[] nums = temp[1].Split(',').Select(kk => int.Parse(kk)).ToArray();

                Dictionary<(int, int, int), long> dp = new Dictionary<(int, int, int), long>();

                temp[0] = '.' + temp[0];
                long recursiveForPartTwo(int currIndex, int numsIndex, int leftFromNums)
                {
                    var key = (currIndex, numsIndex, leftFromNums);
                    if (dp.ContainsKey(key))
                        return dp[key];

                    if(currIndex == temp[0].Length)
                        return (numsIndex == nums.Length && leftFromNums == 0) ? 1 : 0;

                    long sum = 0;
                    if((temp[0][currIndex] == '.' || temp[0][currIndex] == '?') && leftFromNums == 0)
                    {
                        if (numsIndex < nums.Length)
                            sum += recursiveForPartTwo(currIndex + 1, numsIndex + 1, nums[numsIndex]);
                        sum += recursiveForPartTwo(currIndex + 1, numsIndex, 0);
                    }
                    if((temp[0][currIndex] == '#' || temp[0][currIndex] == '?') && leftFromNums > 0)
                        sum += recursiveForPartTwo(currIndex + 1, numsIndex, leftFromNums - 1);

                    dp.Add(key, sum);

                    return sum;
                }

                solution += recursiveForPartTwo(0, 0, 0);
            }

            return solution;
        }
    }
}
