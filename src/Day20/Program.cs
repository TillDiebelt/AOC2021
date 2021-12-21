using System;

namespace Day20
{
    class Program
    {
        public static int day = 20;
        public static string inputPath = "../../../input/input";
        public static string inputPathTest = "../../../input/inputTest";

        static void Main(string[] args)
        {
            Console.WriteLine("AOC 2021 Day " + day);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(Solver.Solve(inputPath, 2));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(Solver.Solve(inputPath, 50));

            Console.WriteLine("\nTests:");
            var test1 = Solver.Solve(inputPathTest, 2);
            Console.WriteLine(test1);
            if (test1 == -1) Console.WriteLine("test 1 successful"); else Console.WriteLine("test 1 failed");
            var test2 = Solver.Solve(inputPathTest, 50);
            Console.WriteLine(test2);
            if (test2 == -1) Console.WriteLine("test 2 successful"); else Console.WriteLine("test 2 failed");
            Console.ReadLine();
        }
	}
}
