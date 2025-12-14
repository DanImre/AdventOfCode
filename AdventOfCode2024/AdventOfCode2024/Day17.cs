using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public class Range
    {
        public long Start { get; set; }
        public long End { get; set; }

        public Range() { }

        public Range(long start, long end)
        {
            Start = start;
            End = end;
        }
    }

    public class Day17 : IDay
    {
        public string InputPath { get; set; } = "day17Input.txt";
        //public string InputPath { get; set; } = "day17TestInput.txt";
        //public string InputPath { get; set; } = "day17TestInput2.txt";

        public void PartOne()
        {
            int registerA = 0;
            int registerB = 0;
            int registerC = 0;

            int[] program = Array.Empty<int>();

            int GetComboValue(int operand)
            {
                if (operand <= 3)
                    return operand;

                switch (operand)
                {
                    case 4:
                        return registerA;
                    case 5:
                        return registerB;
                    default:
                        return registerC;
                }
            }

            using (StreamReader f = new StreamReader(InputPath))
            {
                registerA = int.Parse(f.ReadLine()!.Split(':').Last());
                registerB = int.Parse(f.ReadLine()!.Split(':').Last());
                registerC = int.Parse(f.ReadLine()!.Split(':').Last());
                f.ReadLine();
                program = f.ReadLine()!.Split(':').Last().Split(',').Select(int.Parse).ToArray();
            }

            List<int> output = new();

            for (int i = 0; i < program.Length - 1; i += 2)
            {
                int opcode = program[i];

                int literal = program[i + 1];
                int combo = GetComboValue(program[i + 1]);

                switch (opcode)
                {
                    case 0:
                        registerA = (int)(registerA / Math.Pow(2, combo));
                        break;
                    case 1:
                        registerB ^= literal;
                        break;
                    case 2:
                        registerB = combo % 8;
                        break;
                    case 3:
                        if (registerA == 0)
                            continue;

                        i = literal - 2;
                        registerB = combo % 8;
                        break;
                    case 4:
                        registerB ^= registerC;
                        break;
                    case 5:
                        output.Add(combo % 8);
                        break;
                    case 6:
                        registerB = (int)(registerA / Math.Pow(2, combo));
                        break;
                    default:
                        registerC = (int)(registerA / Math.Pow(2, combo));
                        break;
                }
            }

            string solution = string.Join(',', output);

            Console.WriteLine($"The coma separeted output string is: {solution}");
        }

        // Credit: https://git.sr.ht/~murr/advent-of-code/tree/master/item/2024/17/p2.c
        /*
            Making a quine on this machine isn't as complicated as it looks:
            - op out only every reads 0-3 or the last 3 bits of reg A, B, or C
            - reg B and C are only ever set by:
                - xoring with literal 0-7 (ie on low 3 bits)
                - anding with last 3 bits of 0-3 or a reg (ie set to 0-7)
                - rshift of reg A
            - that means the whole program is basically just shifting off reg A,
              mutating the last 3 bits, and outputting it 3 bits at a time.
            - the xor and jump means we can't easily reverse it but above means that
              if you can get 3 bits in A that gives a valid out value, it will
              always output the same 3 bit value if lshifted by 3
            - not all series of values will eventually produce a correct answer so
              we search the full space, another DFS babee
        */
        public void PartTwo()
        {
            int[] program = Array.Empty<int>();

            using (StreamReader f = new StreamReader(InputPath))
            {
                _ = int.Parse(f.ReadLine()!.Split(':').Last());
                _ = int.Parse(f.ReadLine()!.Split(':').Last());
                _ = int.Parse(f.ReadLine()!.Split(':').Last());
                f.ReadLine();
                program = f.ReadLine()!.Split(':').Last().Split(',').Select(int.Parse).ToArray();
            }

            long registerA = 0;
            long registerB = 0;
            long registerC = 0;

            long GetComboValue(int operand)
            {
                if (operand <= 3)
                    return operand;

                switch (operand)
                {
                    case 4:
                        return registerA;
                    case 5:
                        return registerB;
                    default:
                        return registerC;
                }
            }

            long getFirstOutputWithRegisterAValue(long value)
            {
                registerA = value;
                registerB = 0;
                registerC = 0;

                for (int i = 0; i < program.Length - 1; i += 2)
                {
                    int opcode = program[i];

                    int literal = program[i + 1];
                    long combo = GetComboValue(program[i + 1]);

                    switch (opcode)
                    {
                        case 0:
                            registerA = (long)(registerA / Math.Pow(2, combo));
                            break;
                        case 1:
                            registerB ^= literal;
                            break;
                        case 2:
                            registerB = combo % 8;
                            break;
                        case 3:
                            if (registerA == 0)
                                continue;

                            i = literal - 2;
                            registerB = combo % 8;
                            break;
                        case 4:
                            registerB ^= registerC;
                            break;
                        case 5:
                            return combo % 8;
                        case 6:
                            registerB = (long)(registerA / Math.Pow(2, combo));
                            break;
                        default:
                            registerC = (long)(registerA / Math.Pow(2, combo));
                            break;
                    }
                }

                return -1;
            }

            long recursiveSolution(int index, long curr)
            {
                if (index < 0)
                    return curr;

                curr <<= 3;
                long target = program[index];
                for (int i = 0; i < 8; ++i)
                {
                    long n = curr + i;
                    long firstOutput = getFirstOutputWithRegisterAValue(curr + i);
                    if (firstOutput != target)
                        continue;

                    long nextIter = recursiveSolution(index - 1, n);
                    if (nextIter >= 0)
                        return nextIter;
                }
                return -1;
            }

            long solution = recursiveSolution(program.Length - 1, 0);

            // 37221270076916
            Console.WriteLine($"{solution} is the lowest positive initial value for register A that causes the program to output a copy of itself");
        }
    }
}
