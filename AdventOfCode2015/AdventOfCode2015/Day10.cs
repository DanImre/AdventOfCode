using System.Text;

namespace AdventOfCode2015
{
    public class Day10 : IDay
    {
        private string Iter(string value)
        {
            StringBuilder sb = new();
            int i = 0;

            while (i < value.Length)
            {
                int counter = 0;
                char currentChar = value[i];
                while (i < value.Length && value[i] == currentChar)
                {
                    counter++;
                    ++i;
                }

                sb.Append(counter);
                sb.Append(currentChar);
            }

            return sb.ToString();
        }

        public void PartOne(string inputPath)
        {
            string s = File.ReadAllLines(inputPath)[0];

            for (int i = 0; i < 40; i++)
                s = Iter(s);

            int solution = s.Length;
            Console.WriteLine($"The length of the result is: {solution}.");
        }

        public void PartTwo(string inputPath)
        {
            string s = File.ReadAllLines(inputPath)[0];

            for (int i = 0; i < 50; i++)
                s = Iter(s);

            int solution = s.Length;
            Console.WriteLine($"The length of the new result is: {solution}.");
        }
    }
}
