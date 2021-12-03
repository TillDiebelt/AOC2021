using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        public static int day = 4;
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
