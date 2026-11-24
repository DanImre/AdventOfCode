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
                        Console.WriteLine($"The sum of all of the calibration values: {d.PartOne("Inputs/day1.txt")}");
                    else
                        Console.WriteLine($"The new sum of all of the calibration values: {d.PartTwo("Inputs/day1.txt")}");
                    break;
                case "2":
                    Day02 d2 = new Day02();

                    if (ans2 == "1")
                        Console.WriteLine($"The sum of the IDs of those games: {d2.PartOne("Inputs/day2.txt")}");
                    else
                        Console.WriteLine($"The sum of the power of these sets: {d2.PartTwo("Inputs/day2.txt")}");
                    break;
                case "3":
                    Day03 d3 = new Day03();

                    if (ans2 == "1")
                        Console.WriteLine($"The sum of all of the part numbers in the engine schematic: {d3.PartOne("Inputs/day3.txt")}");
                    else
                        Console.WriteLine($"The sum of all of the gear ratios in your engine schematic: {d3.PartTwo("Inputs/day3.txt")}");
                    break;
                case "4":
                    Day04 d4 = new Day04();

                    if (ans2 == "1")
                        Console.WriteLine($"Point total: {d4.PartOne("Inputs/day4.txt")}");
                    else
                        Console.WriteLine($"Total scratchcards: {d4.PartTwo("Inputs/day4.txt")}");
                    break;
                case "5":
                    Day05 d5 = new Day05();
                    if (ans2 == "1")
                        Console.WriteLine($"The lowest location number that corresponds to any of the initial seed numbers: {d5.PartOne("Inputs/day5.txt")}");
                    else
                        Console.WriteLine($"The lowest location number that corresponds to any of the initial seed numbers: {d5.PartTwo("Inputs/day5.txt")}");
                    break;
                case "6":
                    Day06 d6 = new Day06();
                    if (ans2 == "1")
                        Console.WriteLine($"The numbers multiplied together: {d6.PartOne("Inputs/day6.txt")}");
                    else
                        Console.WriteLine($"Ways to beat the record in the much longer race: {d6.PartTwo("Inputs/day6.txt")}");
                    break;
                case "7":
                    Day07 d7 = new Day07();
                    if (ans2 == "1")
                        Console.WriteLine($"The total winnings: {d7.PartOne("Inputs/day7.txt")}");
                    else
                        Console.WriteLine($"The new total winnings: {d7.PartTwo("Inputs/day7.txt")}");
                    break;
                case "8":
                    Day08 d8 = new Day08();
                    if (ans2 == "1")
                        Console.WriteLine($"Steps required to reach ZZZ: {d8.PartOne("Inputs/day8.txt")}");
                    else
                        Console.WriteLine($"Steps required to be only on nodes that end with Z: {d8.PartTwo("Inputs/day8.txt")}");
                    break;
                case "9":
                    Day09 d9 = new Day09();
                    if (ans2 == "1")
                        Console.WriteLine($"The sum of these extrapolated values: {d9.PartOne("Inputs/day9.txt")}");
                    else
                        Console.WriteLine($"is the sum of these backwards extrapolated values: {d9.PartTwo("Inputs/day9.txt")}");
                    break;
                case "10":
                    Day10 d10 = new Day10();
                    if (ans2 == "1")
                        Console.WriteLine($"Steps required: {d10.PartOne("Inputs/day10.txt")}");
                    else
                        Console.WriteLine($"Tiles enclosed by the loop: {d10.PartTwo("Inputs/day10.txt")}");
                    break;
                case "11":
                    Day11 d11 = new Day11();
                    if (ans2 == "1")
                        Console.WriteLine($"The sum of these lengths: {d11.PartOne("Inputs/day11.txt")}");
                    else
                        Console.WriteLine($"The sum of these new lengths: {d11.PartTwo("Inputs/day11.txt")}");
                    break;
                case "12":
                    Day12 d12 = new Day12();
                    if (ans2 == "1")
                        Console.WriteLine($"The sum of those counts: {d12.PartOne("Inputs/day12.txt")}");
                    else
                        Console.WriteLine($"The new sum of possible arrangement counts: {d12.PartTwo("Inputs/day12.txt")}");
                    break;
                case "13":
                    Day13 d13 = new Day13();
                    if (ans2 == "1")
                        Console.WriteLine($"After summarizing all of my notes: {d13.PartOne("Inputs/day13.txt")}");
                    else
                        Console.WriteLine($"After summarizing the line in each pattern: {d13.PartTwo("Inputs/day13.txt")}");
                    break;
                case "14":
                    Day14 d14 = new Day14();
                    if (ans2 == "1")
                        Console.WriteLine($"The total load on the north support beams: {d14.PartOne("Inputs/day14.txt")}");
                    else
                        Console.WriteLine($"The total load on the north support beams after 1000000000 cycles: {d14.PartTwo("Inputs/day14.txt")}");
                    break;
                case "15":
                    Day15 d15 = new Day15();
                    if (ans2 == "1")
                        Console.WriteLine($"The sum of the initialization sequence results: {d15.PartOne("Inputs/day15.txt")}");
                    else
                        Console.WriteLine($"The focusing power of the resulting lens configuration: {d15.PartTwo("Inputs/day15.txt")}");
                    break;
                case "16":
                    Day16 d16 = new Day16();
                    if (ans2 == "1")
                        Console.WriteLine($"The amount of tiles end up being energized: {d16.PartOne("Inputs/day16.txt")}");
                    else
                        Console.WriteLine($"The amount of tiles are energized in that configuration: {d16.PartTwo("Inputs/day16.txt")}");
                    break;
                case "17":
                    Day17 d17= new Day17();
                    if (ans2 == "1")
                        Console.WriteLine($"The least heat loss it can incur: {d17.PartOne("Inputs/day17.txt")}");
                    else
                        Console.WriteLine($"The least heat loss it can incur with the ultra crucibles: {d17.PartTwo("Inputs/day17.txt")}");
                    break;
                case "18":
                    Day18 d18 = new Day18();
                    if (ans2 == "1")
                        Console.WriteLine($"The lagoon can hold this many cubic meters of lava: {d18.PartOne("Inputs/day18.txt")}");
                    else
                        Console.WriteLine($"The lagoon can hold this many cubic meters of lava with the correct instructions: {d18.PartTwo("Inputs/day18.txt")}");
                    break;
                case "19":
                    Day19 d19 = new Day19();
                    if (ans2 == "1")
                        Console.WriteLine($"Sum of ratings of the accepted parts: {d19.PartOne("Inputs/day19.txt")}");
                    else
                        Console.WriteLine($"The Distinct combinations of ratings that will be accepted: {d19.PartTwo("Inputs/day19.txt")}");
                    break;

                case "25":
                    Day25 d25 = new Day25();
                    if (ans2 == "1")
                        Console.WriteLine($"You get {d25.PartOne("Inputs/day25.txt")} if you multiply the sizes of these two groups together!");
                    else
                        Console.WriteLine($"The Distinct combinations of ratings that will be accepted: {d25.PartTwo("Inputs/day25.txt")}");
                    break;
                default:
                    break;
            }

            Console.WriteLine($"Time elapsed: {timer.ElapsedMilliseconds}ms");
        }
    }
}
