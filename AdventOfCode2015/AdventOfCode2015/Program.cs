using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2015
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Type? classType = null;
                int chosenDay = 0;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Which day would you like to run? [1-24] (Q to quit)");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    string? input = Console.ReadLine();
                    if (input?.ToLower() == "q")
                    {
                        keepRunning = false;
                        break;
                    }

                    int.TryParse(input, out chosenDay);
                    classType = Type.GetType("AdventOfCode2015.Day" + (chosenDay < 10 ? "0" : "") + chosenDay);
                }
                while (chosenDay < 1 || chosenDay > 24 || classType == null);

                if (!keepRunning)
                    break;

                IDay dayInstance = (IDay)Activator.CreateInstance(classType!)!;

                int chosenPart = 0;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Which part would you like to run ? [1-2] (Q to quit)");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    string? input = Console.ReadLine();
                    if (input?.ToLower() == "q")
                    {
                        keepRunning = false;
                        break;
                    }
                    int.TryParse(input, out chosenPart);
                }
                while (chosenPart < 1 || chosenPart > 2);

                if (!keepRunning)
                    break;

                string inputPath = "";
                string[] possibleInputs = [.. Directory.GetFiles("./", "Day*.txt").Select(x => x.Substring(2)).Where(x => Regex.IsMatch(x, $"Day{chosenDay}[a-zA-Z.]+"))];

                if (possibleInputs.Length > 0)
                {
                    int chosenInputFile = 0;
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"Which file would you like to run it with? [1-{possibleInputs.Length}] (Q to quit)");

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine();
                        for (int i = 0; i < possibleInputs.Length; i++)
                            Console.WriteLine($"\t[{i+1}] {possibleInputs[i]}");
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        string? input = Console.ReadLine();
                        if (input?.ToLower() == "q")
                        {
                            keepRunning = false;
                            break;
                        }
                        int.TryParse(input, out chosenInputFile);
                    }
                    while (chosenInputFile < 1 || chosenInputFile > possibleInputs.Length);

                    if (!keepRunning)
                        break;

                    inputPath = possibleInputs[chosenInputFile - 1];
                }
                else
                    Console.WriteLine("Input file not found!");

                Console.ForegroundColor = ConsoleColor.White;

                Stopwatch stopwatch = Stopwatch.StartNew();

                if (chosenPart == 1)
                    dayInstance.PartOne(inputPath);
                else
                    dayInstance.PartTwo(inputPath);

                stopwatch.Stop();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"A kód {stopwatch.Elapsed} idő alatt futott le!");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"Good bye!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
