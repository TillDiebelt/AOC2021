using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day8
{
    public class SevenSegment
    {
        int value;

        public SevenSegment(string input)
        {
            var sides = input.Split('|');
            var left = sides[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var right = sides[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            //vomit 936117
            Dictionary<string, int> encoding = new Dictionary<string, int>();
            var one = left.Where(x => x.Length == 2).First().Sort();
            var six = left.Where(x => x.Length == 6 && !(x.Contains(one[0]) && x.Contains(one[1]))).First().Sort();
            var downR = six.Intersect(one).First();
            var five = left.Where(x => x.Length == 5 && x.Contains(downR) && !(x.Contains(one[0]) && x.Contains(one[1]))).First().Sort();
            var zero = left.Where(x => x.Length == 6 && x.Intersect(five).Count() == 4).First().Sort();
            encoding.Add(one, 1);
            encoding.Add(six, 6);
            encoding.Add(left.Where(x => x.Length == 4).First().Sort(), 4);
            encoding.Add(left.Where(x => x.Length == 3).First().Sort(), 7);
            encoding.Add(left.Where(x => x.Length == 7).First().Sort(), 8);
            encoding.Add(zero, 0);
            encoding.Add(left.Where(x => x.Length == 5 && !x.Contains(downR) && !(x.Contains(one[0]) && x.Contains(one[1]))).First().Sort(), 2);
            encoding.Add(left.Where(x => x.Length == 5 && (x.Contains(one[0]) && x.Contains(one[1]))).First().Sort(), 3);
            encoding.Add(five, 5);
            encoding.Add(left.Where(x => x.Length == 6 && x.Sort() != zero && x.Sort() != six).First().Sort(), 9);

            for (int i = 0; i < 4; i++)
            {
                value += encoding[right[i].Sort()] * (int)Math.Pow(10, 3 - i);
            }
        }

        public int GetValue()
        {
            return value;
        }
    }

    public static class Extender
    {
        public static string Sort(this string self)
        {
            var arr = self.ToArray();
            Array.Sort(arr);
            return arr.Aggregate("", (string r, char c) => r += c);
        }
    }
}

