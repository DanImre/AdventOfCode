using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace AdventOfCode2015
{
    public class Day14 : IDay
    {
        public void PartOne(string inputPath)
        {
            List<(string name, int speed, int flighttime, int resttime)> deerList = File.ReadLines(inputPath)
                .Select(x =>
                {
                    string[] spaceSplit = x.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    string name = spaceSplit[0];
                    int speed = int.Parse(spaceSplit[3]);
                    int flighttime = int.Parse(spaceSplit[6]);
                    int resttime = int.Parse(spaceSplit[^2]);

                    return (name, speed, flighttime, resttime);
                })
                .ToList();

            //int timeFrame = 1000; //test
            int timeFrame = 2503;

            int solution = deerList
                .Max(x =>
                {
                    int whole = timeFrame / (x.flighttime + x.resttime);
                    int remainder = timeFrame % (x.flighttime + x.resttime);

                    int travelledDist = whole * x.flighttime * x.speed
                        + Math.Min(remainder, x.flighttime) * x.speed;

                    return travelledDist;
                });

            // The test should be 1120 if the race ended after 1000 seconds
            Console.WriteLine($"The distance that the winning reindeer traveled is: {solution}.");
        }

        // We need to simulate it now
        public void PartTwo(string inputPath)
        {
            List<(string name, int speed, int flighttime, int resttime)> deerList = File.ReadLines(inputPath)
                .Select(x =>
                {
                    string[] spaceSplit = x.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    string name = spaceSplit[0];
                    int speed = int.Parse(spaceSplit[3]);
                    int flighttime = int.Parse(spaceSplit[6]);
                    int resttime = int.Parse(spaceSplit[^2]);

                    return (name, speed, flighttime, resttime);
                })
                .ToList();

            //int timeFrame = 1000; //test
            int timeFrame = 2503;

            Dictionary<string, int> points = [];
            foreach (var item in deerList)
                points[item.name] = 0;

            for (int currentTime = 1; currentTime <= timeFrame; currentTime++)
            {
                int winningDist = 0;
                List<string> winningDeers = [];
                foreach (var deer in deerList)
                {
                    int whole = currentTime / (deer.flighttime + deer.resttime);
                    int remainder = currentTime % (deer.flighttime + deer.resttime);

                    int travelledDist = whole * deer.flighttime * deer.speed
                        + Math.Min(remainder, deer.flighttime) * deer.speed;

                    if (winningDist < travelledDist)
                    {
                        winningDist = travelledDist;
                        winningDeers.Clear();
                    }

                    if (travelledDist == winningDist)
                        winningDeers.Add(deer.name);
                }

                foreach (var deerName in winningDeers)
                    points[deerName]++;
            }

            int solution = points.Values.Max();
            // The test should be 689 if the race ended after 1000 seconds
            Console.WriteLine($"The winning deer has {solution} points.");
        }
    }
}
