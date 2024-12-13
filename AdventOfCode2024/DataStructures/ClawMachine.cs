using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class ClawMachine
    {
        public static int ACost = 3;
        public static int BCost = 1;
        public (int x, int y) AStep { get; set; } = (0, 0);
        public (int x, int y) BStep { get; set; } = (0, 0);
        public (int x, int y) Prize { get; set; } = (0, 0);
        public ClawMachine((int x, int y) aStep, (int x, int y) bStep, (int x, int y) prize)
        {
            AStep = aStep;
            BStep = bStep;
            Prize = prize;
        }

        public bool IsAtPrize((int x, int y) pos)
        {
            return Prize.x == pos.x && Prize.y == pos.y;
        }
        public bool IsBeyondPrize((int x, int y) pos)
        {
            return Prize.x < pos.x || Prize.y < pos.y;
        }

        public (int x, int y, int cost) StepWith(char which, (int x, int y, int cost) curr)
        {
            if (char.ToUpper(which) == 'A')
                return (curr.x + AStep.x, curr.y + AStep.y, curr.cost + ACost);

            return (curr.x + BStep.x, curr.y + BStep.y, curr.cost + BCost);
        }

        public (int x, int y, int cost, int iter) StepWith(char which, (int x, int y, int cost, int iter) curr)
        {
            if (char.ToUpper(which) == 'A')
                return (curr.x + AStep.x, curr.y + AStep.y, curr.cost + ACost, curr.iter + 1);

            return (curr.x + BStep.x, curr.y + BStep.y, curr.cost + BCost, curr.iter + 1);
        }
    }
}
