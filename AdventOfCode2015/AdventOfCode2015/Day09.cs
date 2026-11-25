using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day09 : IDay
    {
        public void PartOne(string inputPath)
        {
            (string from, string to, int cost)[] s = File.ReadAllLines(inputPath)
                .Select(x =>
                {
                    var eqSplit = x.Split(" = ");
                    var leftSplit = eqSplit[0].Split(" to ");
                    return (leftSplit[0], leftSplit[1], int.Parse(eqSplit[1]));
                })
                .ToArray();

            Dictionary<string, List<(string destination, int cost)>> graph = [];
            foreach (var item in s)
            {
                graph.TryAdd(item.from, []);
                graph.TryAdd(item.to, []);

                graph[item.from].Add((item.to, item.cost));
                graph[item.to].Add((item.from, item.cost));
            }

            Dictionary<(string currNode, string visited), int> memo = [];
            int DFS(string node, HashSet<string> visited)
            {
                if (visited.Count == graph.Keys.Count - 1)
                    return 0;

                string visitedStr = string.Join(",", visited.Order());
                if (memo.TryGetValue((node, visitedStr), out var memoVal))
                    return memoVal;

                visited.Add(node);

                int sol = int.MaxValue / 2;
                foreach (var item in graph[node].Where(x => !visited.Contains(x.destination)))
                    sol = Math.Min(sol, DFS(item.destination, visited) + item.cost);

                visited.Remove(node);

                return sol;
            }

            int solution = int.MaxValue;
            foreach (var item in graph.Keys)
                solution = Math.Min(solution, DFS(item, []));

            // For the test: 605
            Console.WriteLine($"The distance of the shortest route is: {solution}.");
        }

        public void PartTwo(string inputPath)
        {
            (string from, string to, int cost)[] s = File.ReadAllLines(inputPath)
                .Select(x =>
                {
                    var eqSplit = x.Split(" = ");
                    var leftSplit = eqSplit[0].Split(" to ");
                    return (leftSplit[0], leftSplit[1], int.Parse(eqSplit[1]));
                })
                .ToArray();

            Dictionary<string, List<(string destination, int cost)>> graph = [];
            foreach (var item in s)
            {
                graph.TryAdd(item.from, []);
                graph.TryAdd(item.to, []);

                graph[item.from].Add((item.to, item.cost));
                graph[item.to].Add((item.from, item.cost));
            }

            Dictionary<(string currNode, string visited), int> memo = [];
            int DFS(string node, HashSet<string> visited)
            {
                if (visited.Count == graph.Keys.Count - 1)
                    return 0;

                string visitedStr = string.Join(",", visited.Order());
                if (memo.TryGetValue((node, visitedStr), out var memoVal))
                    return memoVal;

                visited.Add(node);

                int sol = 0;
                foreach (var item in graph[node].Where(x => !visited.Contains(x.destination)))
                    sol = Math.Max(sol, DFS(item.destination, visited) + item.cost);

                visited.Remove(node);

                return sol;
            }

            int solution = 0;
            foreach (var item in graph.Keys)
                solution = Math.Max(solution, DFS(item, []));

            // For the test: 982
            Console.WriteLine($"The distance of the longest route is: {solution}.");
        }
    }
}
