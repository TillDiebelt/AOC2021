using System;

namespace Day14
{
    class Program
    {
        public static int day = 14;
        public static string inputPath = "../../../input/input";
        public static string inputPathTest = "../../../input/inputTest";

        static void Main(string[] args)
        {
            Console.WriteLine("AOC 2021 Day " + day);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(Solver.SolvePart1(inputPath));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(Solver.Solve(inputPath, 40));

            Console.WriteLine("\nTests:");
            var test1 = Solver.SolvePart1(inputPathTest);
            Console.WriteLine(test1);
            if (test1 == 1588) Console.WriteLine("test 1 successful"); else Console.WriteLine("test 1 failed");
            var test2 = Solver.Solve(inputPathTest, 40);
            Console.WriteLine(test2);
            if (test2 == 2188189693529) Console.WriteLine("test 2 successful"); else Console.WriteLine("test 2 failed");
            Console.ReadLine();
        }
	}
}
