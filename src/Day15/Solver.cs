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

namespace Day15
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            var map = Parse(inputPath);
            closedlist = new HashSet<(int, int)>();
            openlist = new List<((int, int) point, int cost, int prio)>();


            (int, int) goal = (map.GetUpperBound(0), map.GetUpperBound(1));

            openlist.Add(((0, 0), 0, 0));
            while (true)
            {
                var currentNode = openlist.First();
                if (currentNode.point == goal)
                    break;

                openlist.RemoveAt(0);
                closedlist.Add(currentNode.point);
                expandNode(currentNode, map);
                openlist = openlist.OrderBy(x => x.prio).ToList();
            }

            return openlist.First().cost;
        }

        private static List<((int, int) point, int cost, int prio)> openlist = new List<((int, int) point, int cost, int prio)>();
        private static HashSet<(int, int)> closedlist = new HashSet<(int, int)>();

        private static void expandNode(((int, int) point, int cost, int prio) currentNode, int[,] map, int h = 500)
        {
            foreach (var successor in map.Neighbours(currentNode.point.Item1, currentNode.point.Item2))
            {
                if (closedlist.Contains(successor))
                    continue;
                var cost = currentNode.cost + map[successor.x, successor.y];
                var prio = cost + (h - successor.x) + (h - successor.y);
                if (openlist.Any(x => x.point == successor) && cost >= openlist.Where(x => x.point == successor).First().cost)
                    continue;
                if (openlist.Any(x => x.point == successor))
                    openlist = openlist.Map(x => x.point == successor ? (x.point,cost, prio) : x).ToList();
                else
                    openlist.Add((successor, cost, prio));
            } 
        }


        private static int[,] Parse(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int[,] arr = new int[lines.Length, lines[0].Length];

            for (int y = 0; y < lines.Length; y++)
                for (int x = 0; x < lines[0].Length; x++)
                {
                    arr[y, x] = Convert.ToInt32(lines[y][x] + "");
                }
            return arr;
        }


        public static long SolvePart2(string inputPath)
        {
            var map = Parse(inputPath);
            int[,] bigMap = new int[(map.GetUpperBound(0) + 1) * 5, (map.GetUpperBound(1) + 1) * 5];

            bigMap = fill(bigMap, map, 0, 0);
            map.MapApply(x => x == 9 ? 1 : x + 1);
            bigMap = fill(bigMap, map, 1, 0);
            bigMap = fill(bigMap, map, 0, 1);
            map.MapApply(x => x == 9 ? 1 : x + 1);
            bigMap = fill(bigMap, map, 2, 0);
            bigMap = fill(bigMap, map, 1, 1);
            bigMap = fill(bigMap, map, 0, 2);
            map.MapApply(x => x == 9 ? 1 : x + 1);
            bigMap = fill(bigMap, map, 3, 0);
            bigMap = fill(bigMap, map, 2, 1);
            bigMap = fill(bigMap, map, 1, 2);
            bigMap = fill(bigMap, map, 0, 3);
            map.MapApply(x => x == 9 ? 1 : x + 1);
            bigMap = fill(bigMap, map, 4, 0);
            bigMap = fill(bigMap, map, 3, 1);
            bigMap = fill(bigMap, map, 2, 2);
            bigMap = fill(bigMap, map, 1, 3);
            bigMap = fill(bigMap, map, 0, 4);
            map.MapApply(x => x == 9 ? 1 : x + 1);
            bigMap = fill(bigMap, map, 4, 1);
            bigMap = fill(bigMap, map, 3, 2);
            bigMap = fill(bigMap, map, 2, 3);
            bigMap = fill(bigMap, map, 1, 4);
            map.MapApply(x => x == 9 ? 1 : x + 1);
            bigMap = fill(bigMap, map, 4, 2);
            bigMap = fill(bigMap, map, 3, 3);
            bigMap = fill(bigMap, map, 2, 4);
            map.MapApply(x => x == 9 ? 1 : x + 1);
            bigMap = fill(bigMap, map, 4, 3);
            bigMap = fill(bigMap, map, 3, 4);
            map.MapApply(x => x == 9 ? 1 : x + 1);
            bigMap = fill(bigMap, map, 4, 4);

            closedlist = new HashSet<(int, int)>();
            openlist = new List<((int, int) point, int cost, int prio)>();

            (int, int) goal = (bigMap.GetUpperBound(0), bigMap.GetUpperBound(1));

            openlist.Add(((0, 0), 0, 0));
            while (true)
            {
                var currentNode = openlist.First();
                if (currentNode.point == goal)
                    break;

                openlist.RemoveAt(0);
                closedlist.Add(currentNode.point);
                expandNode(currentNode, bigMap);
                openlist = openlist.OrderBy(x => x.prio).ToList();
            }

            return openlist.First().cost;
        }

        private static int[,] fill(int[,] tofill, int[,] filler, int x, int y)
        {
            int offsetx = x;
            int offsety = y;
            int[,] result = tofill;
            for (int i = 0; i <= filler.GetUpperBound(0); i++)
                for (int j = 0; j <= filler.GetUpperBound(1); j++)
                {
                    result[i+y* filler.GetUpperBound(0) + offsety, j+x* filler.GetUpperBound(1) + offsetx] = filler[i,j];
                }
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
