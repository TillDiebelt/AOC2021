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

namespace Day16
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var hex = input.Map(x => int.Parse(x.ToString(), System.Globalization.NumberStyles.HexNumber));

            long result = 0;

            string binaries = "";
            for (int i = 0; i < hex.Count(); i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if ((hex.ElementAt(i)&(int)Math.Pow(2, j)) == Math.Pow(2, j)) binaries += "1"; else binaries += "0"; 
                }
            }
            
            result = PackageFactory.GeneratePackages(binaries).Aggregate(0, (long res, Package cur) => res + cur.GetValueVersions());

            return result;
        }

        public static long SolvePart2(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var hex = input.Map(x => int.Parse(x.ToString(), System.Globalization.NumberStyles.HexNumber));

            long result = 0;

            string binaries = "";
            for (int i = 0; i < hex.Count(); i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if ((hex.ElementAt(i) & (int)Math.Pow(2, j)) == Math.Pow(2, j)) binaries += "1"; else binaries += "0";
                }
            }

            result = PackageFactory.GeneratePackages(binaries).Aggregate(0, (long res, Package cur) => res + cur.GetValue());

            return result;
        }
    }
}
