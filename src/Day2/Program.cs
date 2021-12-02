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
            var movements = input.Split("\n");

            //calculation
            int forward = 0;
            int depth = 0;
            foreach (var movement in movements)
            {
                var pair = movement.Split(' ');
                switch (pair[0][0])
                {
                    case 'f':
                        forward += Convert.ToInt32(pair[1]);
                        break;
                    case 'd':
                        depth += Convert.ToInt32(pair[1]);
                        break;
                    case 'u':
                        depth -= Convert.ToInt32(pair[1]);
                        break;
                }
            }
            
            result = forward * depth;

            return result;
        }

        private static long SolvePart2(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath);

            //parsing
            var movements = input.Split("\n");

            //calculation
            int aim = 0;
            int horizontal = 0;
            int depth = 0;
            foreach (var movement in movements)
            {
                var pair = movement.Split(' ');
                switch (pair[0][0])
                {
                    case 'f':
                        depth += Convert.ToInt32(pair[1]) * aim;
                        horizontal += Convert.ToInt32(pair[1]);
                        break;
                    case 'd':
                        aim += Convert.ToInt32(pair[1]);
                        break;
                    case 'u':
                        aim -= Convert.ToInt32(pair[1]);
                        break;
                }
            }

            result = horizontal * depth;
            return result;
        }
    }
}
