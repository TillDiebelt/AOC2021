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

namespace Day13
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            var tupels = lines[0].Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => (Convert.ToInt32(x.Replace(" ","").Split(",")[0]), Convert.ToInt32(x.Replace(" ", "").Split(",")[1])));

            var firstfold = lines[1].Split('\n')[0].Split(" ")[2].Split('=');

            string folddir = firstfold[0];
            int foldline = Convert.ToInt32(firstfold[1]);
            if (folddir == "y")
            {
                tupels = tupels.Map(x => x.Item2 > foldline ?
                (x.Item1, (foldline - (x.Item2 - foldline))) : x);
            }
            else
            {
                tupels = tupels.Map(x => x.Item1 > foldline ?
                (foldline - (x.Item1 - foldline), x.Item2) : x);
            }

            return tupels.Cast<(int,int)>().Distinct().Count(); 
        }

        public static long SolvePart2(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            var points = lines[0].Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => (Convert.ToInt32(x.Replace(" ", "").Split(",")[0]), Convert.ToInt32(x.Replace(" ", "").Split(",")[1])));

            var folds = lines[1].Split('\n');

            foreach (var foldInst in folds)
            {
                var fold = foldInst.Split(" ")[2].Split('=');
                string folddir = fold[0];
                int foldline = Convert.ToInt32(fold[1]);
                if (folddir == "y")
                {
                    points = points.Map(x => x.Item2 > foldline ?
                    (x.Item1 , (foldline - (x.Item2 - foldline))) : x);
                }
                else
                {
                    points = points.Map(x => x.Item1 > foldline ?
                    (foldline - (x.Item1 - foldline), x.Item2) : x);
                }
            }

            (int, int) max = points.Reduce((res, cur) => (cur.Item1 > res.Item1 ? cur.Item1 : res.Item1 , cur.Item2 > res.Item2 ? cur.Item2 : res.Item2));
            bool[,] map = new bool[max.Item2+1, max.Item1+1];

            foreach (var point in points)
            {
                map[point.Item2, point.Item1] = true;
            }

            for (int i = 0; i <= max.Item2; i++)
            {
                for (int j = 0; j <= max.Item1; j++)
                {
                    Console.Write(map[i, j] ? "#" : " ");
                }
                Console.WriteLine();
            }
            return map.Cast<bool>().Where(x => x == true).Count();
        }
    }
}
