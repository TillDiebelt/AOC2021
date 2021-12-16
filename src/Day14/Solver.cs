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
using System.Diagnostics;

namespace Day14
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            var rules = lines[1].Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => (x.Replace(" ", "").Split("->")[0], x.Replace(" ", "").Split("->")[1]));

            long result = 0;

            string seed = lines[0];

            for (int i = 0; i < 10; i++)
            {
                string newSeed = seed + "";
                List<(int, string)> todos = new List<(int, string)>();
                int indexshift = 1;
                for (int j = 0; j < seed.Length - 1; j++)
                {
                    foreach (var rule in rules)
                    {
                        if (seed.Substring(j, 2) == rule.Item1)
                        {
                            newSeed = newSeed.Substring(0, j + indexshift) + rule.Item2 + newSeed.Substring(j + indexshift);
                            indexshift++;
                        }
                    }
                }
                seed = newSeed;
            }
            var counts = seed.GroupBy(x => x).Map(x => (x.Key, x.Count())).ToDictionary(t => t.Key, t => t.Item2).ToList().OrderByDescending(x => x.Value);
            result = counts.First().Value - counts.Last().Value;

            return result;
        }

        public static long Solve(string inputPath, int steps)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            var rules = lines[1].Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => (x.Replace(" ", "").Split("->")[0], x.Replace(" ", "").Split("->")[0][0] + x.Replace(" ", "").Split("->")[1] + x.Replace(" ", "").Split("->")[0][1]));
            Dictionary<string, int> RuleToIndex = new Dictionary<string, int>();
            Dictionary<int, string> IndexToRule = new Dictionary<int, string>();
            Dictionary<char, long> LinkedCharCount = lines[1].Replace("\r", "").Replace("\n", "").Replace("-", "").Replace(">", "").Replace(" ", "").GroupBy(x => x).Map(x => (x.Key, (long)0)).ToDictionary(t => t.Key, t => t.Item2);
            int size = rules.Count();
            int index = 0;
            long[,] ruleMatrix = new long[size, size];
            long[,] resultMatrix = new long[size, size];
            long[,] inputVector = new long[size, 1];

            //give Index to each rule (tupel of char)
            foreach (var rule in rules)
            {
                RuleToIndex.Add(rule.Item1, index);
                IndexToRule.Add(index, rule.Item1);
                index++;
            }

            //get a vector of input tupels
            for (int i = 0; i < lines[0].Length - 1; i++)
            {
                inputVector[RuleToIndex[lines[0].Substring(i, 2)], 0]++;
            }

            //create empty solver matrix for each step
            foreach (var rule in rules)
            {
                ruleMatrix[RuleToIndex[rule.Item2.Substring(0, 2)], RuleToIndex[rule.Item1]]++;
                ruleMatrix[RuleToIndex[rule.Item2.Substring(1, 2)], RuleToIndex[rule.Item1]]++;
                resultMatrix[RuleToIndex[rule.Item2.Substring(0, 2)], RuleToIndex[rule.Item1]]++;
                resultMatrix[RuleToIndex[rule.Item2.Substring(1, 2)], RuleToIndex[rule.Item1]]++;
            }

            //result matrix is creates with multipliing with matrix for each step
            for (int i = 1; i < steps - 1; i++)
            {
                resultMatrix = resultMatrix.MultiplyMatrix(ruleMatrix);
            }

            //create the result vector for each tupel
            var resultVector = resultMatrix.MultiplyMatrix(inputVector);

            //get count for each char in the resultVector
            for (int i = 0; i < size; i++)
            {
                LinkedCharCount[IndexToRule[i][0]] += resultVector[i, 0];
                LinkedCharCount[IndexToRule[i][1]] += resultVector[i, 0];
            }

            //all chars are doubled in the list (except first and last of input string there *2-1), so create list of counts/2
            var countOfEachCharInOutput = LinkedCharCount.ToList().Select(x => (x.Value + x.Value%2)/2);
            long result = countOfEachCharInOutput.Max() - countOfEachCharInOutput.Min();

            return result;
        }
    }

    public static class Extenders
    {
        public static long[,] MultiplyMatrix(this long[,] A, long[,] B)
        {
            int rowA = A.GetLength(0);
            int columnA = A.GetLength(1);
            int columnB = B.GetLength(1);
            long[,] result = new long[rowA, columnB];
            for (int row = 0; row < rowA; row++)
            {
                for (int column = 0; column < columnB; column++)
                {
                    long tmp = 0;
                    for (int k = 0; k < columnA; k++)
                    {
                        tmp += A[row, k] * B[k, column];
                    }
                    result[row, column] = tmp;
                }
            }
            return result;
        }
    }
}
