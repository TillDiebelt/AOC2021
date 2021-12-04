using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TillSharp.Extenders.Collections;
using TillSharp.Math;

namespace Day4
{
    public class Bingo
    {
        int[][] game;

        public Bingo(string[] s)
        {
            game = new int[5][];
            int count = 0;
            foreach (var line in s)
            {
                var values = line.Replace("  ", " ").Split(" ").TakeLast(5).ToArray();
                var row = Array.ConvertAll(values, int.Parse);
                game[count] = row;
                count++;
            }
        }

        public bool Play(int number)
        {
            game = game.Select(x => x.Select(y => y == number ? -1 : y).ToArray()).ToArray();
            return isWinning();
        }

        public int SumEmpty()
        {
            return game.ReduceNested((int sum, int current) => current != -1 ? sum+current : sum);
        }

        private bool isWinning()
        {
            var transpose = game.First().Select((_, i) => game.Select(row => row.ElementAt(i)));
            return game.Any(x => x.All(y => y == -1)) || transpose.Any(x => x.All(y => y == -1));
        }
    }
}
