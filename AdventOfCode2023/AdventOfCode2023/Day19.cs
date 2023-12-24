using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2023
{
    public class Day19
    {
        public Day19()
        {

        }

        public int PartOne()
        {
            //string[] s = File.ReadAllLines("day19test.txt");
            string[] s = File.ReadAllLines("day19.txt");

            Dictionary<string, List<Func<(int x, int m, int a, int s), string?>>> dic = new Dictionary<string, List<Func<(int x, int m, int a, int s), string?>>>();

            int index = -1;
            while (s[++index] != "")
            {
                string[] temp = s[index].Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

                string key = temp[0];
                List<Func<(int x, int m, int a, int s), string?>> list = new List<Func<(int x, int m, int a, int s), string?>>();
                temp = temp[1].Split(',');
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    string[] temp2 = temp[i].Split(':');
                    string dest = temp2[1];
                    switch (temp2[0][0])
                    {
                        case 'a':
                            if (temp2[0][1] == '<')
                                list.Add(input => input.a < int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) ? dest : null);
                            else
                                list.Add(input => input.a > int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) ? dest : null);
                            break;
                        case 'm':
                            if (temp2[0][1] == '<')
                                list.Add(input => input.m < int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) ? dest : null);
                            else
                                list.Add(input => input.m > int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) ? dest : null);
                            break;
                        case 'x':
                            if (temp2[0][1] == '<')
                                list.Add(input => input.x < int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) ? dest : null);
                            else
                                list.Add(input => input.x > int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) ? dest : null);
                            break;
                        case 's':
                            if (temp2[0][1] == '<')
                                list.Add(input => input.s < int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) ? dest : null);
                            else
                                list.Add(input => input.s > int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) ? dest : null);
                            break;
                        default:
                            break;
                    }

                }
                list.Add(a => temp[temp.Length - 1]);

                dic.Add(key, list);
            }

            List<(int x, int m, int a, int s)> parts = new List<(int x, int m, int a, int s)>();
            while (++index < s.Length)
            {
                int[] temp = s[index].Split(s[index].Where(kk => !char.IsDigit(kk)).Distinct().ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(kk => int.Parse(kk)).ToArray();
                parts.Add((temp[0], temp[1], temp[2], temp[3]));
            }

            int solution = 0;

            foreach (var item in parts)
            {
                string? currSpot = "in";
                while (currSpot != "A" && currSpot != "R" && currSpot != null)
                {
                    var list = dic[currSpot];
                    currSpot = null;
                    for (int i = 0; i < list.Count && currSpot == null; i++)
                        currSpot = list[i](item);
                }

                if (currSpot == "A")
                    solution += item.x + item.a + item.m + item.s;
            }

            return solution;
        }

        public long PartTwo()
        {
            //string[] s = File.ReadAllLines("day19test.txt");
            string[] s = File.ReadAllLines("day19.txt");

            //Dictionary<string, List<Func<((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s), (((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s) ranges, string next)>>> dic = new Dictionary<string, List<Func<((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s), (((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s) ranges, string next)>>>();
            Dictionary<string, List<(string template, string next)>> dic = new Dictionary<string, List<(string template, string next)>>();

            foreach (var item in s)
            {
                if (item == "")
                    break;

                string[] temp = item.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

                string key = temp[0];
                temp = temp[1].Split(',');

                var list = new List<(string template, string next)>();

                for (int i = 0; i < temp.Length - 1; i++)
                {
                    string[] temp2 = temp[i].Split(':');

                    list.Add((temp2[0], temp2[1]));
                }
                list.Add(("", temp[temp.Length - 1]));
                dic.Add(key, list);
            }

            long recursiveCount(((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s) ranges, string name)
            {
                if (name == "R")
                    return 0;
                if (name == "A")
                    return (long)(ranges.x.max - ranges.x.min + 1) * (long)(ranges.m.max - ranges.m.min + 1) * (long)(ranges.a.max - ranges.a.min + 1) * (long)(ranges.s.max - ranges.s.min + 1);

                long sum = 0;

                foreach (var item in dic[name])
                {
                    if (item.template == "")
                        continue;

                    int value = int.Parse(item.template.Substring(2));
                    var next = ranges;
                    switch (item.template[0])
                    {
                        case 'x':
                            if (item.template[1] == '>')
                            {
                                next.x.min = Math.Max(next.x.min, value + 1);
                                ranges.x.max = Math.Min(ranges.x.max, value);
                            }
                            else
                            {
                                next.x.max = Math.Min(next.x.max, value - 1);
                                ranges.x.min = Math.Max(ranges.x.min, value);
                            }
                            break;
                        case 'm':
                            if (item.template[1] == '>')
                            {
                                next.m.min = Math.Max(next.m.min, value + 1);
                                ranges.m.max = Math.Min(ranges.m.max, value);
                            }
                            else
                            {
                                next.m.max = Math.Min(next.m.max, value - 1);
                                ranges.m.min = Math.Max(ranges.m.min, value);
                            }
                            break;
                        case 'a':
                            if (item.template[1] == '>')
                            {
                                next.a.min = Math.Max(next.a.min, value + 1);
                                ranges.a.max = Math.Min(ranges.a.max, value);
                            }
                            else
                            {
                                next.a.max = Math.Min(next.a.max, value - 1);
                                ranges.a.min = Math.Max(ranges.a.min, value);
                            }
                            break;
                        case 's':
                            if (item.template[1] == '>')
                            {
                                next.s.min = Math.Max(next.s.min, value + 1);
                                ranges.s.max = Math.Min(ranges.s.max, value);
                            }
                            else
                            {
                                next.s.max = Math.Min(next.s.max, value - 1);
                                ranges.s.min = Math.Max(ranges.s.min, value);
                            }
                            break;
                        default:
                            break;
                    }

                    sum += recursiveCount(next, item.next);
                }

                return sum + recursiveCount(ranges, dic[name].Last().next);
            }


            return recursiveCount(((1, 4000), (1, 4000), (1, 4000), (1, 4000)), "in");
        }
    }
}
