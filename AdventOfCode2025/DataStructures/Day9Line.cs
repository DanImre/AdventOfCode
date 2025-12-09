using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Day9Line
    {
        // the left point when horizontal, the bottom point when vertical
        long[] StartCoords { get; set; }
        long[] EndCoords { get; set; }
        public Day9Line(long[] startCoords, long[] endCoords)
        {
            // Wrong order
            if (endCoords[0] < startCoords[0] // Horizontal line
                || endCoords[1] < startCoords[1]) // Vertical line
            {
                StartCoords = endCoords;
                EndCoords = startCoords;
                return;
            }

            // Arrived in the correct order
            StartCoords = startCoords;
            EndCoords = endCoords;
        }

        // Doesn't check for edge collisions
        public bool IntersectsWithRect(long[] rectStart, long[] rectEnd)
        {
            long minX = Math.Min(rectStart[0], rectEnd[0]);
            long maxX = Math.Max(rectStart[0], rectEnd[0]);
            long minY = Math.Min(rectStart[1], rectEnd[1]);
            long maxY = Math.Max(rectStart[1], rectEnd[1]);

            long[] bottomLeftPoint = [minX, minY];
            long[] topRightPoint = [maxX, maxY];

            bool xIntersects = minX < StartCoords[0] && StartCoords[0] < maxX
                    || minX < EndCoords[0] && EndCoords[0] < maxX
                    || StartCoords[0] < minX && maxX <= EndCoords[0]
                    || StartCoords[0] <= minX && maxX < EndCoords[0];

            bool yIntersects = minY < StartCoords[1] && StartCoords[1] < maxY
                    || minY < EndCoords[1] && EndCoords[1] < maxY
                    || StartCoords[1] < minY && maxY <= EndCoords[1]
                    || StartCoords[1] <= minY && maxY < EndCoords[1];

            return xIntersects && yIntersects;
        }
    }
}
