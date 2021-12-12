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
using System.Diagnostics;

namespace Day11
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            var field = Parse(inputPath);

            long result = 0;

            for (int i = 0; i < 100; i++)
            {
                bool[,] flashed = new bool[field.GetUpperBound(0)+1, field.GetUpperBound(1)+1];
                field = Add1(field);
                while (field.Cast<int>().Where(x => x > 9).Count() > 0)
                {
                    for (int y = 0; y <= field.GetUpperBound(0); y++)
                        for (int x = 0; x <= field.GetUpperBound(1); x++)
                        {
                            if (field[y, x] > 9 && !flashed[y, x])
                            {
                                field[y, x] = 0;
                                flashed[y, x] = true;
                                for (int ix = -1; ix < 2; ix++)
                                    for (int jy = -1; jy < 2; jy++)
                                    {
                                        if((ix != 0 || jy != 0) 
                                            && y+jy >= 0 
                                            && y+jy <= field.GetUpperBound(0)
                                            && x + ix >= 0 
                                            && x + ix <= field.GetUpperBound(1)
                                            && !flashed[y+jy,x+ix])
                                            field[y + jy, x + ix]++;
                                    }
                            }
                        }
                }
                result += flashed.Cast<bool>().Where(x => x == true).Count();
            }

            return result;
        }

        public static long SolvePart2(string inputPath)
        {
            var field = Parse(inputPath);

            for (int i = 0; i < 1000; i++)
            {
                bool[,] flashed = new bool[field.GetUpperBound(0) + 1, field.GetUpperBound(1) + 1];
                field = Add1(field);
                while (field.Cast<int>().Where(x => x > 9).Count() > 0)
                {
                    for (int y = 0; y <= field.GetUpperBound(0); y++)
                        for (int x = 0; x <= field.GetUpperBound(1); x++)
                        {
                            if (field[y, x] > 9 && !flashed[y, x])
                            {
                                field[y, x] = 0;
                                flashed[y, x] = true;
                                for (int ix = -1; ix < 2; ix++)
                                    for (int jy = -1; jy < 2; jy++)
                                    {
                                        if ((ix != 0 || jy != 0)
                                            && y + jy >= 0
                                            && y + jy <= field.GetUpperBound(0)
                                            && x + ix >= 0
                                            && x + ix <= field.GetUpperBound(1)
                                            && !flashed[y + jy, x + ix])
                                            field[y + jy, x + ix]++;
                                    }
                            }
                        }
                }
                if (flashed.Cast<bool>().Where(x => x == true).Count() == (field.GetUpperBound(0) + 1) * (field.GetUpperBound(1) + 1)) return i + 1; 
            }

            return 0;
        }

        private static int[,] Add1(int[,] field)
        {
            for (int y = 0; y <= field.GetUpperBound(0); y++)
                for (int x = 0; x <= field.GetUpperBound(1); x++)
                {
                    field[y, x] = field[y, x] + 1;
                }
            return field;
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
    }
}