using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day6
{
    public class Solver
    {
        public static long Solve(string inputPath, int cycles)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var inputGeneration = input.Split(',').Select(x => Convert.ToInt32(x)).GroupBy(i => i).ToDictionary(g => g.Key, g => Convert.ToInt64(g.Count()));
            List<long> fishies = new List<long>(){0, 0, 0, 0, 0, 0, 0, 0, 0};
            foreach (var fish in inputGeneration)
                fishies[fish.Key] += fish.Value;

            for (int i = 0; i < cycles; i++)
            {
                fishies.Add(fishies[0]);
                fishies[7] += fishies[0];
                fishies.RemoveAt(0);
            }

            long result = 0;
            foreach (var ageGroup in fishies)
                result += ageGroup;

            return result;
        }
    }
}
