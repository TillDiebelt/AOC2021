using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TillSharp.Extenders.Collections;

namespace Day18
{
    public class UberSolver
    {
        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            List<List<(int number, int depth)>> equations = new List<List<(int number, int depth)>>();

            foreach (var line in lines)
            {
                List<(int number, int depth)> equation = new List<(int number, int depth)>();
                int depth = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '[') depth++;
                    if (line[i] == ']') depth--;
                    if (line[i].ToDigit() is >= 0 and <= 9) equation.Add((line[i].ToDigit(), depth));
                }
                equations.Add(equation);
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<(int number, int depth)> currentEquation = equations.First();
            for (int i = 1; i <  equations.Count; i++)
            {
                currentEquation.AddRange(equations[i]);
                currentEquation = currentEquation.Map(x => (x.number, x.depth+1)).ToList();
                while (reduce(ref currentEquation)) ;
            }
            long result = magnitude(ref currentEquation);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            return result;
        }


        public static long SolvePart2(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            List<List<(int number, int depth)>> equations = new List<List<(int number, int depth)>>();

            foreach (var line in lines)
            {
                List<(int number, int depth)> equation = new List<(int number, int depth)>();
                int depth = 1;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '[') depth++;
                    if (line[i] == ']') depth--;
                    if (line[i].ToDigit() is >= 0 and <= 9) equation.Add((line[i].ToDigit(), depth));
                }
                equations.Add(equation);
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();

            List<int> results = new List<int>();
            for (int i = 0; i < equations.Count; i++)
            {
                for (int j = 0; j < equations.Count; j++)
                {
                    if (i != j)
                    {
                        List<(int number, int depth)> currentEquation = new List<(int number, int depth)>();
                        currentEquation.AddRange(equations[i]);
                        currentEquation.AddRange(equations[j]);
                        while (reduce(ref currentEquation)) ;
                        results.Add(magnitude(ref currentEquation));
                    }

                }
            }

            long result = results.Max(); ;

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            return result;
        }

        private static bool reduce(ref List<(int number, int depth)> equation)
        {
            return explode(ref equation) || split(ref equation);
        }

        private static bool explode(ref List<(int number, int depth)> equation)
        {
            bool reduced = false;
            for (int i = 0; i < equation.Count; i++)
            {
                if (equation[i].depth == 5)
                {
                    if (i > 0) equation[i - 1] = (equation[i].number + equation[i - 1].number, equation[i - 1].depth);
                    if (i+2 < equation.Count) equation[i+2] = (equation[i+1].number + equation[i +2].number, equation[i +2].depth);
                    equation.RemoveRange(i, 2);
                    equation.Insert(i, (0, 4));
                    reduced = true;
                }
            }
            return reduced;
        }

        private static int magnitude(ref List<(int number, int depth)> equation)
        {
            for (int depth = 5; depth > 0; depth--)
                for (int i = 0; i < equation.Count; i++)
                {
                    if (equation[i].depth == depth)
                    {
                        int newVal;
                        if (i == equation.Count - 1)
                            newVal = equation[i].number;
                        else
                        {
                            newVal = equation[i].number * 3 + equation[i + 1].number * 2;
                            equation.RemoveAt(i);
                        }
                        equation.RemoveAt(i);
                        equation.Insert(i, (newVal, depth-1));
                    }
                }
            return equation.First().number;
        }

        private static bool split(ref List<(int number, int depth)> equation)
        {
            for (int i = 0; i < equation.Count; i++)
            {
                if (equation[i].number > 9)
                {
                    int left = equation[i].number / 2;
                    int right = (equation[i].number+1) / 2;
                    int depth = equation[i].depth + 1;
                    equation.RemoveAt(i);
                    equation.Insert(i, (right, depth));
                    equation.Insert(i, (left, depth));
                    return true;
                }
            }
            return false;
        }

    }


    public static class Extenders
    {
        public static int ToDigit(this char self)
        {
            return self - 48;
        }
    }
}
