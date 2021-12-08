using System;

namespace Day8
{
    class Program
    {
        public static int day = 8;
        public static string inputPath = "../../../input/input";
        public static string inputPathT = "../../../input/inputTest";

        static void Main(string[] args)
        {
            Console.WriteLine("AOC 2021 Day " + day);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(Solver.SolvePart1(inputPath));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(Solver.SolvePart2(inputPath));
            Console.ReadLine();
        }
	}
}
