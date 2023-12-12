using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day05
    {
        public Day05()
        {

        }

        public long PartOne() //403695602
        {
            //string[] s = File.ReadAllLines("day5test.txt");
            string[] s = File.ReadAllLines("day5.txt");
            List<long> map = s[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(kk => long.Parse(kk)).ToList();

            for (int index = 3; index < s.Length; index += 2)
            {
                List<long> newMap = new List<long>();

                while (index < s.Length && s[index] != "")
                {
                    long[] temp = s[index].Split(' ').Select(kk => long.Parse(kk)).ToArray();

                    for (int i = 0; i < map.Count; i++)
                        if (temp[1] <= map[i] && map[i] < temp[1] + temp[2])
                        {
                            newMap.Add(temp[0] + (map[i] - temp[1]));
                            map.RemoveAt(i--);
                        }

                    ++index;
                }

                map = newMap;
            }

            return map.Min();
        }
        public long PartTwo() //96143715 too low
        {
            //string[] s = File.ReadAllLines("day5test.txt");
            string[] s = File.ReadAllLines("day5.txt");
            long[] temp = s[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(kk => long.Parse(kk)).ToArray();

            List<(long start, long end)> seeds = new List<(long start, long end)>();

            for (int i = 1; i < temp.Length; i+=2)
                seeds.Add((temp[i - 1], temp[i - 1] + temp[i]));

            for (int index = 3; index < s.Length; index += 2)
            {
                List<(long start, long end)> newMap = new List<(long start, long end)>();

                while (index < s.Length && s[index] != "")
                {
                    long[] dsr = s[index].Split(' ').Select(kk => long.Parse(kk)).ToArray();

                    for (int i = 0; i < seeds.Count; i++)
                    {
                        if (seeds[i].start >= dsr[1] && seeds[i].end < dsr[1] + dsr[2]) //teljesen benne
                        {
                            newMap.Add((dsr[0] + (seeds[i].start - dsr[1]), dsr[0] + (seeds[i].end - dsr[1])));
                            seeds.RemoveAt(i--);
                        }
                        else if(seeds[i].start < dsr[1] && dsr[1] <= seeds[i].end && seeds[i].end < dsr[1] + dsr[2]) //start kivul end bent
                        {
                            newMap.Add((dsr[0], dsr[0] + (seeds[i].end - dsr[1])));
                            seeds[i] = (seeds[i].start, dsr[1] - 1);
                        }
                        else if (dsr[1] <= seeds[i].start && seeds[i].start < dsr[1] + dsr[2] && dsr[1] + dsr[2] <= seeds[i].end) //start belul end kivul
                        {
                            newMap.Add((dsr[0] + (seeds[i].start - dsr[1]), dsr[0] + dsr[2] - 1));
                            seeds[i] = (dsr[1] + dsr[2], seeds[i].end);
                        }
                        else if (seeds[i].start < dsr[1] && dsr[1] + dsr[2] <= seeds[i].end) //start es end kivul
                        {
                            seeds[i] = (seeds[i].start, dsr[1] - 1);
                            seeds.Add((dsr[1], seeds[i].end));
                            --i;
                        }
                    }

                    ++index;
                }


                foreach (var item in newMap)
                    seeds.Add(item);
            }

            return seeds.Min(kk => kk.start);
        }

        public long Arpi()
        {
            string[] import = File.ReadAllLines("day5.txt");
            List<long> seeds = new List<long>(import[0].Split(':')[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(n => long.Parse(n)));

            List<Func<long, long>> map = new List<Func<long, long>>();
            
            for (int i = 3; i < import.Length; i += 2)
            {
                while (i < import.Length && import[i] != "")
                {
                    long[] line = import[i].Split(' ').Select(kk => long.Parse(kk)).ToArray();
                    //Func<long, long> func = x => x < line[1] || x >= line[1] + line[2] ? x : line[0] + (x - line[1]);
                    map.Add(x => (x >= line[1] && x < line[1] + line[2]) ? line[0] + (x - line[1]) : x);
                    i++;
                }

                for (int k = 0; k < seeds.Count; k++)
                    foreach (var item in map)
                    {
                        long newValue = item(seeds[k]);

                        if (newValue == seeds[k])
                            continue;

                        seeds[k] = newValue;
                        break;
                    }
                
                map.Clear();
            }
            Console.WriteLine($"Az első feladat megoldása: {seeds.Min()}");
            return seeds.Min();
        }
    }
}
