namespace AdventOfCode2025
{
    public class Day9 : IDay
    {
        public string InputPath { get; set; } = "day9Input.txt";
        //public string InputPath { get; set; } = "day9InputTest.txt";

        public void PartOne()
        {
            long[][] s = File.ReadAllLines(InputPath).Select(x => x.Split(",").Select(long.Parse).ToArray()).ToArray();

            long solution = 1;

            for (int i = 0; i < s.Length; i++)
                for (int j = i + 1; j < s.Length; j++)
                    solution = Math.Max(solution, Math.Abs((s[i][0] - s[j][0] + 1) * (s[i][1] - s[j][1] + 1)));

            // 4760959496
            Console.WriteLine($"The largest area of any rectangle you can make is {solution}");
        }

        public void PartTwo()
        {
            string[] s = File.ReadAllLines(InputPath);

            int solution = 0;

            int rotation = 50;
            foreach (string item in s)
            {
                bool rotationStartedAtZero = rotation == 0;
                if (item[0] == 'R')
                    rotation += int.Parse(item.Substring(1));
                else
                    rotation -= int.Parse(item.Substring(1));

                solution += !rotationStartedAtZero && rotation <= 0 ? 1 : 0;
                solution += Math.Abs(rotation / 100);
                rotation %= 100;
                if(rotation < 0)
                    rotation += 100;
            }

            // 6623
            Console.WriteLine($"The password to open the door (with the method 0x434C49434B) is {solution}");
        }
    }
}
