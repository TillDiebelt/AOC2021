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
            Console.WriteLine(SolveBoth(inputPath, 1));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(SolveBoth(inputPath, 3));
            Console.ReadLine();
        }

        private static int SolveBoth(string inputPath, int distance)
        {
            int result = 0;
            string input = File.ReadAllText(inputPath);

            //parsing
            int[] depths = Array.ConvertAll(input.Split("\n"), int.Parse);

            //calculation
            for(int i = 0; i < depths.Length - distance; i++)
            {
                result += depths[i] < depths[i + distance] ? 1 : 0;
            }

            return result;
        }
    }
}
