using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day2
    {
        public Day2()
        {

        }

        public int PartOne() //2156
        {
            string[] s = File.ReadAllLines("day2.txt");

            int solution = 0;

            foreach (var item in s)
            {
                string[] temp = item.Split(':', StringSplitOptions.RemoveEmptyEntries);

                int ID = int.Parse(temp[0].Split(' ')[1]);

                bool possible = true;
                foreach (var i in temp[1].Split(';', StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] temp2 = i.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < temp2.Length && possible; j += 2)
                    {
                        switch (temp2[j + 1])
                        {
                            case "red":
                                if (int.Parse(temp2[j]) > 12)
                                    possible = false;
                                break;
                            case "green":
                                if (int.Parse(temp2[j]) > 13)
                                    possible = false;
                                break;
                            case "blue":
                                if (int.Parse(temp2[j]) > 14)
                                    possible = false;
                                break;
                            default:
                                break;
                        }
                        if (!possible)
                            break;
                    }
                    if (!possible)
                        break;
                }

                if (possible)
                    solution += ID;
            }

            return solution;
        }

        public int PartTwo()
        {
            string[] s = File.ReadAllLines("day2.txt");

            int solution = 0;

            foreach (var item in s)
            {
                string[] temp = item.Split(":", StringSplitOptions.RemoveEmptyEntries);

                int[] redgreenblue = new int[3]; //for max
                foreach (var i in temp[1].Split(';', StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] temp2 = i.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < temp2.Length; j += 2)
                    {
                        switch (temp2[j + 1])
                        {
                            case "red":
                                redgreenblue[0] = Math.Max(redgreenblue[0], int.Parse(temp2[j]));
                                break;
                            case "green":
                                redgreenblue[1] = Math.Max(redgreenblue[1], int.Parse(temp2[j]));
                                break;
                            case "blue":
                                redgreenblue[2] = Math.Max(redgreenblue[2], int.Parse(temp2[j]));
                                break;
                            default:
                                break;
                        }
                    }
                }

                solution += redgreenblue[0] * redgreenblue[1] * redgreenblue[2];
            }

            return solution;
        }
    }
}
