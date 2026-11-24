using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                bool canLoadAssembly = true;
                int chosenYear = 0;
                string projectName = string.Empty;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Which year would you like to run? 20[15-..] (Q to quit)");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    string? input = Console.ReadLine();
                    if (input?.ToLower() == "q")
                    {
                        keepRunning = false;
                        break;
                    }

                    int.TryParse(input, out chosenYear);
                    projectName = $"AdventOfCode20{chosenYear}";

                    try
                    {
                        Assembly.Load(projectName);
                        canLoadAssembly = true;
                    }
                    catch (FileNotFoundException)
                    {
                        canLoadAssembly = false;
                    }
                }
                while (chosenYear < 15 || chosenYear > 99 || !canLoadAssembly);

                if (!keepRunning)
                    break;

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
                    string typeName = $"{projectName}.Day{chosenDay:D2}, {projectName}";
                    classType = Type.GetType(typeName);
                }
                while (chosenDay < 1 || chosenDay > 24 || classType == null);

                if (!keepRunning)
                    break;

                object dayInstance = Activator.CreateInstance(classType!)!;

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
                string[] possibleInputs = [.. Directory.GetFiles($".\\Inputs\\20{chosenYear}", "?ay*.txt").Where(x => Regex.IsMatch(x, $".ay{chosenDay}[a-zA-Z.]+"))];

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
                            Console.WriteLine($"\t[{i + 1}] {possibleInputs[i]}");
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

                MethodInfo? method = null;

                if (chosenPart == 1)
                    method = dayInstance.GetType().GetMethod("PartOne", [typeof(string)]);
                else
                    method = dayInstance.GetType().GetMethod("PartTwo", [typeof(string)]);

                if (method == null)
                {
                    Console.WriteLine("Missing method on that day! (PartOne or PartTwo)");
                    continue;
                }

                Stopwatch stopwatch = Stopwatch.StartNew();
                object? result = method.Invoke(dayInstance, [inputPath]);
                stopwatch.Stop();

                if (method.ReturnType != typeof(void))
                    Console.WriteLine($"Result: {result ?? "null"}");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"A kód {stopwatch.Elapsed} idő alatt futott le!");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"Good bye!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
