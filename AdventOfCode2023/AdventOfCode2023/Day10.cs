using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode2023
{
    public class Day10
    {
        public Day10()
        {

        }

        public int PartOne() //6867
        {
            //char[][] s = File.ReadAllLines("day10test.txt").Select(kk => kk.ToArray()).ToArray();
            char[][] s = File.ReadAllLines("day10.txt").Select(kk => kk.ToArray()).ToArray();

            (int y, int x) start = (0,0);

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == 'S')
                    {
                        start = (i, j);
                        break;
                    }

            Dictionary<char, List<(int y, int x)>> dic = new Dictionary<char, List<(int y, int x)>>
            {
                { '|', new List<(int y, int x)>() { (-1, 0), (1, 0) }},
                { '-', new List<(int y, int x)>() { (0, -1), (0, 1) }},
                { 'L', new List<(int y, int x)>() { (-1, 0), (0, 1) }},
                { 'J', new List<(int y, int x)>() { (-1, 0), (0, -1) }},
                { '7', new List<(int y, int x)>() { (1, 0), (0, -1) }},
                { 'F', new List<(int y, int x)>() { (1, 0), (0, 1) }},
                { '.', new List<(int y, int x)>() }
            };

            List<(int y, int x)> temp = new List<(int y, int x)>();

            if (start.y > 0 && dic[s[start.y - 1][start.x]].Any(kk => kk.y == 1))
                temp.Add((-1, 0));
            if (start.y < s.Length - 1 && dic[s[start.y + 1][start.x]].Any(kk => kk.y == -1))
                temp.Add((1, 0));
            if (start.x > 0 && dic[s[start.y][start.x - 1]].Any(kk => kk.x == 1))
                temp.Add((0, -1));
            if (start.x < s[0].Length - 1 && dic[s[start.y][start.x + 1]].Any(kk => kk.x == -1))
                temp.Add((0, 1));

            s[start.y][start.x] = dic.Where(kk => temp.All(zz => kk.Value.Contains(zz))).First().Key;

            Queue<((int y, int x) pos, int dist)> q = new Queue<((int y, int x) pos, int dist)>();
            q.Enqueue((start, 0));
            HashSet<(int y, int x)> seen = new HashSet<(int y, int x)>();

            while (q.Count != 0)
            {
                int count = q.Count;
                while (count-- > 0)
                {
                    var curr = q.Dequeue();

                    if (!seen.Add(curr.pos))
                        return curr.dist;

                    foreach (var item in dic[s[curr.pos.y][curr.pos.x]])
                    {
                        var newPos = (curr.pos.y + item.y, curr.pos.x + item.x);
                        if (!seen.Contains(newPos))
                            q.Enqueue((newPos, curr.dist + 1));
                    }
                }
            }

            return 0;
        }
        public int PartTwo() //858 too high
        {
            //char[][] s = File.ReadAllLines("day10test2.txt").Select(kk => kk.ToArray()).ToArray();
            char[][] s = File.ReadAllLines("day10.txt").Select(kk => kk.ToArray()).ToArray();

            (int y, int x) start = (0, 0);

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (s[i][j] == 'S')
                    {
                        start = (i, j);
                        break;
                    }

            Dictionary<char, List<(int y, int x)>> dic = new Dictionary<char, List<(int y, int x)>>
            {
                { '|', new List<(int y, int x)>() { (-1, 0), (1, 0) }},
                { '-', new List<(int y, int x)>() { (0, -1), (0, 1) }},
                { 'L', new List<(int y, int x)>() { (-1, 0), (0, 1) }},
                { 'J', new List<(int y, int x)>() { (-1, 0), (0, -1) }},
                { '7', new List<(int y, int x)>() { (1, 0), (0, -1) }},
                { 'F', new List<(int y, int x)>() { (1, 0), (0, 1) }},
                { '.', new List<(int y, int x)>() }
            };

            List<(int y, int x)> temp = new List<(int y, int x)>();

            if (start.y > 0 && dic[s[start.y - 1][start.x]].Any(kk => kk.y == 1))
                temp.Add((-1, 0));
            if (start.y < s.Length - 1 && dic[s[start.y + 1][start.x]].Any(kk => kk.y == -1))
                temp.Add((1, 0));
            if (start.x > 0 && dic[s[start.y][start.x - 1]].Any(kk => kk.x == 1))
                temp.Add((0, -1));
            if (start.x < s[0].Length - 1 && dic[s[start.y][start.x + 1]].Any(kk => kk.x == -1))
                temp.Add((0, 1));

            s[start.y][start.x] = dic.Where(kk => temp.All(zz => kk.Value.Contains(zz))).First().Key;

            Queue<(int y, int x)> q = new Queue<(int y, int x)>();
            q.Enqueue(start);
            HashSet<(int y, int x)> seen = new HashSet<(int y, int x)>();

            while (q.Count != 0)
            {
                int count = q.Count;
                while (count-- > 0)
                {
                    var curr = q.Dequeue();

                    if (!seen.Add(curr))
                        break;

                    foreach (var item in dic[s[curr.y][curr.x]])
                    {
                        var newPos = (curr.y + item.y, curr.x + item.x);
                        if (!seen.Contains(newPos))
                            q.Enqueue(newPos);
                    }
                }
            }

            for (int i = 0; i < s.Length; i++)
                for (int j = 0; j < s[i].Length; j++)
                    if (!seen.Contains((i, j)))
                        s[i][j] = '.';

            for (int i = 0; i < s.Length; i++)
            {
                bool inside = false;

                bool? ridingAndFacingUp = null;
                for (int j = 0; j < s[i].Length; j++)
                {
                    if (s[i][j] == '|')
                        inside = !inside;
                    else if (s[i][j] == 'L' || s[i][j] == 'F')
                        ridingAndFacingUp = s[i][j] == 'L';
                    else if (ridingAndFacingUp.HasValue && (s[i][j] == '7' || s[i][j] == 'J'))
                    {
                        if ((ridingAndFacingUp.Value && s[i][j] == '7') || (!ridingAndFacingUp.Value && s[i][j] == 'J'))
                            inside = !inside;

                        ridingAndFacingUp = null;
                    }

                    if (!inside)
                        seen.Add((i, j));
                }
            }

            return s.Length * s[0].Length - seen.Count;
        }
    }
}
