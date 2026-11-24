using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2023
{
    public class Day25
    {
        public Day25()
        {

        }

        public int PartOne(string inputPath)
        {
            //string[] s = File.ReadAllLines("day25test.txt");
            string[] s = File.ReadAllLines(inputPath);

            List<(string Key, List<string> Conns)> parsed = s
                .Select(x => x.Split(":"))
                .Select(y => (y[0], y[1].Trim().Split(" ").ToList()))
                .ToList();

            string[] blacklistedConns =
            [
                "jzj - vkb",
                "vrx - hhx",
                "grh - nvh"
            ];

            Dictionary<string, HashSet<string>> graph = new();
            foreach (var item in parsed)
            {
                // Cuz graphwiz
                for (int i = 0; i < item.Conns.Count; i++)
                    if(blacklistedConns.Contains(item.Key + " - " + item.Conns[i])
                        || blacklistedConns.Contains(item.Conns[i] + " - " + item.Key))
                    {
                        item.Conns.RemoveAt(i);
                        break;
                    }

                if (graph.TryGetValue(item.Key, out var conns))
                    foreach (var conn in item.Conns)
                        conns.Add(conn);
                else
                    graph[item.Key] = item.Conns.ToHashSet();

                foreach (var conn in item.Conns)
                {
                    if (graph.TryGetValue(conn, out var subConns))
                        subConns.Add(item.Key);
                    else
                        graph[conn] = new() { item.Key };
                }
            }

            HashSet<string> visitedNodes = [];
            Queue<string> q = [];
            q.Enqueue(graph.Keys.First());
            while (q.Count > 0)
            {
                var curr = q.Dequeue();

                if (!visitedNodes.Add(curr))
                    continue;

                foreach (var item in graph[curr])
                    q.Enqueue(item);
            }

            return visitedNodes.Count * (graph.Keys.Count - visitedNodes.Count);

            // Using graphwiz we found that these are the connections that need to be removed
            // jzj - vkb
            // vrx - hhx
            // grh - nvh
            //List<string> content = [];
            //content.Add("digraph G {");
            //foreach (var item in graph)
            //{
            //    foreach (var t in item.Value)
            //    {
            //        if (string.Compare(item.Key, t) > 0)
            //            content.Add(item.Key + " -> " + t);
            //        else
            //            content.Add(t + " -> " + item.Key);

            //        content[^1] = $"\t{content[^1]};";
            //    }
            //}
            //content.Add("}");
            ////File.WriteAllLines("2023day25.dot", content.Distinct());
            //File.WriteAllLines("2023day25WithoutConns.dot", content.Distinct());
        }

        public long PartTwo(string inputPath)
        {
            //string[] s = File.ReadAllLines("day25test.txt");
            string[] s = File.ReadAllLines(inputPath);

            return -1;
        }
    }
}
