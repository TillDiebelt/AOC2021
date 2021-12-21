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

namespace Day21
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            int playerOne = lines[0][lines[0].Length - 1].ToDigit() - 1;
            int playerOneScore = 0;
            int playerTwo = lines[1][lines[1].Length - 1].ToDigit() - 1;
            int playerTwoScore = 0;
            int rounds = 0;
            int dice = 0;
            while (playerOneScore < 1000 && playerTwoScore < 1000)
            {
                int diceturn = 0;
                for (int i = 0; i < 3; i++)
                {
                    dice++;
                    diceturn += dice;
                    if (dice >= 100) dice = 0;
                    rounds++;
                }
                playerOne += diceturn % 10;
                playerOne = playerOne % 10;
                playerOneScore += playerOne + 1;
                if (playerOneScore >= 1000) break;

                diceturn = 0;
                for (int i = 0; i < 3; i++)
                {
                    dice++;
                    diceturn += dice;
                    if (dice >= 100) dice = 0;
                    rounds++;
                }
                playerTwo += diceturn % 10;
                playerTwo = playerTwo % 10;
                playerTwoScore += playerTwo + 1;
            }

            long result = rounds * Math.Min(playerOneScore, playerTwoScore);

            return result;
        }

        public static long SolvePart2(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            int playerOne = lines[0][lines[0].Length - 1].ToDigit();
            int playerTwo = lines[1][lines[1].Length - 1].ToDigit();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            long result = PlayDimensionalGame(playerOne, playerTwo);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            return result;
        }


        public static long PlayDimensionalGame(int a, int b)
        {        
            int playerOne = a;
            int playerOneScore = 0;
            int playerTwo = b;
            int playerTwoScore = 0;

            (long p1, long p2) result = (0, 0);

            for (int i = 0; i < 7; i++)
            {
                var tmp = DimensionalGame(playerOne, playerOneScore, playerTwo, playerTwoScore, i + 3, 1);
                result.p1 += tmp.p1 * Distribution(i);
                result.p2 += tmp.p2 * Distribution(i);
            }

            return Math.Max(result.p1, result.p2);
        }

        private static int Distribution(int input)
        {
            switch (input)
            {
                case 0:
                case 6:
                    return 1;
                case 1:
                case 5:
                    return 3;
                case 2:
                case 4:
                    return 6;
                case 3:
                    return 7;
                default:
                    return 0;
            }
        }

        private static Dictionary<(int, int, int, int, int, int), (long, long)> dyn = new Dictionary<(int, int, int, int, int, int), (long, long)>();

        public static (long p1, long p2) DimensionalGame(int p1, int p1score, int p2, int p2score, int dice, int player)
        {
            if (dyn.ContainsKey((p1, p1score, p2, p2score, dice, player)))
                return dyn[(p1, p1score, p2, p2score, dice, player)];

            int currentPlayerPosition = p1-1;
            int currentPlayerScore = p1score;

            (long p1, long p2) result = (0, 0);

            currentPlayerPosition += dice;
            currentPlayerPosition %= 10;
            currentPlayerPosition++;
            currentPlayerScore += currentPlayerPosition;
            if (currentPlayerScore >= 21) return (2 - player, player-1);

            for (int i = 0; i < 7; i++)
            {
                var tmp = DimensionalGame(p2, p2score, currentPlayerPosition, currentPlayerScore, i + 3, player%2+1);
                result.p1 += tmp.p1 * Distribution(i);
                result.p2 += tmp.p2 * Distribution(i);
            }
            dyn.Add((p1, p1score, p2, p2score, dice, player),result);
            return result;
        }
    }

    public static class Extenders
    {
        public static int ToDigit(this char self)
        {
            return self - 48;
        }

        public static IEnumerable<(int x, int y)> Neighbours<T>(this T[][] self, int x, int y)
        {
            List<(int x, int y)> neighbours = new List<(int x, int y)>();
            if (x - 1 >= 0) neighbours.Add((x - 1, y));
            if (x + 1 < self[y].Length) neighbours.Add((x + 1, y));
            if (y - 1 >= 0) neighbours.Add((x, y - 1));
            if (y + 1 < self.Length) neighbours.Add((x, y + 1));
            return neighbours;
        }

        public static IEnumerable<(int x, int y)> Neighbours<T>(this T[,] self, int x, int y)
        {
            List<(int x, int y)> neighbours = new List<(int x, int y)>();
            if (x - 1 >= 0) neighbours.Add((x - 1, y));
            if (x + 1 <= self.GetUpperBound(1)) neighbours.Add((x + 1, y));
            if (y - 1 >= 0) neighbours.Add((x, y - 1));
            if (y + 1 <= self.GetUpperBound(0)) neighbours.Add((x, y + 1));
            return neighbours;
        }

        public static IEnumerable<(int x, int y)> NeighboursDiag<T>(this T[][] self, int x, int y)
        {
            List<(int x, int y)> neighbours = new List<(int x, int y)>();
            if (x - 1 >= 0)
            {
                neighbours.Add((x - 1, y));
                if (y - 1 >= 0) neighbours.Add((x - 1, y - 1));
                if (y + 1 < self.Length) neighbours.Add((x - 1, y + 1));
            }
            if (x + 1 < self[y].Length)
            {
                neighbours.Add((x + 1, y));
                if (y - 1 >= 0) neighbours.Add((x + 1, y - 1));
                if (y + 1 < self.Length) neighbours.Add((x + 1, y + 1));
            }
            if (y - 1 >= 0) neighbours.Add((x, y - 1));
            if (y + 1 < self[y].Length) neighbours.Add((x, y + 1));
            return neighbours;
        }

        public static IEnumerable<(int x, int y)> NeighboursDiag<T>(this T[,] self, int x, int y)
        {
            List<(int x, int y)> neighbours = new List<(int x, int y)>();
            if (x - 1 >= 0)
            {
                neighbours.Add((x - 1, y));
                if (y - 1 >= 0) neighbours.Add((x - 1, y - 1));
                if (y + 1 <= self.GetUpperBound(0)) neighbours.Add((x - 1, y + 1));
            }
            if (x + 1 <= self.GetUpperBound(1))
            {
                neighbours.Add((x + 1, y));
                if (y - 1 >= 0) neighbours.Add((x + 1, y - 1));
                if (y + 1 <= self.GetUpperBound(0)) neighbours.Add((x + 1, y + 1));
            }
            if (y - 1 >= 0) neighbours.Add((x, y - 1));
            if (y + 1 <= self.GetUpperBound(0)) neighbours.Add((x, y + 1));
            return neighbours;
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
