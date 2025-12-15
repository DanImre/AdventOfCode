namespace AdventOfCode2024
{
    public class Day21 : IDay
    {
        //public string InputPath { get; set; } = "day21Input.txt";
        public string InputPath { get; set; } = "day21InputTest.txt";

        private List<(int y, int x)> moves =
        [
            (1, 0),
            (-1, 0),
            (0, 1),
            (0, -1)
        ];
        public void PartOne()
        {
            char[,] numericMap = new char[4, 3]
            {
                { '7', '8', '9' },
                { '4', '5', '6' },
                { '1', '2', '3' },
                { '_', '0', 'A' }
            };
            Dictionary<char, int> numericToState = new();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 3; j++)
                {
                    int currState = i * 3 + j;
                    numericToState[numericMap[i, j]] = currState;
                }

            char[,] directionMap = new char[2, 3]
            {
                { '_', '^', 'A' },
                { '<', 'v', '>' }
            };
            Dictionary<char, int> directionToState = new();
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 3; j++)
                {
                    int currState = i * 3 + j;
                    directionToState[directionMap[i, j]] = currState;
                }

            long solution = 0;

            // For the test it should be: 126384
            Console.WriteLine($"the sum of the complexities of the five codes on my list is: {solution}!");
        }

        public void PartTwo()
        {
            int solution = 0;
            Console.WriteLine($"The total distance between my lists is: {solution}!");
        }
    }
}
