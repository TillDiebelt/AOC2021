using System;
using System.Linq;
using TillSharp.Extenders.Collections;

namespace Day4
{
    public class Bingo
    {
        private int[][] game;
        private int width = 5;
        private int hight = 5;

        public Bingo(string[] s)
        {
            game = new int[hight][];
            int rowIndex = 0;
            foreach (var line in s)
            {
                var values = line.Replace("  ", " ").Split(" ").TakeLast(width).ToArray();
                var row = Array.ConvertAll(values, int.Parse);
                game[rowIndex] = row;
                rowIndex++;
            }
        }

        public bool Play(int number)
        {
            game = game.Select(x => x.Select(y => y == number ? -1 : y).ToArray()).ToArray();
            return isWinning();
        }

        public int SumEmpty()
        {               
            return game.Aggregate(0, (int result, int[] current) => result + current.Aggregate(0, (x,y) => y != -1 ? x + y : x)); // ReduceNested((int sum, int current) => current != -1 ? sum+current : sum);
        }

        private bool isWinning()
        {
            var transpose = game.First().Select((_, i) => game.Select(row => row.ElementAt(i)));
            return game.Any(x => x.All(y => y == -1)) || transpose.Any(x => x.All(y => y == -1));
        }
    }
}
