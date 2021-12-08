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
            //vomit
            var sides = input.Split('|');
            var left = sides[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var right = sides[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var one = left.Where(x => x.Length == 2).ToArray()[0];
            var four = left.Where(x => x.Length == 4).ToArray()[0];
            var seven = left.Where(x => x.Length == 3).ToArray()[0];
            var eight = left.Where(x => x.Length == 7).ToArray()[0];
            var three = left.Where(x => x.Length == 5 && (x.Contains(one[0]) && x.Contains(one[1]))).ToArray()[0];
            var six = left.Where(x => x.Length == 6 && !(x.Contains(one[0]) && x.Contains(one[1]))).ToArray()[0];
            var zero = left.Where(x => x.Length == 6 && x.Intersect(three).Count() == 4 && x.Intersect(six).Count() == 5).ToArray()[0];
            var nine = left.Where(x => x.Length == 6 && x != zero && (x.Contains(one[0]) && x.Contains(one[1]))).ToArray()[0];
            var downR = six.Intersect(one).ToArray()[0];
            var two = left.Where(x => x.Length == 5 && !x.Contains(downR) && !(x.Contains(one[0]) && x.Contains(one[1]))).ToArray()[0];
            var five = left.Where(x => x.Length == 5 && x.Contains(downR) && !(x.Contains(one[0]) && x.Contains(one[1]))).ToArray()[0];

            for (int i = 0; i < 4; i++)
            {
                if (right[i].Length == 7) value += 8 * (int)Math.Pow(10, 3 - i);
                else if (right[i].Length == 4) value += 4 * (int)Math.Pow(10, 3 - i);
                else if (right[i].Length == 3) value += 7 * (int)Math.Pow(10, 3 - i);
                else if (right[i].Length == 2) value += 1 * (int)Math.Pow(10, 3 - i);
                else if (right[i].Intersect(six).Count() == 6) value += 6 * (int)Math.Pow(10, 3 - i);
                else if (right[i].Intersect(nine).Count() == 6) value += 9 * (int)Math.Pow(10, 3 - i);
                else if (right[i].Length == 6) value += 0;
                else if (right[i].Intersect(two).Count() == 5) value += 2 * (int)Math.Pow(10, 3 - i);
                else if (right[i].Intersect(three).Count() == 5) value += 3 * (int)Math.Pow(10, 3 - i);
                else if (right[i].Intersect(five).Count() == 5) value += 5 * (int)Math.Pow(10, 3 - i);
            }
        }

        public int GetValue()
        {
            return value;
        }
    }
}
