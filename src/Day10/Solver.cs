using System;
using System.IO;
using System.Linq;
using TillSharp.Extenders.Collections;
using TillSharp.Extenders.Numerical;
using System.Collections.Generic;

namespace Day10
{
    public class Solver
    {
        private static Dictionary<char, int> corruptetCost = new Dictionary<char, int>() { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        private static Dictionary<char, int> bracketCost = new Dictionary<char, int>() { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
        private static Dictionary<char, char> inverse = new Dictionary<char, char>() { { ')', '(' }, { ']', '[' }, { '}', '{' }, { '>', '<' } };
        private static List<char> openingBrackets = new List<char>() { '(', '<', '{', '[' };

        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            long result = lines.Aggregate(0, (long value, string line) => value + CorruptetLineValue(line));

            return result;
        }

        private static int CorruptetLineValue(string line)
        {
            Stack<char> open = new Stack<char>();
            foreach (var bracket in line)
            {
                if (openingBrackets.Contains(bracket))
                    open.Push(bracket);
                else
                    if (open.Pop() != inverse[bracket])
                        return corruptetCost[bracket];
            }
            return 0;
        }

        public static long SolvePart2(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            List<long> completionCosts = new List<long>();

            foreach (var line in lines)
            {
                Stack<char> open = new Stack<char>();
                bool skip = false;
                foreach (var bracket in line)
                {
                    if (openingBrackets.Contains(bracket))
                        open.Push(bracket);
                    else
                        if (open.Pop() != inverse[bracket])
                        {
                            skip = true;
                            break;
                        }
                }
                if (!skip)
                {
                    long completionCost = 0;
                    while (open.Count > 0)
                    {
                        completionCost *= 5;
                        completionCost += bracketCost[open.Pop()];
                    }
                    completionCosts.Add(completionCost);
                }
            }

            long result = completionCosts.Median();
            return result;
        }
    }
}
