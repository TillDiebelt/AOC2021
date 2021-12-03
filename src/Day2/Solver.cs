using System;
using System.Collections.Generic;
using System.IO;

namespace Day2
{
    public class Solver
    {
        private const int FIRST_CHAR = 0;
        private const int DIRECTION = 0;

        public static long Part1(string inputPath)
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
                switch (pair[DIRECTION][FIRST_CHAR])
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

        public static long Part2(string inputPath)
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
                switch (pair[DIRECTION][FIRST_CHAR])
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
