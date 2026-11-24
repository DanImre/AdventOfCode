using System.Net.Http.Headers;
using System.Xml.Linq;

namespace AdventOfCode2024
{
    public class Day23 : IDay
    {
        public void PartOne(string inputPath)
        {
            string[][] s = [.. File.ReadAllLines(inputPath).Select(x => x.Split('-'))];

            Dictionary<string, HashSet<string>> nodes = [];

            foreach (var pair in s)
            {
                if (nodes.ContainsKey(pair[0]))
                    nodes[pair[0]].Add(pair[1]);
                else
                    nodes[pair[0]] = [pair[1]];

                if (nodes.ContainsKey(pair[1]))
                    nodes[pair[1]].Add(pair[0]);
                else
                    nodes[pair[1]] = [pair[0]];
            }


            HashSet<(string first, string second, string third)> hs = [];
            foreach (var startNode in nodes.Where(x => x.Key[0] == 't'))
            {
                Queue<(string node, List<string> path)> q = [];
                q.Enqueue((startNode.Key, [startNode.Key]));
                while (q.Count != 0)
                {
                    var curr = q.Dequeue();

                    if (curr.path.Count == 3)
                    {
                        curr.path.Sort();
                        hs.Add((curr.path[0], curr.path[1], curr.path[2]));
                        continue;
                    }

                    foreach (var item in nodes[curr.node].Where(x => !curr.path.Contains(x)))
                        q.Enqueue((item, curr.path.Append(item).ToList()));
                }
            }

            hs = [.. hs.Where(x =>
                nodes[x.first].Contains(x.second) && nodes[x.first].Contains(x.third)
                && nodes[x.second].Contains(x.third))];

            long solution = hs.Count;

            // For the test it should be: 7
            // 998
            Console.WriteLine($"There are {solution} sets of three inter-connected computers where at least one computer with a name that starts with 't'!");
        }

        // Runs for 11.3735033 secs
        public void PartTwo(string inputPath)
        {
            string[][] s = [.. File.ReadAllLines(inputPath).Select(x => x.Split('-'))];

            Dictionary<string, HashSet<string>> nodes = [];
            Dictionary<string, int> degrees = [];

            foreach (var pair in s)
            {
                if (pair[0] == pair[1])
                    continue;

                if (nodes.ContainsKey(pair[0]))
                {
                    nodes[pair[0]].Add(pair[1]);
                    degrees[pair[0]]++;
                }
                else
                {
                    nodes[pair[0]] = [pair[1]];
                    degrees[pair[0]] = 1;
                }

                if (nodes.ContainsKey(pair[1]))
                {
                    nodes[pair[1]].Add(pair[0]);
                    degrees[pair[1]]++;
                }
                else
                {
                    nodes[pair[1]] = [pair[0]];
                    degrees[pair[1]] = 1;
                }
            }

            Dictionary<HashSet<string>, HashSet<string>> memo = new(HashSet<string>.CreateSetComparer());

            HashSet<string> recursiveSolution(HashSet<string> bucket, string curr)
            {
                bucket.Add(curr);
                if(memo.TryGetValue(bucket, out var res))
                {
                    bucket.Remove(curr);
                    return res;
                }

                HashSet<string> solution = bucket.ToHashSet();
                int foundSolutionLength = solution.Count;


                (string node, int degree)[] potentialNodes = nodes[curr]
                    .Where(x => bucket.All(y => nodes[x].Contains(y))) // connect to every node in the bucket
                    .Select(x => (node:x, degree:degrees[x])) // Get their degrees
                    .Where(x => !bucket.Contains(x.node)) // No loops
                    .ToArray();

                foreach (var item in potentialNodes.OrderByDescending(x => x.degree))
                {
                    if (item.degree < foundSolutionLength)
                        break;

                    HashSet<string> currSol = recursiveSolution(bucket, item.node);
                    int currSolLength = currSol.Count;

                    if (foundSolutionLength >= currSolLength)
                        continue;

                    solution = currSol;
                    foundSolutionLength = currSolLength;
                }

                memo.Add(bucket, solution);
                bucket.Remove(curr);
                return solution;
            }

            int foundSolutionLength = 0;
            HashSet<string> solution = [];
            int index = 0;
            foreach (var item in degrees.OrderByDescending(x => x.Value))
            {
                //Console.WriteLine($"Checking: Node: {item.Key} | Degree: {item.Value} | Index: {index++}");
                if (item.Value < foundSolutionLength)
                    break;

                HashSet<string> currSol = recursiveSolution([], item.Key);
                int currSolLength = currSol.Count;

                if (foundSolutionLength >= currSolLength)
                    continue;

                solution = currSol;
                foundSolutionLength = currSolLength;
            }


            // For the test it should be: co,de,ka,ta
            // cc,ff,fh,fr,ny,oa,pl,rg,uj,wd,xn,xs,zw
            Console.WriteLine($"The password to get into the LAN party is '{string.Join(",", solution.Order())}'!");
        }
    }
}
