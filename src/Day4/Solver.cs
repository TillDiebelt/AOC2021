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
            (List<Bingo> games, int[] turns) play = parse(inputPath);
            return playGames(play.games, play.turns).First();
        }

        public static long Part2(string inputPath)
        {
            (List<Bingo> games, int[] turns) play = parse(inputPath);
            return playGames(play.games, play.turns).Last();            
        }

        private static IEnumerable<int> playGames(List<Bingo> games, int[] turns)
        {
            foreach (var turn in turns)
            {
                //var win = games.Select(x => x.Play(turn)).Contains(true);
                for (int i = 0; i < games.Count; i++)
                {
                    var game = games[i];
                    if (game.Play(turn))
                    {
                        yield return turn * game.SumEmpty();
                        games.Remove(game);
                        i--;
                    }
                }
            }
        }

        private static (List<Bingo>, int[]) parse(string path)
        {
            string input = File.ReadAllText(path).Replace("\r", "");
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var turns = Array.ConvertAll(lines[0].Split(","), int.Parse);
            List<Bingo> games = new List<Bingo>();
            int bingoLength = 5;
            for (int i = 0; i < lines.Length / bingoLength; i++)
            {
                games.Add(new Bingo(lines[(i * bingoLength + 1)..(i * bingoLength + 6)]));
            }
            return (games,turns);
        }
    }
}
