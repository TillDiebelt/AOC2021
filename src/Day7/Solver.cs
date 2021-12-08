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
    }
}
