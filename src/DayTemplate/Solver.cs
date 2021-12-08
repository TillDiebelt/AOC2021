using System;
using System.IO;
using System.Linq;
using TillSharp.Math.Parser;

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
}
