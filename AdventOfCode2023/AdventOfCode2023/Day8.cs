using System;



using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day8
    {
        public Day8()
        {

        }

        public class Node
        {
            public string Name { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node(string name)
            {
                Left = null!;
                Right = null!;
                Name = name;
            }

            public Node(string name, Node left, Node right)
            {
                Left = left;
                Right = right;
                Name = name;
            }
        }

        public long PartOne() //19199
        {
            //string[] s = File.ReadAllLines("day8test.txt");
            string[] s = File.ReadAllLines("day8.txt");

            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach (var item in s.TakeLast(s.Length - 2).Select(kk => kk.Substring(0,3)))
                nodes.Add(item, new Node(item));

            for (int i = 2; i < s.Length; i++)
            {
                string name = s[i].Substring(0, 3);

                string left = s[i].Substring(7,3);
                string right = s[i].Substring(12,3);

                nodes[name].Left = nodes[left];
                nodes[name].Right = nodes[right];
            }

            long solution = 0;

            Node curr = nodes["AAA"];
            while (curr.Name != "ZZZ")
                if (s[0][(int)(solution++ % s[0].Length)] == 'L')
                    curr = curr.Left;
                else
                    curr = curr.Right;

            return solution;
        }
        public long PartTwo() //13663968099527
        {
            //string[] s = File.ReadAllLines("day8test2.txt");
            string[] s = File.ReadAllLines("day8.txt");

            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            foreach (var item in s.TakeLast(s.Length - 2).Select(kk => kk.Substring(0, 3)))
                nodes.Add(item, new Node(item));

            for (int i = 2; i < s.Length; i++)
            {
                string name = s[i].Substring(0, 3);

                string left = s[i].Substring(7, 3);
                string right = s[i].Substring(12, 3);

                nodes[name].Left = nodes[left];
                nodes[name].Right = nodes[right];
            }

            // Conveniently each start node has a repeating path, ending with a node that ends with a 'Z'
            // Don't know why yet

            Node[] startNodes = nodes.Keys.Where(kk => kk[2] == 'A').Select(kk => nodes[kk]).ToArray();

            List<int> loopLengths = new List<int>();

            foreach (var item in startNodes)
            {
                Node curr = item;
                int steps = 0;

                while (curr.Name[2] != 'Z')
                    if (s[0][steps++ % s[0].Length] == 'L')
                        curr = curr.Left;
                    else
                        curr = curr.Right;

                loopLengths.Add(steps);
            }

            long solution = loopLengths[0];

            for (int i = 1; i < loopLengths.Count; i++)
                solution = solution / EuclidianAlgorithmGCD(solution, loopLengths[i]) * loopLengths[i];

            return solution;
        }

        static long EuclidianAlgorithmGCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }
    }
}
