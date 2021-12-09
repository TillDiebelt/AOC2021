using System;
using System.IO;
using System.Linq;
using TillSharp.Math.ArrayExtender;
using TillSharp.Extenders.Collections;
using System.Collections.Generic;

namespace Day9
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            int[,] heights = Parse(inputPath);

            long result = 0;

            for (int y = 0; y < heights.Length; y++)
                for (int x = 0; x <= heights.GetUpperBound(1); x++)
                {
                    if (isLocalMin(heights.SliceSquare(y, x, 1, 9)))
                    {
                        result += heights[y, x] + 1;
                    }
                }
            
            return result;
        }

        public static long SolvePart2(string inputPath)
        {
            int[,] heights = Parse(inputPath);

            List<int> basins = new List<int>();

            for (int y = 0; y < heights.Length; y++)
                for (int x = 0; x <= heights.GetUpperBound(1); x++)
                {
                    if (isLocalMin(heights.SliceSquare(y, x, 1, 9)))
                    {
                        basins.Add(countBasinSize(heights, x, y));
                    }
                }

            long result = basins.OrderByDescending(x => x).Take(3).Reduce(1, (res, current) => res * current);
            return result;
        }

        private static int[,] Parse(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int[,] arr = new int[lines.Length, lines[0].Length];

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                {
                    arr[y, x] = Convert.ToInt32(lines[y][x] + "");
                }
            return arr;
        }

        private static bool isLocalMin(int[,] sqaure)
        {
            var q = sqaure[1, 1];
            bool min = true;
            min &= sqaure[0, 1] > q;
            min &= sqaure[1, 0] > q;
            min &= sqaure[2, 1] > q;
            min &= sqaure[1, 2] > q;
            return min;
        }

        private static int countBasinSize(int[,] arr, int x, int y)
        {
            bool[,] seen = new bool[arr.GetUpperBound(0)+1, arr.GetUpperBound(1) + 1];
            int result = 0;
            List<(int, int)> queue = new List<(int, int)>();
            queue.Add((x, y));
            while (queue.Count > 0)
            {
                var point = queue[0];
                queue.RemoveAt(0);
                if (seen[point.Item2, point.Item1])
                    continue;
                if (arr[point.Item2, point.Item1] == 9)
                    continue;
                seen[point.Item2, point.Item1] = true;
                if(arr.GetUpperBound(1) + 1 > point.Item1 + 1)
                    queue.Add((point.Item1+1, point.Item2));
                if (arr.GetUpperBound(0) + 1 > point.Item2 + 1)
                    queue.Add((point.Item1, point.Item2+1));
                if (point.Item1 - 1 >= 0)
                    queue.Add((point.Item1-1, point.Item2));
                if ( point.Item2 -1 >= 0)
                    queue.Add((point.Item1, point.Item2-1));
                result++;
            }
            return result;
        }
    }
}
