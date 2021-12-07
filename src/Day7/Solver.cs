using System;
using System.IO;
using System.Linq;

namespace Day7
{
    public class Solver
    {
        public static long Solve(string inputPath, Func<int, int> cost)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var crabs = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x));

            int maxDistance = crabs.Max();
            long[] costs = new long[maxDistance];

            foreach (var crab in crabs)
                costs = costs.Select((x, i) => x + cost(Math.Abs(i - crab))).ToArray();

            return costs.Min();
        }

        public static long SolvePart1Alternitive(string inputPath, Func<int, int> cost)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var crabs = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x));
            int count = crabs.Count();
            var ordered = crabs.OrderBy(p => p);
            float median = ordered.ElementAt(count / 2) + ordered.ElementAt((count - 1) / 2);
            median /= 2;
            var index = (int)Math.Floor(median);
            long result = 0;
            foreach (var crab in crabs)
                result += cost(Math.Abs(index - crab));
            return result;
        }

        public static long SolvePart2Alternitive(string inputPath, Func<int, int> cost)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var crabs = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x));
            var index = (int)Math.Floor(crabs.Average());
            long result = 0;
            foreach (var crab in crabs)
                result += cost(Math.Abs(index - crab));
            return result;
        }
    }
}
