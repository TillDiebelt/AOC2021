using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    public class Line
    {
        public (int x, int y) start;
        public (int x, int y) end;

        public Line(string input)
        {
            var points = input.Replace(" ", "").Split("->");
            var startPoint = points[0].Split(',');
            this.start = (Convert.ToInt32(startPoint[0]), Convert.ToInt32(startPoint[1]));
            var endPoint = points[1].Split(',');
            this.end = (Convert.ToInt32(endPoint[0]), Convert.ToInt32(endPoint[1]));
        }
        public IEnumerable<(int x, int y)> GetPoints()
        {
            int length = Math.Max(Math.Abs(start.x - end.x), Math.Abs(start.y - end.y));
            int stepX = Math.Sign(end.x - start.x);
            int stepY = Math.Sign(end.y - start.y);
            return Enumerable.Range(0, length + 1).Select(x => (start.x + x * stepX, start.y + x * stepY));
        }

        public IEnumerable<(int x, int y)> GetPointsLazy()
        {
            (int x, int y) point = (start.x, start.y);
            int length = Math.Max(Math.Abs(start.x - end.x), Math.Abs(start.y - end.y));
            int stepX = point.x += Math.Sign(end.x - start.x);
            int stepY = point.y += Math.Sign(end.y - start.y);
            for (int i = 0; i <= length; i ++)
            {
                yield return point;
                point.x += stepX;
                point.y += stepY;
            }
        }
    }
}
