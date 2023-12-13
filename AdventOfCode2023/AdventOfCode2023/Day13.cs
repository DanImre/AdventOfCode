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

        public int PartOne() //35210
        {
            //string[] file = File.ReadAllLines("day13test.txt");
            string[] file = File.ReadAllLines("day13.txt");

            int solution = 0;

            int kulsoIndex = 0;

            //need to repeat for every table
            while (kulsoIndex < file.Length)
            {
                List<string> temp = new List<string>();
                while (kulsoIndex < file.Length && file[kulsoIndex] != "")
                    temp.Add(file[kulsoIndex++]);

                string[] s = temp.ToArray();

                ++kulsoIndex;

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
                            rows[i].Add(0);
                            columns[j].Add(0);
                        }
                //columns
                bool needToBreak = false;
                for (int i = 1; i < columns.Count; i++)
                {
                    bool isSymetric = true;
                    for (int j = 1; j <= Math.Min(i,columns.Count - i) && isSymetric; j++)
                        if (!AreEqualLists(columns[i - j], columns[i + j - 1]))
                            isSymetric = false;

                    if (isSymetric)
                    {
                        solution += i;
                        needToBreak = true;
                        break;
                    }
                }
                if (needToBreak)
                    continue;

                //rows
                for (int i = 1; i < rows.Count; i++)
                {
                    bool isSymetric = true;
                    for (int j = 1; j <= Math.Min(i, rows.Count - i) && isSymetric; j++)
                        if (!AreEqualLists(rows[i - j], rows[i + j - 1]))
                            isSymetric = false;

                    if (isSymetric)
                    {
                        solution += 100 * i;
                        break;
                    }
                }
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
        public int ErrorBetweenLists(List<int> a, List<int> b)
        {
            //Each list is the same size anyways
            //if (a.Count != b.Count)
            //    return -1; 

            int solution = 0;

            for (int i = 0; i < a.Count; i++)
                if (a[i] != b[i])
                    ++solution;

            return solution;
        }

        public int PartTwo() //31974
        {
            //string[] file = File.ReadAllLines("day13test.txt");
            string[] file = File.ReadAllLines("day13.txt");

            int solution = 0;

            int kulsoIndex = 0;

            //need to repeat for every table
            while (kulsoIndex < file.Length)
            {
                List<string> temp = new List<string>();
                while (kulsoIndex < file.Length && file[kulsoIndex] != "")
                    temp.Add(file[kulsoIndex++]);

                string[] s = temp.ToArray();

                ++kulsoIndex;

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
                            rows[i].Add(0);
                            columns[j].Add(0);
                        }

                (bool horizontal, int index) prevSolution = (false, -1);
                //columns
                for (int i = 1; i < columns.Count; i++)
                {
                    bool isSymetric = true;
                    for (int j = 1; j <= Math.Min(i, columns.Count - i) && isSymetric; j++)
                        if (!AreEqualLists(columns[i - j], columns[i + j - 1]))
                            isSymetric = false;

                    if (isSymetric)
                    {
                        prevSolution = (true, i);
                        break;
                    }
                }

                //rows
                if (prevSolution.index == -1)
                    for (int i = 1; i < rows.Count; i++)
                    {
                        bool isSymetric = true;
                        for (int j = 1; j <= Math.Min(i, rows.Count - i) && isSymetric; j++)
                            if (!AreEqualLists(rows[i - j], rows[i + j - 1]))
                                isSymetric = false;

                        if (isSymetric)
                        {
                            prevSolution = (false, i);
                            break;
                        }
                    }


                bool needToBreak = false;
                for (int i = 1; i < columns.Count; i++)
                {
                    int difference = 0;
                    for (int j = 1; j <= Math.Min(i, columns.Count - i) && difference <= 1; j++)
                            difference += ErrorBetweenLists(columns[i - j], columns[i + j - 1]);

                    if (difference == 1)
                    {
                        solution += i;
                        needToBreak = true;
                        break;
                    }
                }

                if (needToBreak)
                    continue;

                for (int i = 1; i < rows.Count; i++)
                {
                    int difference = 0;
                    for (int j = 1; j <= Math.Min(i, rows.Count - i) && difference <= 1; j++)
                        difference += ErrorBetweenLists(rows[i - j], rows[i + j - 1]);

                    if (difference == 1)
                    {
                        solution += 100 * i;
                        break;
                    }
                }
            }

            return solution;
        }
    }
}
