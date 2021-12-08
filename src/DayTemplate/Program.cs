using System;

namespace Day0
{
    class Program
    {
        public static int day = 0;
        public static string inputPath = "../../../input/input";
        public static string inputPathT = "../../../input/inputTest";

        static void Main(string[] args)
        {
            Console.WriteLine("AOC 2021 Day " + day);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(Solver.SolvePart1(inputPath));
            Console.WriteLine(Solver.SolvePart1(inputPathT));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(Solver.SolvePart2(inputPath));
            Console.WriteLine(Solver.SolvePart2(inputPathT));
            Console.ReadLine();
        }
	}
}
