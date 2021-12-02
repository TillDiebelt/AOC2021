using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TillSharp.Extenders.Collections;
using TillSharp.Math.Array;
using TillSharp.Math.ArrayExtender;
using TillSharp.Math.Parser;
using TillSharp.Math.Vectors;

namespace Day1
{
    class Program
    {
        public static int day = 2;
        public static string inputPath = "../../../input/input";

        static void Main(string[] args)
        {
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("                                       AOC 2021 Day " + day + "                                  ");
            Console.WriteLine("=================================================================================================");
            (long parsing, long calculation, long solution) runtimePart1 = SolvePart1(inputPath);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(runtimePart1.solution);
            double ns1 = 1000000000.0 * (double)runtimePart1.calculation / Stopwatch.Frequency;
            double pns1 = 1000000000.0 * (double)runtimePart1.parsing / Stopwatch.Frequency;
            Console.WriteLine("Solution for day " + day + " part 1 took: " + pns1 + " ns to parse and "
                + ns1 + " ns to calculate");
            Console.WriteLine();

            (long parsing, long calculation, long solution) runtimePart2 = SolvePart2(inputPath);
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(runtimePart2.solution);
            double ns2 = 1000000000.0 * (double)runtimePart2.calculation / Stopwatch.Frequency;
            double pns2 = 1000000000.0 * (double)runtimePart2.parsing / Stopwatch.Frequency;
            Console.WriteLine("Solution for day " + day + " part 1 took: " + pns2 + " ns to parse and "
                + ns2 + " ns to calculate");
            Console.ReadLine();
        }

        private static (long parsing, long calculation, long solution) SolvePart1(string inputPath)
        {
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();
            long result = 0;
            string input = File.ReadAllText(inputPath);
            stopwatch.Start();

            //parsing
            var movements = input.Split("\n");

            stopwatch.Stop();
            long parseTime = stopwatch.ElapsedTicks;
            stopwatch.Restart();

            //calculation
            int forward = 0;
            int depth = 0;
            foreach (var movement in movements)
            {
                var pair = movement.Split(' ');
                switch (pair[0][0])
                {
                    case 'f':
                        forward += Convert.ToInt32(pair[1]);
                        break;
                    case 'd':
                        depth += Convert.ToInt32(pair[1]);
                        break;
                    case 'u':
                        depth -= Convert.ToInt32(pair[1]);
                        break;
                }
            }

            result = forward * depth;

            stopwatch.Stop();
            return (parseTime, stopwatch.ElapsedTicks, result);
        }

        private static (long parsing, long calculation, long solution) SolvePart2(string inputPath)
        {
            Stopwatch stopwatch = new Stopwatch();
            Stopwatch stopwatch2 = new Stopwatch();
            long result = 0;
            string input = File.ReadAllText(inputPath);
            stopwatch.Start();

            //parsing
            var movements = input.Split("\n");

            stopwatch.Stop();
            long parseTime = stopwatch.ElapsedTicks;
            stopwatch.Restart();

            //calculation
            int aim = 0;
            int horizontal = 0;
            int depth = 0;
            foreach (var movement in movements)
            {
                var pair = movement.Split(' ');
                switch (pair[0][0])
                {
                    case 'f':
                        depth += Convert.ToInt32(pair[1]) * aim;
                        horizontal += Convert.ToInt32(pair[1]);
                        break;
                    case 'd':
                        aim += Convert.ToInt32(pair[1]);
                        break;
                    case 'u':
                        aim -= Convert.ToInt32(pair[1]);
                        break;
                }
            }

            result = horizontal * depth;

            stopwatch.Stop();
            return (parseTime, stopwatch.ElapsedTicks, result);
        }
    }
}
