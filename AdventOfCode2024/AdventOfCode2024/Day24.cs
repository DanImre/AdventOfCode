using System.Net.Http.Headers;
using System.Xml.Linq;

namespace AdventOfCode2024
{
    public class Day24 : IDay
    {
        public string InputPath { get; set; } = "day24Input.txt";
        //public string InputPath { get; set; } = "day24TestInput.txt";

        public void PartOne()
        {
            string[] s = File.ReadAllLines(InputPath);

            Dictionary<string, bool> valueDict = [];

            Dictionary<string, (string op, string[] ops)> nodes = [];

            int i = 0;
            while (s[i] != "")
            {
                var sp = s[i].Split(":");
                valueDict[sp[0]] = sp[1].Trim() == "1";
                ++i;
            }
            ++i;
            while (i < s.Length)
            {
                var sp = s[i].Split("->");
                var left = sp[0].Trim().Split(" ");
                nodes[sp[1].Trim()] = (left[1].Trim(), [left[0].Trim(), left[2].Trim()]);
                ++i;
            }

            bool calculate(string node)
            {
                if (valueDict.TryGetValue(node, out var val))
                    return val;

                bool result = false;
                switch (nodes[node].op)
                {
                    case "AND":
                        result = calculate(nodes[node].ops[0]) && calculate(nodes[node].ops[1]);
                        break;
                    case "XOR":
                        result = calculate(nodes[node].ops[0]) ^ calculate(nodes[node].ops[1]);
                        break;
                    case "OR":
                        result = calculate(nodes[node].ops[0]) || calculate(nodes[node].ops[1]);
                        break;
                }

                valueDict[node] = result;
                return result;
            }

            List<string> zs = [.. nodes.Keys.Where(x => x[0] == 'z')];

            long solution = 0;
            i = 0;
            foreach (var item in zs.Order())
            {
                bool result = calculate(item);

                if (result)
                    solution += (long)Math.Pow(2, i);

                ++i;
            }

            // For the test it should be: 7
            // 998
            Console.WriteLine($"The decimal number that is the output on the wires starting with z is {solution}!");
        }

        // Runs for 11.3735033 secs
        public void PartTwo()
        {
            string[][] s = [.. File.ReadAllLines(InputPath).Select(x => x.Split('-'))];

            long solution = 0;
            // For the test it should be: co,de,ka,ta
            // cc,ff,fh,fr,ny,oa,pl,rg,uj,wd,xn,xs,zw
            Console.WriteLine($"The password to get into the LAN party is '{solution}'!");
        }
    }
}
