using System;
using System.IO;
using System.Linq;
using TillSharp.Math.Parser;
using TillSharp.Math.Functions;
using TillSharp.Math.Vectors;
using TillSharp.Math.Array;
using TillSharp.Math.ArrayExtender;
using TillSharp.Math;
using TillSharp.Extenders.Collections;
using TillSharp.Extenders.String;
using TillSharp.Extenders.Numerical;
using System.Collections.Generic;

namespace Day8
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var rightEntries = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Map(x => x.Split("|")[1].Split(' ', StringSplitOptions.RemoveEmptyEntries));
            
            List<int> simpleNumbers = new List<int>() { 2, 3, 4, 7 };

            return rightEntries.Reduce(
                0, (long count, string[] numbers) =>
                count + 
                numbers
                    .Map(x => x.Length)
                    .Filter(x => simpleNumbers.Any(y => y ==x))
                    .Count());
        }

        public static long SolvePart2(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var displays = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Map(x => new SevenSegment(x));

            return displays.Reduce(0, (long sum, SevenSegment display) => sum + display.GetValue());
        }
    }
}
