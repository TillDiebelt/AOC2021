using System;
using System.Diagnostics;

namespace AOC2021
{
    class Program
    {
        public static int day = 0;

        static void Main(string[] args)
        {
            long runtimePart1 = SolvePart1();
            long runtimePart2 = SolvePart2();
            Console.WriteLine("Solution for day " + day + " part 1 took: " + runtimePart1 + " ms");
            Console.WriteLine("Solution for day " + day + " part 2 took: " + runtimePart2 + " ms");
        }

        private static long SolvePart1()
        {
            Stopwatch stopwatch = new Stopwatch();
            int result = -1;
            stopwatch.Start();
            stopwatch.Stop();
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(result);
            return stopwatch.ElapsedMilliseconds;
        }

        private static long SolvePart2()
        {
            Stopwatch stopwatch = new Stopwatch();
            int result = -1;
            stopwatch.Start();
            stopwatch.Stop();
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(result);
            return stopwatch.ElapsedMilliseconds;
        }
    }
}
