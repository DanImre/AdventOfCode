using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public class Day13
    {
        public Day13()
        {

        }

        public int PartOne()
        {
            string[] file = File.ReadAllLines("day13test.txt");

            int solution = 0;

            int tableCount = file.Count(kk => kk == "") + 1;
            
            int kulsoIndex = 0;

            //need to repeat for every table
            while (kulsoIndex < file.Length)
            {
                List<string> temp = new List<string>();
                while (kulsoIndex < file.Length && file[kulsoIndex] != "")
                    temp.Add(file[kulsoIndex++]);

                string[] s = temp.ToArray();

                List<List<int>> rows = new List<List<int>>();
                List<List<int>> columns = new List<List<int>>();

                for (int i = 0; i < s.Length; i++)
                    rows.Add(new List<int>());
                for (int i = 0; i < s[0].Length; i++)
                    columns.Add(new List<int>());

                for (int i = 0; i < s.Length; i++)
                    for (int j = 0; j < s[i].Length; j++)
                        if (s[i][j] == '#')
                        {
                            rows[i].Add(1);
                            columns[j].Add(1);
                        }
                        else
                        {
                            rows[i].Add(1);
                            columns[j].Add(1);
                        }

                int index = -1;

                for (int i = 1; i < rows.Count; i++)
                {
                    int j = 1;
                    while (i - j >= 0 && i + j < rows.Count && AreEqualLists(rows[i - j], rows[i + j]))
                    {
                        ++j;
                    }
                    if (i - j == -1 || i + j == rows.Count) //talált szimetriatengelyt
                    {
                        index = i;
                        break;
                    }
                }

                if (index != -1)
                {
                    solution += index;
                    continue;
                }

                for (int i = 1; i < columns.Count; i++)
                {
                    int j = 1;
                    while (i - j >= 0 && i + j < columns.Count && AreEqualLists(rows[i - j], columns[i + j]))
                    {
                        ++j;
                    }
                    if (i - j == -1 || i + j == rows.Count) //talált szimetriatengelyt
                    {
                        index = i;
                        break;
                    }
                }

                solution += 100 * index;
            }

            return solution;
        }

        public bool AreEqualLists(List<int> a, List<int> b)
        {
            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
                if (a[i] != b[i])
                    return false;

            return true;
        }

        public int PartTwo()
        {
            return 0;
        }
    }
}
