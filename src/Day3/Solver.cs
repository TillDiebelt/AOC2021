using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    public class Solver
    {
        public static long Part1(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath);

            var binaries = input.Split('\n');

            string gamma = "";
            string epsilon = "";
            for (int i = 0; i < binaries[0].Length - 1; i++)
            {
                var countSearched = binaries.Where(x => x[i] == '1').Count();
                var rest = binaries.Length - countSearched;

                gamma += countSearched > rest ? '1' : '0';
                epsilon += countSearched < rest ? '1' : '0';
            }

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