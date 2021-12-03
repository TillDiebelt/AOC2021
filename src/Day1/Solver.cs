using System;
using System.Collections.Generic;
using System.IO;

namespace Day1
{
    public class Solver
    {
        public static int Solve(string inputPath, int distance)
        {
            int result = 0;
            string input = File.ReadAllText(inputPath);

            //parsing
            int[] depths = Array.ConvertAll(input.Split("\n"), int.Parse);

            //calculation
            for (int i = 0; i < depths.Length - distance; i++)
            {
                result += depths[i] < depths[i + distance] ? 1 : 0;
            }

            return result;
        }
    }
}
