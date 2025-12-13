using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public void PartTwo()
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

            string goalState = string.Join(",", program);

            for (int llll = 700000000; llll < int.MaxValue; llll++)
            {
                if (llll % 1000000 == 0)
                    Console.WriteLine($"Testing register A initial value: {llll}");

                registerA = llll;
                registerB = 0;
                registerC = 0;

                int outputIndex = 0;

                List<int> output = new();
                bool IsShet = false;

                for (int i = 0; i < program.Length - 1 && !IsShet; i += 2)
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
                            if (program.Length <= outputIndex
                                || (combo % 8) != program[outputIndex++])
                            {
                                IsShet = true;
                                continue;
                            }

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

                if (IsShet)
                    continue;

                string solution = string.Join(',', output);

                if (solution == goalState)
                    Console.WriteLine(registerA);
            }

            //Console.WriteLine($"{solution} is the lowest positive initial value for register A that causes the program to output a copy of itself"); // 1000000000 low
        }

        public void PartTwoSmart()
        {
            Range registerA = new();
            Range registerB = new();
            Range registerC = new();

            int[] program = Array.Empty<int>();

            Range GetComboValue(int operand)
            {
                if (operand <= 3)
                    return new(operand, operand);

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
                _ = int.Parse(f.ReadLine()!.Split(':').Last());
                _ = int.Parse(f.ReadLine()!.Split(':').Last());
                _ = int.Parse(f.ReadLine()!.Split(':').Last());
                f.ReadLine();
                program = f.ReadLine()!.Split(':').Last().Split(',').Select(int.Parse).ToArray();
            }

            string goalState = string.Join(",", program);

            int outputIndex = 0;

            List<int> output = new();

            for (int i = program.Length - 2; i >= 0; i -= 2)
            {
                int opcode = program[i];

                int literal = program[i + 1];
                Range combo = GetComboValue(program[i + 1]);

                switch (opcode)
                {
                    case 0:
                        //registerA = (int)(registerA / Math.Pow(2, combo));

                        long lowEndMultipler = (long)Math.Pow(2, combo.Start);
                        long highEndMultipler = (long)Math.Pow(2, combo.End + 1);
                        registerA = new Range(registerA.Start * lowEndMultipler, registerA.End * highEndMultipler - 1);
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
                        if (program.Length <= outputIndex
                            || (combo % 8) != program[outputIndex++])
                        {
                            IsShet = true;
                            continue;
                        }

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

            int solution = 0;

            Console.WriteLine($"{solution} is the lowest positive initial value for register A that causes the program to output a copy of itself"); // 1000000000 low
        }
    }
}
