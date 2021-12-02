using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day2
{
    public class AnotherSolution
    {
        public static long SolvePart1(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath);

            //parsing
            Dictionary<string, Func<int, Movement>> movementFactory = new Dictionary<string, Func<int, Movement>>()
            {
                {"forward", (int distance) => new Forward(distance) },
                {"up", (int distance) => new Up(distance) },
                {"down", (int distance) => new Down(distance) }
            };
            var movements = input.Split("\n").Select(x => movementFactory[x.Split(' ')[0]](Convert.ToInt32(x.Split(' ')[1])));

            //calculation
            sw.Start();
            (int forward, int depth) finalDestination = movements.Aggregate(
                (0, 0), ((int forward, int depth) destination, Movement current) =>
                    current switch
                    {
                        Forward f => (destination.forward + f.Distance, destination.depth),
                        Up u => (destination.forward, destination.depth - u.Distance),
                        Down d => (destination.forward, destination.depth + d.Distance)
                    }
            );

            result = finalDestination.depth * finalDestination.forward;
            return result;
        }

        public static long SolvePart2(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath);

            //parsing
            Dictionary<string, Func<int, Movement>> movementFactory = new Dictionary<string, Func<int, Movement>>()
            {
                {"forward", (int distance) => new Forward(distance) },
                {"up", (int distance) => new Up(distance) },
                {"down", (int distance) => new Down(distance) }
            };
            var movements = input.Split("\n").Select(x => movementFactory[x.Split(' ')[0]](Convert.ToInt32(x.Split(' ')[1])));

            //calculation
            (int forward, int depth, int aim) finalDestination = movements.Aggregate(
                (0, 0, 0), ((int forward, int depth, int aim) destination, Movement current) =>
                    current switch
                    {
                        Forward f => (destination.forward + f.Distance, destination.depth + f.Distance * destination.aim, destination.aim),
                        Up u => (destination.forward, destination.depth, destination.aim - u.Distance),
                        Down d => (destination.forward, destination.depth, destination.aim + d.Distance)
                    }
            );

            result = finalDestination.depth * finalDestination.forward;

            return result;
        }

        private abstract class Movement { public int Distance { get; set; } public Movement(int distance) { this.Distance = distance; } }
        private class Forward : Movement { public Forward(int distance) : base(distance) { } }
        private class Down : Movement { public Down(int distance) : base(distance) { } }
        private class Up : Movement { public Up(int distance) : base(distance) { } }
    }
}
