using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace Day20
{
    public class Solver
    {
        public static long Solve(string inputPath, int steps)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            string decoder = lines[0];
            var smallMap = lines[1].Split('\n').Select(x => x.Select(y => y == '.' ? 0 : 1).ToArray()).ToArray();
            var hugeMap = new int[smallMap.Length + steps * 2, smallMap[1].Length + steps * 2];

            for (int i = 0; i < smallMap.Length; i++)
            {
                for (int j = 0; j < smallMap[0].Length; j++)
                {
                    hugeMap[i + steps, j + steps] = smallMap[i][j];
                }
            }

            int inverting = decoder[0] == '#' && decoder[511] == '.' ? 1 : 0;

            int maxX = hugeMap.GetUpperBound(1);
            int maxY = hugeMap.GetUpperBound(0);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int step = 0; step < steps; step++)
            {
                int[,] newmap = new int[maxY + 1, maxX + 1];

                for (int i = 0; i <= maxY; i++)
                {
                    for (int j = 0; j <= maxX; j++)
                    {
                        int x = j - 1;
                        int y = i - 1;
                        int index = 0;
                        int mask = 256;

                        for (int n = 0; n < 9; n++)
                        {
                            index += IsInBounds(x, y, maxX, maxY) ? mask * hugeMap[y, x] : mask * (step % 2) * inverting;
                            mask >>= 1;
                            y += (n % 3) == 2 ? 1 : 0;  // 0 0  1 0 0  1 0 0
                            x += (n % 3) == 2 ? -2 : 1; // 1 1 -2 1 1 -2 1 1
                        }

                        newmap[i, j] = decoder[index] == '.' ? 0 : 1;
                    }
                }
                hugeMap = newmap;
            }

            long result = hugeMap.Cast<int>().Where(x => x == 1).Count();

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

            return result;
        }
        private static bool IsInBounds(int x, int y, int maxx, int maxy)
        {
            return x <= maxx && x >= 0 && y <= maxy && y >= 0;
        }
    }
}