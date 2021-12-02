using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TillSharp.Extenders.Collections;
using TillSharp.Math.Array;
using TillSharp.Math.ArrayExtender;
using TillSharp.Math.Parser;
using TillSharp.Math.Vectors;

namespace Day3
{
    class Program
    {
        public static int day = 3;
        public static string inputPath = "../../../input/input";

        static void Main(string[] args)
        {
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("                                       AOC 2021 Day " + day + "                                  ");
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("Solution Part 1:");
            Console.WriteLine(SolvePart1(inputPath));
            Console.WriteLine("Solution Part 2:");
            Console.WriteLine(SolvePart2(inputPath));
            Console.ReadLine();
        }

        private static long SolvePart1(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath);

            //parsing
            var split = input.Split("\n");

            //calculation          
            return result;
        }

        private static long SolvePart2(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath);

            //parsing
            var split = input.Split("\n");

            //calculation          
            return result;
        }
    }
}
