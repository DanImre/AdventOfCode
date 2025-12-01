using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
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

            Queue<int> output = new(program);

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
                        if (output.Dequeue() != combo % 8)
                            continue;
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
