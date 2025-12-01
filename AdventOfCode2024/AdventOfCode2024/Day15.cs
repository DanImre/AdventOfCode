using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Day15 : IDay
    {
        public string InputPath { get; set; } = "day15Input.txt";
        //public string InputPath { get; set; } = "day15TestInput.txt";
        //public string InputPath { get; set; } = "day15TestInput2.txt";
        //public string InputPath { get; set; } = "day15TestInput3.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            List<List<char>> map = new();
            Queue<char> instructions = new();

            using (StreamReader f = new StreamReader(InputPath))
            {
                while (!f.EndOfStream
                    && f.ReadLine() is string line && line != "")
                    map.Add(line.ToCharArray().ToList());

                while (!f.EndOfStream)
                    foreach (var item in f.ReadLine()!)
                        instructions.Enqueue(item);
            }

            (int y, int x) robotPos = (0, 0);

            for (int i = 0; i < map.Count; i++)
                for (int j = 0; j < map[i].Count; j++)
                    if (map[i][j] == '@')
                    {
                        map[i][j] = '.';
                        robotPos = (i, j);
                        break;
                    }

            while (instructions.Count > 0)
            {
                (int y, int x) step = (0, 0);
                switch (instructions.Dequeue())
                {
                    case '>':
                        step = (0, 1);
                        break;
                    case '<':
                        step = (0, -1);
                        break;
                    case '^':
                        step = (-1, 0);
                        break;
                    case 'v':
                        step = (1, 0);
                        break;
                }


                (int y, int x) next = (robotPos.y + step.y, robotPos.x + step.x);

                // Can't move there
                if (map[next.y][next.x] == '#')
                    continue;

                // Can just simply move there
                if (map[next.y][next.x] == '.')
                {
                    robotPos = next;
                    continue;
                }

                // Move along the boxes
                (int y, int x) boxRider = next;
                while (map[boxRider.y][boxRider.x] == 'O')
                    boxRider = (boxRider.y + step.y, boxRider.x + step.x);

                // Can't push them
                if (map[boxRider.y][boxRider.x] == '#')
                    continue;

                // Push them
                map[boxRider.y][boxRider.x] = 'O';
                map[next.y][next.x] = '.';

                robotPos = next;
            }

            int solution = 0;
            for (int i = 0; i < map.Count; i++)
                for (int j = 0; j < map[i].Count; j++)
                    if (map[i][j] == 'O')
                        solution += 100 * i + j;

            Console.WriteLine($"The sum of all boxes' GPS coordinates is {solution}");
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            List<List<char>> map = new();
            Queue<char> instructions = new();

            using (StreamReader f = new StreamReader(InputPath))
            {
                while (!f.EndOfStream
                    && f.ReadLine() is string line && line != "")
                {
                    List<char> stretchedLine = new List<char>();
                    foreach (var item in line)
                    {
                        if (item == 'O')
                        {
                            stretchedLine.Add('[');
                            stretchedLine.Add(']');
                        }
                        else if (item == '@')
                        {
                            stretchedLine.Add('@');
                            stretchedLine.Add('.');
                        }
                        else
                        {
                            stretchedLine.Add(item);
                            stretchedLine.Add(item);
                        }
                    }
                    map.Add(stretchedLine);
                }

                while (!f.EndOfStream)
                    foreach (var item in f.ReadLine()!)
                        instructions.Enqueue(item);
            }

            (int y, int x) robotPos = (0, 0);

            for (int i = 0; i < map.Count; i++)
                for (int j = 0; j < map[i].Count; j++)
                    if (map[i][j] == '@')
                    {
                        map[i][j] = '.';
                        robotPos = (i, j);
                        break;
                    }

            while (instructions.Count > 0)
            {
                (int y, int x) step = (0, 0);
                bool isUpOrDown = false;
                char instrunction = instructions.Dequeue();
                switch (instrunction)
                {
                    case '>':
                        step = (0, 1);
                        break;
                    case '<':
                        step = (0, -1);
                        break;
                    case '^':
                        isUpOrDown = true;
                        step = (-1, 0);
                        break;
                    case 'v':
                        isUpOrDown = true;
                        step = (1, 0);
                        break;
                }

                (int y, int x) next = (robotPos.y + step.y, robotPos.x + step.x);

                // Can't move there
                if (map[next.y][next.x] == '#')
                    continue;

                // Can just simply move there
                if (map[next.y][next.x] == '.')
                {
                    robotPos = next;
                    continue;
                }

                // Move along the boxes
                (int y, int x) boxRider = next;
                int extraPushingWidthLeft = 0;
                int extraPushingWidthRight = 0;

                bool EndedCuzFoundAWall = false;
                List<(int y, int x, char wallPiece)> wallsThatNeedpushing = new List<(int y, int x, char wallPiece)>();
                HashSet<int> xCoordinates = new() { boxRider.x };

                if (!isUpOrDown)
                {
                    while (map[boxRider.y][boxRider.x] == '['
                        || map[boxRider.y][boxRider.x] == ']')
                        boxRider = (boxRider.y, boxRider.x + step.x);

                    EndedCuzFoundAWall = map[boxRider.y][boxRider.x] == '#';
                }
                else
                {
                    while (true)
                    {

                        foreach (var item in xCoordinates.ToList())
                        {
                            if (map[boxRider.y][item] == ']')
                                xCoordinates.Add(item - 1);
                            else if (map[boxRider.y][item] == '[')
                                xCoordinates.Add(item + 1);
                            else if (map[boxRider.y][item] == '.')
                                xCoordinates.Remove(item);
                        }

                        bool WasBox = false;
                        bool foundWall = false;
                        foreach (var item in xCoordinates)
                        {
                            if (map[boxRider.y][item] == '#')
                            {
                                foundWall = true;
                                break;
                            }
                            if (map[boxRider.y][item] == ']'
                                || map[boxRider.y][item] == '[')
                            {
                                wallsThatNeedpushing.Add((boxRider.y, item, map[boxRider.y][item]));
                                WasBox = true;
                            }
                        }

                        if (foundWall || !WasBox)
                        {
                            EndedCuzFoundAWall = foundWall;
                            break;
                        }

                        boxRider = (boxRider.y + step.y, boxRider.x);
                    }
                }

                // Can't push them
                if (EndedCuzFoundAWall)
                    continue;

                // Push them
                //Right
                if (step == (0, 1))
                    for (int i = boxRider.x; i >= next.x; i--)
                    {
                        map[next.y][i] = map[next.y][i - 1];
                    }
                else if (step == (0, -1))
                    for (int i = boxRider.x; i <= next.x; i++)
                    {
                        map[next.y][i] = map[next.y][i + 1];
                    }
                else
                {
                    if (step == (-1, 0))
                        wallsThatNeedpushing.Sort((a, b) => a.y.CompareTo(b.y));
                    else if (step == (1, 0))
                        wallsThatNeedpushing.Sort((a, b) => b.y.CompareTo(a.y));


                    foreach (var item in wallsThatNeedpushing)
                        map[item.y + step.y][item.x] = item.wallPiece;

                    foreach (var item in wallsThatNeedpushing)
                        if (wallsThatNeedpushing.All(kk => (kk.y, kk.x) != (item.y - step.y, item.x)))
                            map[item.y][item.x] = '.';
                }


                robotPos = next;
            }

            int solution = 0;
            for (int i = 0; i < map.Count; i++)
                for (int j = 0; j < map[i].Count; j++)
                    if (map[i][j] == '[')
                        solution += 100 * i + j;

            Console.WriteLine($"The sum of all boxes' final GPS coordinates is {solution}"); // 1554058
        }
    }
}
