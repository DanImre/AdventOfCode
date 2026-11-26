using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace AdventOfCode2015
{
    public class Day13 : IDay
    {
        // Idea: this is a graph and we need to create the loop with the most cost.
        // The cost from going from 1 node to an other is the sum of the directed edges between them.
        // Creating a route that consists of every node is a loop because this is a fully connected graph,
        // for the cost to be right we need to add the first and last node connection cost to the final sum.
        // The start node is irrelevant, because the loop needs to contain every node.
        public void PartOne(string inputPath)
        {
            Dictionary<string, List<(string node, int cost)>> graph = File.ReadLines(inputPath)
                .Select(x =>
                {
                    string[] wouldSplit = x.Split(" would ");
                    string[] spaceSplit = wouldSplit[1].Split([' ', '.'], StringSplitOptions.RemoveEmptyEntries);

                    int gainOrLoseMult = spaceSplit[0] == "gain" ? 1 : -1;
                    int amount = gainOrLoseMult * int.Parse(spaceSplit[1]);
                    string destNode = spaceSplit[^1];

                    return (Key: wouldSplit[0], Value: (node: destNode, cost: amount));
                })
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Value).ToList());


            Dictionary<(string currNode, string set), int> memo = [];

            string startNode = graph.Keys.First();
            HashSet<string> visited = [];

            int DFS(string currNode)
            {
                if (!visited.Add(currNode))
                    return int.MinValue / 4;

                // This is the end of the loop
                if(visited.Count == graph.Keys.Count)
                {
                    int closing = graph[currNode].First(x => x.node == startNode).cost
                        + graph[startNode].First(x => x.node == currNode).cost;
                    visited.Remove(currNode);
                    return closing;
                }

                int bestSol = int.MinValue / 4;
                foreach (var item in graph[currNode])
                    bestSol = Math.Max(bestSol, item.cost + graph[item.node].First(x => x.node == currNode).cost + DFS(item.node));

                visited.Remove(currNode);

                return bestSol;
            }

            int solution = DFS(startNode);

            // The test should be 330
            // 441 too low
            // 311 too low
            Console.WriteLine($"The sum of all numbers is: {solution}.");
        }

        public void PartTwo(string inputPath)
        {
            Dictionary<string, List<(string node, int cost)>> graph = File.ReadLines(inputPath)
                .Select(x =>
                {
                    string[] wouldSplit = x.Split(" would ");
                    string[] spaceSplit = wouldSplit[1].Split([' ', '.'], StringSplitOptions.RemoveEmptyEntries);

                    int gainOrLoseMult = spaceSplit[0] == "gain" ? 1 : -1;
                    int amount = gainOrLoseMult * int.Parse(spaceSplit[1]);
                    string destNode = spaceSplit[^1];

                    return (Key: wouldSplit[0], Value: (node: destNode, cost: amount));
                })
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key, x => x.Select(y => y.Value).ToList());


            foreach (var item in graph.Values)
                item.Add(("me", 0));

            graph.Add("me", graph.Keys
                .Select(x => (x,0))
                .ToList());

            Dictionary<(string currNode, string set), int> memo = [];

            string startNode = graph.Keys.First();
            HashSet<string> visited = [];

            int DFS(string currNode)
            {
                if (!visited.Add(currNode))
                    return int.MinValue / 4;

                // This is the end of the loop
                if (visited.Count == graph.Keys.Count)
                {
                    int closing = graph[currNode].First(x => x.node == startNode).cost
                        + graph[startNode].First(x => x.node == currNode).cost;
                    visited.Remove(currNode);
                    return closing;
                }

                int bestSol = int.MinValue / 4;
                foreach (var item in graph[currNode])
                    bestSol = Math.Max(bestSol, item.cost + graph[item.node].First(x => x.node == currNode).cost + DFS(item.node));

                visited.Remove(currNode);

                return bestSol;
            }

            int solution = DFS(startNode);

            // The test should be 330
            // 441 too low
            // 311 too low
            Console.WriteLine($"The sum of all numbers is: {solution}.");
        }
    }
}
