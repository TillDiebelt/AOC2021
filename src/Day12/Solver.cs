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

namespace Day12
{
    public class Solver
    {
        public static long SolvePart1(string inputPath)
        {
            Dictionary<string, Cave> graph = Parse(inputPath);
            Cave start = graph["start"];
            Cave end = graph["end"];
            return Paths(start, end, graph);
        }

        public static long SolvePart2(string inputPath)
        {
            Dictionary<string, Cave> graph = Parse(inputPath);
            Cave start = graph["start"];
            Cave end = graph["end"];
            return Paths(start, end, graph, false);
        }

        private static Dictionary<string, Cave> Parse(string inputPath)
        {

            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, Cave> graph = new Dictionary<string, Cave>();

            foreach (var line in lines)
            {
                var connection = line.Split('-');
                graph.TryAdd(connection[0], new Cave(connection[0]));
                graph[connection[0]].connected.Add(connection[1]);
                graph.TryAdd(connection[1], new Cave(connection[1]));
                graph[connection[1]].connected.Add(connection[0]);
            }

            return graph;
        }

        private static int Paths(Cave start, Cave end, Dictionary<string, Cave> graph)
        {
            int uniquePaths = 0;
            graph[start.id].visited = true;
            if (start.id == end.id)
                return 1;
            foreach (var next in start.connected)
            {
                if (next == "start")
                    continue;
                Cave nextStart = graph[next];
                if (nextStart.smallCave && nextStart.visited)
                    continue;
                Dictionary<string, Cave> graphCopy = copyGraph(graph);
                uniquePaths += Paths(nextStart, end, graphCopy);
            }
            return uniquePaths;
        }

        private static int Paths(Cave start, Cave end, Dictionary<string, Cave> graph, bool twice)
        {
            if (start.id == end.id)
                return 1;
            int uniquePaths = 0;
            graph[start.id].visited = true;
            foreach (var next in start.connected)
            {
                if (next == "start")
                    continue;
                Cave nextStart = graph[next];
                if (nextStart.smallCave && nextStart.visited && twice)
                    continue;
                bool isTwice = twice;
                isTwice |= nextStart.smallCave && nextStart.visited;
                Dictionary<string, Cave> graphCopy = copyGraph(graph);
                uniquePaths += Paths(nextStart, end, graphCopy, isTwice);
            }
            return uniquePaths;
        }

        private static Cave copyCave(Cave copy)
        {
            Cave n = new Cave(copy.id);
            n.visited = copy.visited;
            foreach (var con in copy.connected) n.connected.Add(con);
            return n;
        }

        private static Dictionary<string, Cave> copyGraph(Dictionary<string, Cave> copy)
        {
            Dictionary<string, Cave> n = new Dictionary<string, Cave>();
            foreach (var con in copy)
            {
                n.Add(con.Key, copyCave(con.Value));
            }
            return n;
        }
    }
}
