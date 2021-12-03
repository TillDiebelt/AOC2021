using System;
using System.IO;

namespace Day1
{
    class Program
    {
        public static int day = 1;
        public static string inputPath = "../../../input/input";

        static void Main(string[] args)
        {
            Console.WriteLine("AOC 2021 Day " + day);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(Solver.Solve(inputPath, 1));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(Solver.Solve(inputPath, 3));
            Console.ReadLine();
        }
    }
}
