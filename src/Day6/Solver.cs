using System;
using System.IO;
using System.Linq;

namespace Day6
{
    public class Solver
    {
        public static long Solve(string inputPath, int cycles)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var fishies = input.Split(',').ToList().Select(x => Convert.ToInt64(x)).GroupBy(i => i).ToDictionary(g => g.Key, g => Convert.ToInt64(g.Count()));

            for (int i = 0; i < cycles; i++)
            {
                fishies = fishies.Select(x => (x.Key - 1, x.Value)).ToDictionary(d => d.Item1, d => d.Value);
                fishies.TryAdd(6, 0);
                fishies.TryAdd(-1, 0);
                fishies.Add(8, fishies[-1]);
                fishies[6] += fishies[-1];
                fishies.Remove(-1);
            }

            long result = 0;
            foreach (var ageGroup in fishies)
                result += ageGroup.Value;

            return result;
        }
    }
}
