using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    public class Day04 : IDay
    {
        public string InputPath { get; set; } = "Day4Input.txt";

        public void PartOne()
        {
            string hasKey = File.ReadAllLines(InputPath)[0];

            int solution = 1;
            while (true)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.ASCII.GetBytes(hasKey + solution.ToString());
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    string hexString = Convert.ToHexString(hashBytes);
                    if (hexString.Substring(0, 5) == "00000")
                        break;

                    solution++;
                }
            }

            Console.WriteLine($"The lowest positive number to produce a hash with 5 starting zeros is: {solution}.");
        }

        public void PartTwo()
        {
            string hasKey = File.ReadAllLines(InputPath)[0];

            int solution = 1;
            while (true)
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.ASCII.GetBytes(hasKey + solution.ToString());
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    string hexString = Convert.ToHexString(hashBytes);
                    if (hexString.Substring(0, 6) == "000000")
                        break;

                    solution++;
                }
            }

            Console.WriteLine($"\nThe lowest positive number to produce a hash with 6 starting zeros is: {solution}.");
        }
    }
}
