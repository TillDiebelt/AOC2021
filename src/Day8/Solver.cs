using System;
using System.IO;
using TillSharp.Math.Parser;

namespace Day8
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);            

            long result = 0;

            int startIndex = 0;
            foreach (var line in lines)
                result += (long)Parser.LoadAndCalculate(line, ref startIndex);

            return result;
        }

        public static long SolvePart2(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n');

            long result = 0;



            return result;
        }
    }
}
