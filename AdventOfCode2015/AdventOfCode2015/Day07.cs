using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day07 : IDay
    {
        private enum Operation
        {
            EQUALS,
            NOT,
            AND,
            OR,
            LSHIFT,
            RSHIFT,
        }
        public void PartOne(string inputPath)
        {
            string[] s = File.ReadAllLines(inputPath);
            Dictionary<string, (Operation op, string[] operands)> gates = [];

            foreach (var row in s)
            {
                string[] arrowSplit = row.Split(" -> ");
                string[] leftSplit = arrowSplit[0].Split(" ");
                if (leftSplit.Length == 1)
                {
                    gates[arrowSplit[1]] = (Operation.EQUALS, [arrowSplit[0]]);
                    continue;
                }

                if (leftSplit.Length == 2)
                {
                    gates[arrowSplit[1]] = (Operation.NOT, [leftSplit[1]]);
                    continue;
                }

                Operation op = (Operation)Enum.Parse(typeof(Operation), leftSplit[1]);
                gates[arrowSplit[1]] = (op, [leftSplit[0], leftSplit[2]]);
            }

            Dictionary<string, ushort> memo = [];

            ushort calculate(string wire)
            {
                if (ushort.TryParse(wire, out var val))
                    return val;

                if(memo.TryGetValue(wire, out var memoVal))
                    return memoVal;

                ushort[] values = [.. gates[wire].operands.Select(calculate)];

                ushort value = (ushort)(gates[wire].op switch
                {
                    Operation.EQUALS => values[0],
                    Operation.NOT => ~values[0],
                    Operation.AND => values[0] & values[1],
                    Operation.OR => values[0] | values[1],
                    Operation.LSHIFT => values[0] << values[1],
                    Operation.RSHIFT => values[0] >> values[1],
                    _ => throw new NotImplementedException(),
                });

                memo[wire] = value;
                return value;
            }

            int solution = calculate("a");
            Console.WriteLine($"The result signal on wire \"a\" is: {solution}.");
        }

        public void PartTwo(string inputPath)
        {
            string[] s = File.ReadAllLines(inputPath);
            Dictionary<string, (Operation op, string[] operands)> gates = [];

            foreach (var row in s)
            {
                string[] arrowSplit = row.Split(" -> ");
                string[] leftSplit = arrowSplit[0].Split(" ");
                if (leftSplit.Length == 1)
                {
                    gates[arrowSplit[1]] = (Operation.EQUALS, [arrowSplit[0]]);
                    continue;
                }

                if (leftSplit.Length == 2)
                {
                    gates[arrowSplit[1]] = (Operation.NOT, [leftSplit[1]]);
                    continue;
                }

                Operation op = (Operation)Enum.Parse(typeof(Operation), leftSplit[1]);
                gates[arrowSplit[1]] = (op, [leftSplit[0], leftSplit[2]]);
            }

            Dictionary<string, ushort> memo = [];

            ushort calculate(string wire)
            {
                if (ushort.TryParse(wire, out var val))
                    return val;

                if (memo.TryGetValue(wire, out var memoVal))
                    return memoVal;

                ushort[] values = [.. gates[wire].operands.Select(calculate)];

                ushort value = (ushort)(gates[wire].op switch
                {
                    Operation.EQUALS => values[0],
                    Operation.NOT => ~values[0],
                    Operation.AND => values[0] & values[1],
                    Operation.OR => values[0] | values[1],
                    Operation.LSHIFT => values[0] << values[1],
                    Operation.RSHIFT => values[0] >> values[1],
                    _ => throw new NotImplementedException(),
                });

                memo[wire] = value;
                return value;
            }

            ushort newB = calculate("a");

            memo.Clear();
            memo["b"] = newB;

            ushort solution = calculate("a");

            Console.WriteLine($"The result signal on wire \"a\" is: {solution}.");
        }
    }
}
