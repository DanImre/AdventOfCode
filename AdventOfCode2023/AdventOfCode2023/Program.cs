namespace AdventOfCode2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WHich day and part would you like to run? [1-25]");
            switch (Console.ReadLine())
            {
                case "1":
                    Day1 d = new Day1();
                    Console.WriteLine("Part 1 or Part 2? [1,2]");
                    if (Console.ReadLine() == "1")
                        Console.WriteLine(d.PartOne());
                    else
                        Console.WriteLine(d.PartTwo());
                    break;
                default:
                    break;
            }
        }
    }
}
