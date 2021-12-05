using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    public class Solver
    {
        public static long Solve(string inputPath, bool diag)
        {
            long result = 0;
            List<Line> lines = ParseDay5(inputPath);

            Dictionary<(int, int), int> field = new Dictionary<(int, int), int>();

            foreach (var line in lines)
                Draw(line, field, diag);

            result = field.Where(x => x.Value >= 2).Count();
            return result;
        }

        private static List<Line> ParseDay5(string path)
        {
            string input = File.ReadAllText(path).Replace("\r", "");
            var lineInput = input.Split('\n').ToList();
            List<Line> lines = new List<Line>();
            foreach (var line in lineInput)
                lines.Add(new Line(line));
            return lines;
        }

        public static void Draw(Line line, Dictionary<(int, int), int> field, bool diag = false)
        {
            if (line.start.x == line.end.x || line.start.y == line.end.y || diag)
            {
                foreach (var point in line.GetPoints())
                    if (!field.TryAdd((point.y, point.x), 1))
                        field[(point.y, point.x)]++;
            }
        }
    }
}
