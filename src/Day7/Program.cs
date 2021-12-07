using System;

namespace Day7
{
    class Program
    {
        public static int day = 7;
        public static string inputPath = "../../../input/input";

        static void Main(string[] args)
        {
            Console.WriteLine("AOC 2021 Day " + day);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(Solver.Solve(inputPath, x => x));
            Console.WriteLine(Solver.SolvePart1Alternitive(inputPath, x => (x)));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(Solver.Solve(inputPath, x => (x * (1 + x) / 2)));
            Console.WriteLine(Solver.SolvePart2Alternitive(inputPath, x => (x * (1 + x) / 2)));
            Console.ReadLine();
        }
	}
}
