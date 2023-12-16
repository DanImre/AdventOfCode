using System.Diagnostics;
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

            Stopwatch timer = new Stopwatch();
            timer.Start();

            switch (ans1)
            {
                case "1":
                    Day01 d = new Day01();

                    if (ans2 == "1")
                        Console.WriteLine($"The sum of all of the calibration values: {d.PartOne()}");
                    else
                        Console.WriteLine($"The new sum of all of the calibration values: {d.PartTwo()}");
                    break;
                case "2":
                    Day02 d2 = new Day02();

                    if (ans2 == "1")
                        Console.WriteLine($"The sum of the IDs of those games: {d2.PartOne()}");
                    else
                        Console.WriteLine($"The sum of the power of these sets: {d2.PartTwo()}");
                    break;
                case "3":
                    Day03 d3 = new Day03();

                    if (ans2 == "1")
                        Console.WriteLine($"The sum of all of the part numbers in the engine schematic: {d3.PartOne()}");
                    else
                        Console.WriteLine($"The sum of all of the gear ratios in your engine schematic: {d3.PartTwo()}");
                    break;
                case "4":
                    Day04 d4 = new Day04();

                    if (ans2 == "1")
                        Console.WriteLine($"Point total: {d4.PartOne()}");
                    else
                        Console.WriteLine($"Total scratchcards: {d4.PartTwo()}");
                    break;
                case "5":
                    Day05 d5 = new Day05();
                    if (ans2 == "1")
                        Console.WriteLine($"The lowest location number that corresponds to any of the initial seed numbers: {d5.PartOne()}");
                    else
                        Console.WriteLine($"The lowest location number that corresponds to any of the initial seed numbers: {d5.PartTwo()}");
                    break;
                case "6":
                    Day06 d6 = new Day06();
                    if (ans2 == "1")
                        Console.WriteLine($"The numbers multiplied together: {d6.PartOne()}");
                    else
                        Console.WriteLine($"Ways to beat the record in the much longer race: {d6.PartTwo()}");
                    break;
                case "7":
                    Day07 d7 = new Day07();
                    if (ans2 == "1")
                        Console.WriteLine($"The total winnings: {d7.PartOne()}");
                    else
                        Console.WriteLine($"The new total winnings: {d7.PartTwo()}");
                    break;
                case "8":
                    Day08 d8 = new Day08();
                    if (ans2 == "1")
                        Console.WriteLine($"Steps required to reach ZZZ: {d8.PartOne()}");
                    else
                        Console.WriteLine($"Steps required to be only on nodes that end with Z: {d8.PartTwo()}");
                    break;
                case "9":
                    Day09 d9 = new Day09();
                    if (ans2 == "1")
                        Console.WriteLine($"The sum of these extrapolated values: {d9.PartOne()}");
                    else
                        Console.WriteLine($"is the sum of these backwards extrapolated values: {d9.PartTwo()}");
                    break;
                case "10":
                    Day10 d10 = new Day10();
                    if (ans2 == "1")
                        Console.WriteLine($"Steps required: {d10.PartOne()}");
                    else
                        Console.WriteLine($"Tiles enclosed by the loop: {d10.PartTwo()}");
                    break;
                case "11":
                    Day11 d11 = new Day11();
                    if (ans2 == "1")
                        Console.WriteLine($"The sum of these lengths: {d11.PartOne()}");
                    else
                        Console.WriteLine($"The sum of these new lengths: {d11.PartTwo()}");
                    break;
                case "12":
                    Day12 d12 = new Day12();
                    if (ans2 == "1")
                        Console.WriteLine($"The sum of those counts: {d12.PartOne()}");
                    else
                        Console.WriteLine($"The new sum of possible arrangement counts: {d12.PartTwo()}");
                    break;
                case "13":
                    Day13 d13 = new Day13();
                    if (ans2 == "1")
                        Console.WriteLine($"After summarizing all of my notes: {d13.PartOne()}");
                    else
                        Console.WriteLine($"After summarizing the line in each pattern: {d13.PartTwo()}");
                    break;
                case "14":
                    Day14 d14 = new Day14();
                    if (ans2 == "1")
                        Console.WriteLine($"The total load on the north support beams: {d14.PartOne()}");
                    else
                        Console.WriteLine($"The total load on the north support beams after 1000000000 cycles: {d14.PartTwo()}");
                    break;
                case "15":
                    Day15 d15 = new Day15();
                    if (ans2 == "1")
                        Console.WriteLine($"The sum of the initialization sequence results: {d15.PartOne()}");
                    else
                        Console.WriteLine($"The focusing power of the resulting lens configuration: {d15.PartTwo()}");
                    break;
                case "16":
                    Day16 d16 = new Day16();
                    if (ans2 == "1")
                        Console.WriteLine($"The amount of tiles end up being energized: {d16.PartOne()}");
                    else
                        Console.WriteLine($"The amount of tiles are energized in that configuration: {d16.PartTwo()}");
                    break;
                default:
                    break;
            }

            Console.WriteLine($"Time elapsed: {timer.ElapsedMilliseconds}ms");
        }
    }
}
