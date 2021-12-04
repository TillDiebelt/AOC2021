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
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(game[i][j] == number) game[i][j] = -1;
                }
            }
            return isWinning();
        }

        public int SumEmpty()
        {
            int sumNotPlayed = game.ReduceNested((int sum, int current) => current != -1 ? sum+current : sum);
            return sumNotPlayed;
        }

        private bool isWinning()
        {
            bool winning = false;
            for (int y = 0; y < game.GetLength(0); y++)
            {
                bool row = game[y].All(x => x == -1);
                int column = 0;
                for (int x = 0; x < game.GetLength(0); x++)
                    column += game[x][y];
                if (row || column==-5)
                { 
                    winning = true;
                    break;
                }
            }
            return winning;
        }
    }
}
