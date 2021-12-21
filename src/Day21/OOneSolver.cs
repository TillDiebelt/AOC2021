using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    public static class OOneSolver
    {
        public static void Primes(string inputPath)
        {
            string input = File.ReadAllText(inputPath).Replace("\r", "");
            var solutions = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt64(x.Split(" ")[1]));
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToArray();

            int c = 0;
            foreach (var x in solutions)
            {
                var primes = Generate(x);
                Console.Write(lines[c] + " : ");
                c++;
                foreach (var p in primes)
                    Console.Write(p + " ");
                Console.WriteLine();
            }
        }
        public static List<(long, int)> Generate(long number)
        {
            var primes = new List<(long, int)>();

            int c = 0;
            while (number % 2 == 0)
            {
                c++;
                number /= 2;
            }
            if (c > 0)
                primes.Add((2, c));
            long maxSearch = (long)Math.Sqrt(number);
            for (long div = 3; div <= maxSearch; div+=2)
            {
                int count = 0;
                while (number % div == 0)
                {
                    count++;
                    number = number / div;
                }
                if(count > 0)
                    primes.Add((div,count));
            }

            return primes;
        }
    }
}