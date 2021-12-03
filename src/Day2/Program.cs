using System;
using System.IO;

namespace Day2
{
    class Program
    {
        public static int day = 2;
        public static string inputPath = "../../../input/input";

        static void Main(string[] args)
        {
            Console.WriteLine("AOC 2021 Day " + day);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(Solver.Part1(inputPath));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(Solver.Part2(inputPath));
            Console.ReadLine();
        }
    }
}
