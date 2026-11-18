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

            // For the test it should be: 2024
            // 48508229772400
            Console.WriteLine($"The decimal number that is the output on the wires starting with z is {solution}!");
        }

        // Total bullshit, had to cheat
        // Unsubstantiated assumtions are needed, like XOR has to be the the last gate before a z wire, I can prove this is not the case by adding 2 additional gates (an OR and an AND) and that is still a valid configuration based on the task description
        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            Dictionary<string, (string op, string[] ops)> nodes = [];

            int i = 0;
            while (s[i] != "")
            {
                ++i;
            }
            ++i;
            while (i < s.Length)
            {
                var sp = s[i].Split("->");
                var left = sp[0].Trim().Split(" ");

                var operandOne = left[0].Trim();
                var op = left[1].Trim();
                var operandTwo = left[2].Trim();
                var output = sp[1].Trim();

                nodes[output] = (op, [operandOne, operandTwo]);
                ++i;
            }

            char[] variableList = ['x', 'y', 'z'];
            string highestZ = nodes.Keys.Where(x => x[0] == 'z').MaxBy(x => int.Parse(x.Substring(1)))!;
            HashSet<string> wrong = [];
            foreach (var item in nodes)
            {
                var op1 = item.Value.ops[0];
                var op = item.Value.op;
                var op2 = item.Value.ops[1];
                var res = item.Key;
                if (res[0] == 'z' && op != "XOR" && res != highestZ)
                    wrong.Add(res);

                if (
                    op == "XOR"
                    && !variableList.Contains(res[0])
                    && !variableList.Contains(op1[0])
                    && !variableList.Contains(op2[0])
                )
                    wrong.Add(res);

                if (op == "AND" && "x00" != op1 && "x00" != op2)
                    foreach (var subItem in nodes)
                    {
                        var subop1 = subItem.Value.ops[0];
                        var subop = subItem.Value.op;
                        var subop2 = subItem.Value.ops[1];
                        var subres = subItem.Key;

                        if ((res == subop1 || res == subop2) && subop != "OR")
                            wrong.Add(res);
                    }

                if (op == "XOR")
                    foreach (var subItem in nodes)
                    {
                        var subop1 = subItem.Value.ops[0];
                        var subop = subItem.Value.op;
                        var subop2 = subItem.Value.ops[1];
                        var subres = subItem.Key;

                        if ((res == subop1 || res == subop2) && subop == "OR")
                            wrong.Add(res);
                    }
            }

            // For the test it should be: 2024
            // 48508229772400
            Console.WriteLine($"You get '{string.Join(",", wrong.Order())}' if you sort the names of the eight wires involved in a swap and then join those names with commas!");
        }
    }
}
