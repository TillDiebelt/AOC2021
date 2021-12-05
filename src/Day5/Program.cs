using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        public static int day = 5;
        public static string inputPath = "../../../input/input";

        static void Main(string[] args)
        {
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(Solver.Solve(inputPath, false));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(Solver.Solve(inputPath, true));
            Console.ReadLine();
        }
	}
}
