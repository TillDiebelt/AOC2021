using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TillSharp.Extenders;

namespace Day3
{
    public class Solver
    {
        private static int bits = 12;

        public static long Part1(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath);

            var cleanedInput = input.Replace("\n", "").Replace("\r", "");

            var KeyPairs = cleanedInput.Select((x, i) => new KeyValuePair<int, char>(i % (bits), x))
                .GroupBy(p => new { p.Key, p.Value })
                .Select(g => new
                {
                    KeyValuePair = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(k => k.KeyValuePair.Key);

            var epsilon = KeyPairs
                .Where(e =>
                    e.Count ==
                        KeyPairs
                        .Where(c => e.KeyValuePair.Key == c.KeyValuePair.Key)
                        .OrderByDescending(value => value.Count)
                        .FirstOrDefault().Count)
                .Select(x => x.KeyValuePair.Value)
                .Aggregate("", (string res, char current) => res + current);

            var gamma = KeyPairs
                .Where(e =>
                    e.Count ==
                        KeyPairs
                        .Where(c => e.KeyValuePair.Key == c.KeyValuePair.Key)
                        .OrderBy(value => value.Count)
                        .FirstOrDefault().Count)
                .Select(x => x.KeyValuePair.Value)
                .Aggregate("", (string res, char current) => res + current);

            result = Convert.ToInt64(gamma, 2) * Convert.ToInt64(epsilon, 2);

            return result;
        }

        public static long Part2(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var binaries = input.Split('\n').ToList();

            var oxy = Search(binaries, '1');
            var co2 = Search(binaries, '0');
            result = Convert.ToInt64(oxy, 2) * Convert.ToInt64(co2, 2);
            return result;
        }

        private static string Search(List<string> binaries, char commonDenominator)
        {
            for (int i = 0; i < binaries[0].Length; i++)
            {
                var countSearched = binaries.Where(x => x[i] == commonDenominator).Count();
                var rest = binaries.Count - countSearched;

                char mostCommon = countSearched == rest ? commonDenominator : countSearched > rest ? '1' : '0';

                binaries = binaries.Where(x => mostCommon == x[i]).ToList();
                if (binaries.Count == 1)
                    return binaries[0];
            }
            return binaries.FirstOrDefault();
        }
    }
}
