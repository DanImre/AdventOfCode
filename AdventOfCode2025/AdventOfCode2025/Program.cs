using System.Diagnostics;
using System.Threading.Channels;

namespace AdventOfCode2025
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
                    string? input = Console.ReadLine()?.Trim();
                    if (input?.ToLower() == "q")
                    {
                        keepRunning = false;
                        break;
                    }
                    int.TryParse(input, out chosenDay);
                    classType = Type.GetType("AdventOfCode2025.Day" + chosenDay);
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
                    string? input = Console.ReadLine()?.Trim();
                    if (input?.ToLower() == "q")
                    {
                        keepRunning = false;
                        break;
                    }
                    int.TryParse(input, out chosenPart);
                }
                while (chosenPart < 1 || chosenPart > 2);

                Console.ForegroundColor = ConsoleColor.White;

                if (!keepRunning)
                    break;

                Stopwatch stopwatch = Stopwatch.StartNew();

                if (chosenPart == 1)
                    dayInstance.PartOne();
                else
                    dayInstance.PartTwo();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Runtime: {stopwatch.Elapsed}");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"Good bye!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
