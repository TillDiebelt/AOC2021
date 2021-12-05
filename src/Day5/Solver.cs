using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TillSharp.Math.Array;
using TillSharp.Math.Vectors;

namespace Day5
{
    public class Solver
    {
        public static long Part1(string inputPath)
        {
            long result = 0;
            List<Line> lines = ParseDay5(inputPath);

            int[,] field = new int[1100, 1100];

            foreach (var line in lines)
                line.Draw(field);

            result = field.Cast<int>().Where(x => x >= 2).Count();
            return result;
        }

        public static long Part2(string inputPath)
        {
            long result = 0;
            List<Line> lines = ParseDay5(inputPath);

            int[,] field = new int[1100, 1100];

            foreach (var line in lines)
                line.Draw(field, true);

            result = field.Cast<int>().Where(x => x >= 2).Count();
            return result;
        }

        private static List<Line> ParseDay5(string path)
        {
            string input = File.ReadAllText(path).Replace("\r", "");
            var lineInput = input.Split('\n').ToList();
            List<Line> lines = new List<Line>();
            foreach (var line in lineInput)
                lines.Add(new Line(line));
            return lines;
        }
    }
}
