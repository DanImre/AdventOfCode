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
                    Day1 d = new Day1();
                    
                    if (ans2 == "1")
                        Console.WriteLine(d.PartOne());
                    else
                        Console.WriteLine(d.PartTwo());
                    break;
                case "2":
                    Day2 d2 = new Day2();

                    if (ans2 == "1")
                        Console.WriteLine(d2.PartOne());
                    else
                        Console.WriteLine(d2.PartTwo());
                    break;
                case "3":
                    Day3 d3 = new Day3();

                    if (ans2 == "1")
                        Console.WriteLine(d3.PartOne());
                    else
                        Console.WriteLine(d3.PartTwo());
                    break;
                case "4":
                    Day4 d4 = new Day4();

                    if (ans2 == "1")
                        Console.WriteLine(d4.PartOne());
                    else
                        Console.WriteLine(d4.PartTwo());
                    break;
                case "5":
                    Day5 d5 = new Day5();
                    if (ans2 == "1")
                        Console.WriteLine(d5.PartOne());
                    else
                        Console.WriteLine(d5.PartTwo());
                    break;
                case "6":
                    Day6 d6 = new Day6();
                    if (ans2 == "1")
                        Console.WriteLine(d6.PartOne());
                    else
                        Console.WriteLine(d6.PartTwo());
                    break;
                case "7":
                    Day7 d7 = new Day7();
                    if (ans2 == "1")
                        Console.WriteLine(d7.PartOne());
                    else
                        Console.WriteLine(d7.PartTwo());
                    break;
                case "8":
                    Day8 d8 = new Day8();
                    if (ans2 == "1")
                        Console.WriteLine(d8.PartOne());
                    else
                        Console.WriteLine(d8.PartTwo());
                    break;
                case "9":
                    Day9 d9 = new Day9();
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
                default:
                    break;
            }
        }
    }
}
