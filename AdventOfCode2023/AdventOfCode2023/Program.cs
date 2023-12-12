using System.Reflection;

namespace AdventOfCode2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? ans1;
            int ans1Int = -1;

            string? ans2;
            do
            {
                Console.WriteLine("WHich day and part would you like to run? [1-25]");
                ans1 = Console.ReadLine();
            } while (!int.TryParse(ans1, out ans1Int) || ans1Int < 0 || ans1Int > 25);

            do
            {
                Console.WriteLine("Part 1 or Part 2? [1,2]");
                ans2 = Console.ReadLine();
            } while (ans2 == null || ans2.Length != 1 || !char.IsDigit(ans2[0]));

            switch (ans1)
            {
                case "1":
                    Day01 d = new Day01();
                    
                    if (ans2 == "1")
                        Console.WriteLine(d.PartOne());
                    else
                        Console.WriteLine(d.PartTwo());
                    break;
                case "2":
                    Day02 d2 = new Day02();

                    if (ans2 == "1")
                        Console.WriteLine(d2.PartOne());
                    else
                        Console.WriteLine(d2.PartTwo());
                    break;
                case "3":
                    Day03 d3 = new Day03();

                    if (ans2 == "1")
                        Console.WriteLine(d3.PartOne());
                    else
                        Console.WriteLine(d3.PartTwo());
                    break;
                case "4":
                    Day04 d4 = new Day04();

                    if (ans2 == "1")
                        Console.WriteLine(d4.PartOne());
                    else
                        Console.WriteLine(d4.PartTwo());
                    break;
                case "5":
                    Day05 d5 = new Day05();
                    if (ans2 == "1")
                        Console.WriteLine(d5.PartOne());
                    else
                        Console.WriteLine(d5.PartTwo());
                    break;
                case "6":
                    Day06 d6 = new Day06();
                    if (ans2 == "1")
                        Console.WriteLine(d6.PartOne());
                    else
                        Console.WriteLine(d6.PartTwo());
                    break;
                case "7":
                    Day07 d7 = new Day07();
                    if (ans2 == "1")
                        Console.WriteLine(d7.PartOne());
                    else
                        Console.WriteLine(d7.PartTwo());
                    break;
                case "8":
                    Day08 d8 = new Day08();
                    if (ans2 == "1")
                        Console.WriteLine(d8.PartOne());
                    else
                        Console.WriteLine(d8.PartTwo());
                    break;
                case "9":
                    Day09 d9 = new Day09();
                    if (ans2 == "1")
                        Console.WriteLine(d9.PartOne());
                    else
                        Console.WriteLine(d9.PartTwo());
                    break;
                case "10":
                    Day10 d10 = new Day10();
                    if (ans2 == "1")
                        Console.WriteLine(d10.PartOne());
                    else
                        Console.WriteLine(d10.PartTwo());
                    break;
                case "11":
                    Day11 d11 = new Day11();
                    if (ans2 == "1")
                        Console.WriteLine(d11.PartOne());
                    else
                        Console.WriteLine(d11.PartTwo());
                    break;
                case "12":
                    Day12 d12 = new Day12();
                    if (ans2 == "1")
                        Console.WriteLine(d12.PartOne());
                    else
                        Console.WriteLine(d12.PartTwo());
                    break;
                default:
                    break;
            }
        }
    }
}
