using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day4
{
    public class Bingo
    {
        int[][] game;
        bool[][] seen;

        public Bingo(string[] s)
        {
            game = new int[5][];
            seen = new bool[5][];
            for (int i = 0; i < 5; i++)
            {
                seen[i] = new bool[5];
            }
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
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (game[i][j] == number)
                        seen[i][j] = true;
                }
            }
            return isWinning();
        }

        public int SumEmpty()
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    sum += seen[i][j] ? 0 : game[i][j];
                }
            }
            return sum;
        }

        private bool isWinning()
        {
            bool winning = false;
            for (int y = 0; y < seen.GetLength(0); y++)
            {
                bool row = seen[y].All(x => x == true);
                bool column = true;
                for (int x = 0; x < seen.GetLength(0); x++)
                    column &= seen[x][y];
                if (row || column)
                { 
                    winning = true;
                    break;
                }
            }
            return winning;
        }
    }
}
