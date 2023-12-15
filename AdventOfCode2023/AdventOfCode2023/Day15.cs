using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day15
    {
        public Day15()
        {

        }
        public int PartOne() //495972
        {
            string[] s = File.ReadAllLines("day15.txt")[0].Split(',');
            //string[] s = File.ReadAllLines("day15test.txt")[0].Split(',');

            int solution = 0;
            foreach (var item in s)
            {
                int temp = 0;
                foreach (var chr in item)
                    temp = ((temp + chr) * 17) % 256;

                solution += temp;
            }

            return solution;

        }
        public int PartTwo() //245223
        {
            string[] s = File.ReadAllLines("day15.txt")[0].Split(',');
            //string[] s = File.ReadAllLines("day15test.txt")[0].Split(',');
            List<List<string>> boxes = new List<List<string>>();
            for (int i = 0; i < 256; i++)
                boxes.Add(new List<string>());


            foreach (var item in s)
            {
                int temp = 0;

                string[] splitted = item.Split(new char[] { '-', '=' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var chr in splitted[0])
                    temp = ((temp + chr) * 17) % 256;

                int foundIndex = -1;
                for (int i = 0; i < boxes[temp].Count; i++)
                    if (boxes[temp][i].Split(' ')[0] == splitted[0])
                    {
                        boxes[temp].RemoveAt(i);
                        foundIndex = i;
                        break;
                    }

                if (splitted.Length == 1) //-
                    continue;
                else if (foundIndex == -1) //=
                    boxes[temp].Add(splitted[0] + " " + splitted[1]);
                else
                    boxes[temp].Insert(foundIndex, splitted[0] + " " + splitted[1]);
            }
            int solution = 0;

            for (int i = 0; i < boxes.Count; i++)
                for (int j = 0; j < boxes[i].Count; j++)
                    solution += (i + 1) * (j + 1) * int.Parse(boxes[i][j].Split(' ')[1]);

            return solution;
        }
    }
}
