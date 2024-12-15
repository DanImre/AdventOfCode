using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class RestroomRobot
    {
        public (int x, int y) Pos { get; set; } = (0, 0);
        public (int x, int y) Velocity { get; set; } = (0, 0);
        public RestroomRobot((int x, int y) pos, (int x, int y) velocity)
        {
            Pos = pos;
            Velocity = velocity;
        }
    }
}
