using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TillSharp.Extenders.Collections;
using TillSharp.Math.Array;
using TillSharp.Math.ArrayExtender;
using TillSharp.Math.Parser;
using TillSharp.Math.Vectors;

namespace Day1
{
    class Program
    {
        public static int day = 1;
        public static string inputPath = "../../../input/input";

        static void Main(string[] args)
        {
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("                                       AOC 2021 Day " + day + "                                  ");
            Console.WriteLine("=================================================================================================");
            (long parsing, long calculation, int solution) runtimePart1 = SolveBoth(inputPath, 1);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(runtimePart1.solution);
            Console.WriteLine("Solution for day " + day + " part 1 took: " + runtimePart1.parsing + " ticks to parse and " 
                + runtimePart1.calculation + " ticks to calculate");
            Console.WriteLine();

            (long parsing, long calculation, int solution) runtimePart2 = SolveBoth(inputPath, 3);
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(runtimePart2.solution);
            Console.WriteLine("Solution for day " + day + " part 1 took: " + runtimePart2.parsing + " ticks to parse and " 
                + runtimePart2.calculation + " ticks to calculate");

        }

        private static (long parsing, long calculation, int solution) SolveBoth(string inputPath, int distance)
        {
            Stopwatch stopwatch = new Stopwatch();
            int result = 0;
            string input = File.ReadAllText(inputPath);
            stopwatch.Start();

            //parsing
            int[] depths = Array.ConvertAll(input.Split("\n"), int.Parse);

            stopwatch.Stop();
            long parseTime = stopwatch.ElapsedTicks;
            stopwatch.Restart();

            //calculation
            for (int i = 0; i < depths.Length - distance; i++)
            {
                result += depths[i] < depths[i + distance] ? 1 : 0;
            }

            stopwatch.Stop();
            return (parseTime, stopwatch.ElapsedTicks, result);
        }
    }
}
