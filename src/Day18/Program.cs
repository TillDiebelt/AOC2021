﻿using System;

namespace Day18
{
    class Program
    {
        public static int day = 18;
        public static string inputPath = "../../../input/input";
        public static string inputPathTest = "../../../input/inputTest";

        static void Main(string[] args)
        {
            Console.WriteLine("AOC 2021 Day " + day);
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(UberSolver.SolvePart1(inputPath));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(UberSolver.SolvePart2(inputPath));

            Console.WriteLine("\nTests:");
            var test1 = UberSolver.SolvePart1(inputPathTest);
            Console.WriteLine(test1);
            if (test1 == -1) Console.WriteLine("test 1 successful"); else Console.WriteLine("test 1 failed");
            var test2 = UberSolver.SolvePart2(inputPathTest);
            Console.WriteLine(test2);
            if (test2 == -1) Console.WriteLine("test 2 successful"); else Console.WriteLine("test 2 failed");
            Console.ReadLine();
        }
	}
}