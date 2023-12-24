using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string[] s = File.ReadAllLines("day19test.txt");
            //string[] s = File.ReadAllLines("day19.txt");

            Dictionary<string, List<Func<((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s), (((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s) ranges, string next)>>> dic = new Dictionary<string, List<Func<((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s), (((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s) ranges, string next)>>>();

            int index = -1;
            while (s[++index] != "")
            {
                string[] temp = s[index].Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

                string key = temp[0];
                List<Func<((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s), (((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s) ranges, string next)>> list = new List<Func<((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s), (((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s) ranges, string next)>>();
                temp = temp[1].Split(',');
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    string[] temp2 = temp[i].Split(':');
                    string dest = temp2[1];
                    switch (temp2[0][0])
                    {
                        case 'a':
                            if (temp2[0][1] == '<')
                                list.Add(input => ((input.x, input.m, (input.a.min, Math.Min(input.a.max, int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) - 1)), input.s), dest));
                            else
                                list.Add(input => ((input.x, input.m, (Math.Max(input.a.min, int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) + 1), input.a.max), input.s), dest));
                            break;
                        case 'm':
                            if (temp2[0][1] == '<')
                                list.Add(input => ((input.x, (input.m.min, Math.Min(input.m.max, int.Parse(temp2[0].Substring(2, temp2[0].Length - 2))) - 1), input.a, input.s), dest));
                            else
                                list.Add(input => ((input.x, (Math.Max(input.m.min, int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) + 1), input.m.max), input.a, input.s), dest));
                            break;
                        case 'x':
                            if (temp2[0][1] == '<')
                                list.Add(input => (((input.x.min, Math.Min(input.x.max, int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) - 1)), input.m, input.a, input.s), dest));
                            else
                                list.Add(input => (((Math.Max(input.x.min, int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) + 1), input.x.max), input.m, input.a, input.s), dest));
                            break;
                        case 's':
                            if (temp2[0][1] == '<')
                                list.Add(input => ((input.x, input.m, input.a, (input.s.min, Math.Min(input.s.max, int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) - 1))), dest));
                            else
                                list.Add(input => ((input.x, input.m, input.a, (Math.Max(input.s.min, int.Parse(temp2[0].Substring(2, temp2[0].Length - 2)) + 1), input.s.max)), dest));
                            break;
                        default:
                            break;
                    }

                }
                list.Add(a => (a, temp[temp.Length - 1]));

                dic.Add(key, list);
            }

            Queue<(((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s) ranges, string next)> q = new Queue<(((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s) ranges, string next)>();
            q.Enqueue((((1, 4000), (1, 4000), (1, 4000), (1, 4000)), "in"));

            List<((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s)> acceptedRanges = new List<((int min, int max) x, (int min, int max) m, (int min, int max) a, (int min, int max) s)>();

            while (q.Count > 0)
            {
                var curr = q.Dequeue();

                foreach (var item in dic[curr.next])
                {
                    var temp = item(curr.ranges);

                    if (temp.ranges.x.min > temp.ranges.x.max
                        || temp.ranges.m.min > temp.ranges.m.max
                        || temp.ranges.a.min > temp.ranges.a.max
                        || temp.ranges.s.min > temp.ranges.s.max
                        || temp.next == "R")
                        continue;

                    if (temp.next == "A")
                    {
                        acceptedRanges.Add(temp.ranges);
                        continue;
                    }

                    q.Enqueue((temp.ranges, temp.next));
                }
            }

            //az utsó lehetőségnél az előttiek negáltja kell legyen!!!

            foreach (var item in acceptedRanges)
            {
                Console.WriteLine($"({item.x.min},{item.x.max})\t({item.m.min},{item.m.max})\t({item.a.min},{item.a.max})\t({item.s.min},{item.s.max})");
            }

            return 0;
        }
    }
}
