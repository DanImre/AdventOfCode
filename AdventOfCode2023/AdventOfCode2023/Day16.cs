using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day16
    {
        public Day16()
        {

        }

        public int PartOne() // 7632
        {
            //string[] s = File.ReadAllLines("day16test.txt");
            string[] s = File.ReadAllLines("day16.txt");

            HashSet<(int y, int x)> visited = new HashSet<(int y, int x)>();
            HashSet<((int y, int x), int direction)> visitedWithoutLoops = new HashSet<((int y, int x), int direction)>();

            Queue<((int y, int x) pos, int direction)> q = new Queue<((int y, int x) pos, int direction)>();
            q.Enqueue(((0, 0), 0));

            while (q.Count != 0)
            {
                var curr = q.Dequeue();

                if (curr.pos.x < 0 || curr.pos.y < 0 || curr.pos.y == s.Length || curr.pos.x == s[curr.pos.y].Length)
                    continue;

                visited.Add(curr.pos);
                if (!visitedWithoutLoops.Add(curr))
                    continue;

                if (s[curr.pos.y][curr.pos.x] == '|')//|
                    switch (curr.direction)
                    {
                        case 0:
                            q.Enqueue((curr.pos, 1));
                            q.Enqueue((curr.pos, 3));
                            break;
                        case 1:
                            q.Enqueue(((curr.pos.y + 1, curr.pos.x),1));
                            break;
                        case 2:
                            q.Enqueue((curr.pos, 1));
                            q.Enqueue((curr.pos, 3));
                            break;
                        default: //3
                            q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                            break;
                    }
                else if (s[curr.pos.y][curr.pos.x] == '-')//|
                    switch (curr.direction)
                    {
                        case 0:
                            q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                            break;
                        case 1:
                            q.Enqueue((curr.pos, 0));
                            q.Enqueue((curr.pos, 2));
                            break;
                        case 2:
                            q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                            break;
                        default: //3
                            q.Enqueue((curr.pos, 0));
                            q.Enqueue((curr.pos, 2));
                            break;
                    }
                else if (s[curr.pos.y][curr.pos.x] == '/')//|
                    switch (curr.direction)
                    {
                        case 0:
                            q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                            break;
                        case 1:
                            q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                            break;
                        case 2:
                            q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                            break;
                        default: //3
                            q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                            break;
                    }
                else if(s[curr.pos.y][curr.pos.x] == '.')//(s[curr.pos.x][curr.pos.y] == '\\')//\
                    switch (curr.direction)
                    {
                        case 0:
                            q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                            break;
                        case 1:
                            q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                            break;
                        case 2:
                            q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                            break;
                        default: //3
                            q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                            break;
                    }
                else // '\'
                    switch (curr.direction)
                    {
                        case 0:
                            q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                            break;
                        case 1:
                            q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                            break;
                        case 2:
                            q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                            break;
                        default: //3
                            q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                            break;
                    }
            }

            return visited.Count;
        }
        public int PartTwo() // 8023
        {
            //string[] s = File.ReadAllLines("day16test.txt");
            string[] s = File.ReadAllLines("day16.txt");

            HashSet<(int y, int x)> visited = new HashSet<(int y, int x)>();
            HashSet<((int y, int x), int direction)> visitedWithoutLoops = new HashSet<((int y, int x), int direction)>();

            Queue<((int y, int x) pos, int direction)> q = new Queue<((int y, int x) pos, int direction)>();

            int solution = 0;

            for (int i = -1; i <= s.Length; i++)
            {
                //left side
                if (i == -1) //top left
                    q.Enqueue(((0, 0), 1));
                else if(i == s.Length) // bottom left
                    q.Enqueue(((s.Length - 1, 0), 3));
                else
                    q.Enqueue(((i, 0), 0));

                while (q.Count != 0)
                {
                    var curr = q.Dequeue();

                    if (curr.pos.x < 0 || curr.pos.y < 0 || curr.pos.y == s.Length || curr.pos.x == s[curr.pos.y].Length)
                        continue;

                    visited.Add(curr.pos);
                    if (!visitedWithoutLoops.Add(curr))
                        continue;

                    if (s[curr.pos.y][curr.pos.x] == '|')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue((curr.pos, 1));
                                q.Enqueue((curr.pos, 3));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 2:
                                q.Enqueue((curr.pos, 1));
                                q.Enqueue((curr.pos, 3));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '-')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 1:
                                q.Enqueue((curr.pos, 0));
                                q.Enqueue((curr.pos, 2));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            default: //3
                                q.Enqueue((curr.pos, 0));
                                q.Enqueue((curr.pos, 2));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '/')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '.')//(s[curr.pos.x][curr.pos.y] == '\\')//\
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                        }
                    else // '\'
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                        }
                }

                solution = Math.Max(solution, visited.Count);

                visitedWithoutLoops.Clear();
                visited.Clear();

                //Right side
                if (i == -1) //top left
                    q.Enqueue(((0, s[0].Length - 1), 1));
                else if (i == s.Length) // bottom left
                    q.Enqueue(((s.Length - 1, s[0].Length - 1), 3));
                else
                    q.Enqueue(((i, s[i].Length - 1), 2));

                while (q.Count != 0)
                {
                    var curr = q.Dequeue();

                    if (curr.pos.x < 0 || curr.pos.y < 0 || curr.pos.y == s.Length || curr.pos.x == s[curr.pos.y].Length)
                        continue;

                    visited.Add(curr.pos);
                    if (!visitedWithoutLoops.Add(curr))
                        continue;

                    if (s[curr.pos.y][curr.pos.x] == '|')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue((curr.pos, 1));
                                q.Enqueue((curr.pos, 3));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 2:
                                q.Enqueue((curr.pos, 1));
                                q.Enqueue((curr.pos, 3));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '-')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 1:
                                q.Enqueue((curr.pos, 0));
                                q.Enqueue((curr.pos, 2));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            default: //3
                                q.Enqueue((curr.pos, 0));
                                q.Enqueue((curr.pos, 2));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '/')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '.')//(s[curr.pos.x][curr.pos.y] == '\\')//\
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                        }
                    else // '\'
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                        }
                }

                solution = Math.Max(solution, visited.Count);

                visitedWithoutLoops.Clear();
                visited.Clear();
            }

            for (int i = 1; i < s[0].Length - 1; i++)
            {
                //top
                q.Enqueue(((0, i), 1));

                while (q.Count != 0)
                {
                    var curr = q.Dequeue();

                    if (curr.pos.x < 0 || curr.pos.y < 0 || curr.pos.y == s.Length || curr.pos.x == s[curr.pos.y].Length)
                        continue;

                    visited.Add(curr.pos);
                    if (!visitedWithoutLoops.Add(curr))
                        continue;

                    if (s[curr.pos.y][curr.pos.x] == '|')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue((curr.pos, 1));
                                q.Enqueue((curr.pos, 3));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 2:
                                q.Enqueue((curr.pos, 1));
                                q.Enqueue((curr.pos, 3));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '-')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 1:
                                q.Enqueue((curr.pos, 0));
                                q.Enqueue((curr.pos, 2));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            default: //3
                                q.Enqueue((curr.pos, 0));
                                q.Enqueue((curr.pos, 2));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '/')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '.')//(s[curr.pos.x][curr.pos.y] == '\\')//\
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                        }
                    else // '\'
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                        }
                }

                solution = Math.Max(solution, visited.Count);

                visitedWithoutLoops.Clear();
                visited.Clear();

                //bottom

                q.Enqueue(((s.Length - 1, i), 3));

                while (q.Count != 0)
                {
                    var curr = q.Dequeue();

                    if (curr.pos.x < 0 || curr.pos.y < 0 || curr.pos.y == s.Length || curr.pos.x == s[curr.pos.y].Length)
                        continue;

                    visited.Add(curr.pos);
                    if (!visitedWithoutLoops.Add(curr))
                        continue;

                    if (s[curr.pos.y][curr.pos.x] == '|')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue((curr.pos, 1));
                                q.Enqueue((curr.pos, 3));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 2:
                                q.Enqueue((curr.pos, 1));
                                q.Enqueue((curr.pos, 3));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '-')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 1:
                                q.Enqueue((curr.pos, 0));
                                q.Enqueue((curr.pos, 2));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            default: //3
                                q.Enqueue((curr.pos, 0));
                                q.Enqueue((curr.pos, 2));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '/')//|
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                        }
                    else if (s[curr.pos.y][curr.pos.x] == '.')//(s[curr.pos.x][curr.pos.y] == '\\')//\
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                        }
                    else // '\'
                        switch (curr.direction)
                        {
                            case 0:
                                q.Enqueue(((curr.pos.y + 1, curr.pos.x), 1));
                                break;
                            case 1:
                                q.Enqueue(((curr.pos.y, curr.pos.x + 1), 0));
                                break;
                            case 2:
                                q.Enqueue(((curr.pos.y - 1, curr.pos.x), 3));
                                break;
                            default: //3
                                q.Enqueue(((curr.pos.y, curr.pos.x - 1), 2));
                                break;
                        }
                }

                solution = Math.Max(solution, visited.Count);

                visitedWithoutLoops.Clear();
                visited.Clear();
            }


            return solution;
        }
    }
}
