using System;
using System.IO;
using System.Linq;
using TillSharp.Math.Parser;
using TillSharp.Math.Functions;
using TillSharp.Math.Vectors;
using TillSharp.Math.Array;
using TillSharp.Math.ArrayExtender;
using TillSharp.Math;
using TillSharp.Extenders.Collections;
using TillSharp.Extenders.String;
using TillSharp.Extenders.Numerical;
using System.Collections.Generic;

namespace Day0
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            //var tupels = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => (Convert.ToInt32(x.Replace(" ","").Split("->")[0]), Convert.ToInt32(x.Replace(" ", "").Split("->")[1])));
            //var ints = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x));
            //var x = lines.Map(x => x).Reduce((x,y) => x + y);

            long result = 0;

            int startIndex = 0;
            foreach (var line in lines)
                result += (long)Parser.LoadAndCalculate(line, ref startIndex);

            return result;
        }

        public static long SolvePart2(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n');

            long result = 0;



            return result;
        }
    }

    public static class Extenders
    {
        public static int ToDigit(this char self)
        {
            return self - 48;
        }

        public static IEnumerable<(int x, int y)> Neighboars<T>(this T[][] self, int x, int y)
        {
            List<(int x, int y)> neighboars = new List<(int x, int y)>();
            if (x - 1 >= 0) neighboars.Add((x - 1, y));
            if (x + 1 < self[y].Length) neighboars.Add((x + 1, y));
            if (y - 1 >= 0) neighboars.Add((x, y - 1));
            if (y + 1 < self.Length) neighboars.Add((x, y + 1));
            return neighboars;
        }

        public static IEnumerable<(int x, int y)> Neighboars<T>(this T[,] self, int x, int y)
        {
            List<(int x, int y)> neighboars = new List<(int x, int y)>();
            if (x - 1 >= 0) neighboars.Add((x - 1, y));
            if (x + 1 <= self.GetUpperBound(1)) neighboars.Add((x + 1, y));
            if (y - 1 >= 0) neighboars.Add((x, y - 1));
            if (y + 1 <= self.GetUpperBound(0)) neighboars.Add((x, y + 1));
            return neighboars;
        }

        public static IEnumerable<(int x, int y)> NeighboarsDiag<T>(this T[][] self, int x, int y)
        {
            List<(int x, int y)> neighboars = new List<(int x, int y)>();
            if (x - 1 >= 0)
            {
                neighboars.Add((x - 1, y));
                if (y - 1 >= 0) neighboars.Add((x - 1, y - 1));
                if (y + 1 < self.Length) neighboars.Add((x - 1, y + 1));
            }
            if (x + 1 < self[y].Length)
            {
                neighboars.Add((x + 1, y));
                if (y - 1 >= 0) neighboars.Add((x + 1, y - 1));
                if (y + 1 < self.Length) neighboars.Add((x + 1, y + 1));
            }
            if (y - 1 >= 0) neighboars.Add((x, y - 1));
            if (y + 1 < self[y].Length) neighboars.Add((x, y + 1));
            return neighboars;
        }

        public static IEnumerable<(int x, int y)> NeighboarsDiag<T>(this T[,] self, int x, int y)
        {
            List<(int x, int y)> neighboars = new List<(int x, int y)>();
            if (x - 1 >= 0)
            {
                neighboars.Add((x - 1, y));
                if (y - 1 >= 0) neighboars.Add((x - 1, y - 1));
                if (y + 1 <= self.GetUpperBound(0)) neighboars.Add((x - 1, y + 1));
            }
            if (x + 1 <= self.GetUpperBound(1))
            {
                neighboars.Add((x + 1, y));
                if (y - 1 >= 0) neighboars.Add((x + 1, y - 1));
                if (y + 1 <= self.GetUpperBound(0)) neighboars.Add((x + 1, y + 1));
            }
            if (y - 1 >= 0) neighboars.Add((x, y - 1));
            if (y + 1 <= self.GetUpperBound(0)) neighboars.Add((x, y + 1));
            return neighboars;
        }

        public static T[,] MapApply<T>(this T[,] self, Func<T, T> func)
        {
            for (int y = 0; y <= self.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= self.GetUpperBound(1); x++)
                {
                    self[y, x] = func(self[y, x]);
                }
            }
            return self;
        }

        public static T[][] MapApply<T>(this T[][] self, Func<T, T> func)
        {
            for (int y = 0; y <= self.GetUpperBound(0); y++)
            {
                for (int x = 0; x < self[y].Length; x++)
                {
                    self[y][x] = func(self[y][x]);
                }
            }
            return self;
        }

        public static R[,] Map<T, R>(this T[,] self, Func<T, R> func)
        {
            R[,] result = new R[self.GetUpperBound(0) + 1, self.GetUpperBound(1) + 1];
            for (int y = 0; y <= self.GetUpperBound(0); y++)
            {
                for (int x = 0; x <= self.GetUpperBound(1); x++)
                {
                    result[y, x] = func(self[y, x]);
                }
            }
            return result;
        }

        public static R[][] Map<T, R>(this T[][] self, Func<T, R> func)
        {
            R[][] result = new R[self.GetUpperBound(0) + 1][];
            for (int y = 0; y <= self.GetUpperBound(0); y++)
            {
                result[y] = new R[self[y].Length];
                for (int x = 0; x < self[y].Length; x++)
                {
                    result[y][x] = func(self[y][x]);
                }
            }
            return result;
        }
    }
}
