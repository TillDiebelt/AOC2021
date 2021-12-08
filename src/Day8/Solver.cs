using System;
using System.IO;
using System.Linq;
using TillSharp.Extenders.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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

            var displays = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Map(x => SevenSegment(x));

            var result = displays.Reduce(0, (long sum, int display) => sum + display);
            return result;
        }

        public static int SevenSegment(string input)
        {
            var sides = input.Split('|');
            var right = sides[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var counts = sides[0].GroupBy(x => x).Map(x => (x.Key, x.Count())).ToDictionary(t => t.Key, t => t.Item2);

            return right.Select((digit, i) => (digit.Reduce(0, (int value, char current) => value + counts[current]), i))
                .Reduce(0, (result, current) =>
                     result + (encoder[current.Item1] * (int)Math.Pow(10, 3 - current.i)));
        }

        private static Dictionary<int, int> encoder = new Dictionary<int, int>() {
            { 42, 0 },
            { 17, 1 },
            { 34, 2 },
            { 39, 3 },
            { 30, 4 },
            { 37, 5 },
            { 41, 6 },
            { 25, 7 },
            { 49, 8 },
            { 45, 9 }};
    }
}
