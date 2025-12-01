using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public interface IDay
    {
        public string InputPath { get; set; }

        public void PartOne();

        public void PartTwo();
    }
}
