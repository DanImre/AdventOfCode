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
    public class Day08 : IDay
    {
        public void PartOne(string inputPath)
        {
            string[] s = File.ReadAllLines(inputPath);

            int solution = 0;
            foreach (var row in s)
                solution += row.Length - Regex.Unescape(row).Length + 2;

            // For the test: 12
            Console.WriteLine($"The number of characters of code for string literals minus the number of characters in memory for the values of the strings is: {solution}.");
        }

        public void PartTwo(string inputPath)
        {
            string[] s = File.ReadAllLines(inputPath);

            int solution = 0;
            foreach (var row in s)
                solution += row.Count(x => x == '\"' || x == '\\') + 2;

            // For the test: 19
            Console.WriteLine($"The total number of characters to represent the newly encoded strings minus the number of characters of code in each original string literal is: {solution}.");
        }
    }
}
