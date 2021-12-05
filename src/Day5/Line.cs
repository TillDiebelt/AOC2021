using System;
using System.Collections.Generic;
using System.Text;

namespace Day5
{
    public class Line
    {
        (int x, int y) start;
        (int x, int y) end;

        public Line(string input)
        {
            var points = input.Replace(" ", "").Split("->");
            var startPoint = points[0].Split(',');
            this.start = (Convert.ToInt32(startPoint[0]), Convert.ToInt32(startPoint[1]));
            var endPoint = points[1].Split(',');
            this.end = (Convert.ToInt32(endPoint[0]), Convert.ToInt32(endPoint[1]));
        }

        public int[,] Draw(int[,] field, bool diag = false)
        {
            if (this.start.x == this.end.x || this.start.y == this.end.y || diag)
            {
                foreach (var point in getPoints())
                field[point.y, point.x]++;
            }
            return field;
        }

        private IEnumerable<(int x, int y)> getPoints()
        {
            (int x, int y) point = (start.x, start.y);
            int length = Math.Max(Math.Abs(start.x - end.x), Math.Abs(start.y - end.y));
            for (int i = 0; i <= length; i ++)
            {
                yield return point;
                point.x += Math.Sign(end.x - start.x);
                point.y += Math.Sign(end.y - start.y);
            }
        }
    }
}
