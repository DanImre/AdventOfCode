using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day06 : IDay
    {
        private enum Action
        {
            ON,
            OFF,
            TOGGLE
        }

        public void PartOne(string inputPath)
        {
            List<(Action action, int[] firstCoords, int[] secondCoords)> actions = File.ReadAllLines(inputPath)
                .Select(static x =>
                {
                    List<string> words = [.. x.Split(" ", StringSplitOptions.RemoveEmptyEntries)];

                    Action action = Action.TOGGLE;
                    if (words[0] == "toggle")
                        words.RemoveAt(0);
                    else
                    {
                        action = words[1] == "on" ? Action.ON : Action.OFF;
                        words.RemoveRange(0, 2);
                    }

                    words.RemoveAt(1); // removing through

                    int[] firstCoords = words[0].Split(",").Select(int.Parse).ToArray();
                    int[] secondCoords = words[1].Split(",").Select(int.Parse).ToArray();
                    return (action, firstCoords, secondCoords);
                })
                .ToList();

            bool[][] grid = new bool[1000][];
            for (int i = 0; i < 1000; i++)
                grid[i] = new bool[1000];

            foreach (var item in actions)
            {
                for (int y = item.firstCoords[0]; y <= item.secondCoords[0]; y++)
                    for (int x = item.firstCoords[1]; x <= item.secondCoords[1]; x++)
                        switch (item.action)
                        {
                            case Action.ON:
                                grid[y][x] = true;
                                break;
                            case Action.OFF:
                                grid[y][x] = false;
                                break;
                            case Action.TOGGLE:
                                grid[y][x] = !grid[y][x];
                                break;
                        }


            }

            int solution = grid.Sum(x => x.Count(y => y));

            Console.WriteLine($"There are {solution} nice strings.");
        }

        public void PartTwo(string inputPath)
        {
            List<(Action action, int[] firstCoords, int[] secondCoords)> actions = File.ReadAllLines(inputPath)
                .Select(static x =>
                {
                    List<string> words = [.. x.Split(" ", StringSplitOptions.RemoveEmptyEntries)];

                    Action action = Action.TOGGLE;
                    if (words[0] == "toggle")
                        words.RemoveAt(0);
                    else
                    {
                        action = words[1] == "on" ? Action.ON : Action.OFF;
                        words.RemoveRange(0, 2);
                    }

                    words.RemoveAt(1); // removing through

                    int[] firstCoords = words[0].Split(",").Select(int.Parse).ToArray();
                    int[] secondCoords = words[1].Split(",").Select(int.Parse).ToArray();
                    return (action, firstCoords, secondCoords);
                })
                .ToList();

            int[][] grid = new int[1000][];
            for (int i = 0; i < 1000; i++)
                grid[i] = new int[1000];

            foreach (var item in actions)
            {
                for (int y = item.firstCoords[0]; y <= item.secondCoords[0]; y++)
                    for (int x = item.firstCoords[1]; x <= item.secondCoords[1]; x++)
                        switch (item.action)
                        {
                            case Action.ON:
                                grid[y][x] += 1;
                                break;
                            case Action.OFF:
                                grid[y][x] = Math.Max(grid[y][x] - 1, 0);
                                break;
                            case Action.TOGGLE:
                                grid[y][x] += 2;
                                break;
                        }


            }

            int solution = grid.Sum(x => x.Sum());

            Console.WriteLine($"There are {solution} nice strings.");
        }
    }
}
