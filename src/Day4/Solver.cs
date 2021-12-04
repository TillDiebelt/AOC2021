using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    public class Solver
    {
        public static long Part1(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath).Replace("\r","");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var turns = Array.ConvertAll(lines[0].Split(","), int.Parse);

            List<Bingo> games = new List<Bingo>();
            for (int i = 0; i < lines.Length / 5; i++)
            {
                games.Add(new Bingo(lines[(i*5+1)..(i*5+6)]));
            }

            foreach (var turn in turns)
            {
                //var win = games.Select(x => x.Play(turn)).Contains(true);
                foreach (var game in games)
                {
                    if (game.Play(turn))
                    {
                        return turn * game.SumEmpty();
                    }
                }
            }

            return result;
        }

        public static long Part2(string inputPath)
        {
            long result = 0;
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var turns = Array.ConvertAll(lines[0].Split(","), int.Parse);

            List<Bingo> games = new List<Bingo>();
            for (int i = 0; i < lines.Length / 5; i++)
            {
                games.Add(new Bingo(lines[(i * 5 + 1)..(i * 5 + 6)]));
            }

            foreach (var turn in turns)
            {
                //var win = games.Select(x => x.Play(turn)).Contains(true);
                for(int i = 0; i < games.Count; i++)
                {
                    var game = games[i];
                    if (game.Play(turn))
                    {
                        if (games.Count == 1)
                            return turn * game.SumEmpty();
                        games.Remove(game);
                        i--;
                    }
                }
            }
            return result;
        }
    }
}
