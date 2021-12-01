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

        static void Main(string[] args)
        {
            (long parsing, long calculation) runtimePart1 = SolvePart1();
            Console.WriteLine("Solution for day " + day + " part 1 took: " + runtimePart1.parsing + " ticks to parse and " 
                + runtimePart1.calculation + " ticks to calculate");
            Console.WriteLine();
            (long parsing, long calculation) runtimePart2 = SolvePart2();
            Console.WriteLine("Solution for day " + day + " part 1 took: " + runtimePart2.parsing + " ticks to parse and " 
                + runtimePart2.calculation + " ticks to calculate");

        }

        private static (long parsing, long calculation) SolvePart1()
        {
            Stopwatch stopwatch = new Stopwatch();
            int result = 0;
            string input = File.ReadAllText("../../../input/inputP1");
            stopwatch.Start();
            int[] depths = Array.ConvertAll(input.Split("\n"), int.Parse);
            stopwatch.Stop();
            long parseTime = stopwatch.ElapsedTicks;
            stopwatch.Restart();
            for (int i = 1; i < depths.Length; i++)
            {
                result += depths[i - 1] < depths[i] ? 1 : 0;
            }
            stopwatch.Stop();
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(result);
            return (parseTime,stopwatch.ElapsedTicks);
        }

        private static (long parsing, long calculation) SolvePart2()
        {
            Stopwatch stopwatch = new Stopwatch();
            int result = 0;
            string input = File.ReadAllText("../../../input/inputP1");
            stopwatch.Start();
            int[] depths = Array.ConvertAll(input.Split("\n"), int.Parse);
            stopwatch.Stop();
            long parseTime = stopwatch.ElapsedTicks;
            stopwatch.Restart();
            for (int i = 0; i < depths.Length-3; i++)
            {
                result += (depths[i..(i + 3)].Sum() < depths[(i + 1)..(i + 4)].Sum()) ? 1 : 0;
            }
            stopwatch.Stop();
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(result);
            return (parseTime, stopwatch.ElapsedTicks);
        }
    }
}
