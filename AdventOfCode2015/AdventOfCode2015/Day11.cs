using System.Text;

namespace AdventOfCode2015
{
    public class Day11 : IDay
    {
        // Brute force iter
        private string Iter(char[] value)
        {
            while (true)
            {
                for (int i = value.Length - 1; i >= 0; i--)
                {
                    if (value[i] != 'z')
                    {
                        value[i] = (char)(value[i]+1);
                        break;
                    }
                    else
                        value[i] = 'a';
                }

                if (isValid(value))
                    return new string(value);
            }
        }

        private char[] bannedLetters = ['i', 'o', 'l'];
        private bool isValid(char[] value)
        {
            if (value.Any(x => bannedLetters.Contains(x)))
                return false;

            bool firstCheck = false;
            for (int i = 0; i < value.Length - 2; i++)
                if (value[i + 1] - value[i] == 1
                && value[i + 2] - value[i + 1] == 1)
                {
                    firstCheck = true;
                    break;
                }

            if (!firstCheck)
                return false;

            HashSet<char> foundPair = [];
            int secondCheckCounter = 0;
            for (int i = 0; i < value.Length - 1; i++)
                if (value[i] == value[i + 1]
                    && foundPair.Add(value[i]))
                {
                    ++i;
                    secondCheckCounter++;
                    if (secondCheckCounter == 2)
                        return true;
                }

            return false;
        }

        private string PartOneSolution = "";
        // First idea, we could figure out some skipping but it's mainly brute-force
        // Lets just try true brute force for the first part

        public void PartOne(string inputPath)
        {
            string s = File.ReadAllLines(inputPath)[0];

            string solution = Iter(s.ToArray());
            PartOneSolution = solution;

            Console.WriteLine($"Santa's next password would be: {solution}.");
        }

        // Brute force worked great on the first one, we just have to repeat it again
        public void PartTwo(string inputPath)
        {
            PartOne(inputPath);
            string solution = Iter(PartOneSolution.ToArray());
            Console.WriteLine($"Santa's next password would be: {solution}.");
        }
    }
}
