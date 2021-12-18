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

namespace Day17
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var sides = input.Split(',');
            var ys = sides.Last().Split('=').Last().Split("..");
            int miny = Convert.ToInt32(ys.First());
            return (miny) * (miny + 1) / 2;
        }

        public static long SolvePart2(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var sides = input.Split(',');
            var xs = sides.First().Split('=').Last().Split("..");
            var ys = sides.Last().Split('=').Last().Split("..");
            int minx = Convert.ToInt32(xs.First());
            int maxx = Convert.ToInt32(xs.Last());
            int miny = Convert.ToInt32(ys.First());
            int maxy = Convert.ToInt32(ys.Last());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            long result = 0;

            for (int fy = miny; fy <= -miny; fy++)
            {
                for (int fx = (int)Math.Sqrt(2*minx); fx <= maxx; fx++)
                {
                    (int x, int y) Pos = (0, 0);
                    int vx = fx;
                    int vy = fy;
                    bool landed = false;
                    if (fy > 0)
                    {
                        int g1 = (fx) * (fx + 1) / 2;
                        int g2 = (fx - 2*fy -1) * (fx - 2*fy) / 2;
                        int nvx = fy * 2 + 1 >= fx ? g1 : g1 - g2;
                        Pos = (nvx, 0);
                        vx = Math.Max(fx-2*(fy)-1, 0);
                        vy = -fy-1;
                    }
                    while (Pos.x <= maxx && Pos.y >= miny)
                    {
                        Pos.x += vx;
                        Pos.y += vy;
                        if (Pos.x <= maxx && Pos.x >= minx && Pos.y <= maxy && Pos.y >= miny)
                        {
                            landed = true;
                            break;
                        }
                        vx -= Math.Sign(vx);
                        vy--;
                    }
                    if (landed)
                    {
                        result++;
                    }
                }
            }

            sw.Stop();
            Console.WriteLine(((double)sw.ElapsedTicks/ (double)Stopwatch.Frequency) * 1000000000);

            return result;
        }
    }
}
