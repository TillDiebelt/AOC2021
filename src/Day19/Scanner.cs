using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public class Scanner
    {
        public int id;
        public List<(int x, int y, int z)> triples;
        public (int x, int y, int z) position = (0,0,0);
        public List<int> neighbours;

        public Scanner(string input)
        {
            var lines = input.Split('\n');
            this.id = Convert.ToInt32(lines[0].Split(' ')[2]);
            this.triples = new List<(int x, int y, int z)>();
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var tripel = line.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
                triples.Add((tripel[0], tripel[1], tripel[2]));
            }
            this.neighbours = new List<int>();
        }

        public int Overlapping(Scanner scanner)
        {
            List<List<(int x, int y, int z)>> BeaconVectorsThis = new List<List<(int x, int y, int z)>>();
            for (int i = 0; i < triples.Count; i++)
            {
                List<(int x, int y, int z)> beaconators = new List<(int x, int y, int z)>();
                for (int j = 0; j < triples.Count; j++)
                {
                    if (i != j)
                    {
                        (int x, int y, int z) vec = (
                            Math.Abs(triples[i].x - triples[j].x),
                            Math.Abs(triples[i].y - triples[j].y),
                            Math.Abs(triples[i].z - triples[j].z));
                        beaconators.Add(vec);
                    }
                }
                BeaconVectorsThis.Add(beaconators);
            }

            int result = 0;
            foreach (var vecListThis in BeaconVectorsThis)
            {
                for (int p = 0; p < 6; p++)
                {

                    List<List<(int x, int y, int z)>> BeaconVectorsIn = new List<List<(int x, int y, int z)>>();
                    for (int i = 0; i < scanner.triples.Count; i++)
                    {
                        List<(int x, int y, int z)> beaconators = new List<(int x, int y, int z)>();
                        for (int j = 0; j < scanner.triples.Count; j++)
                        {
                            if (i != j)
                            {
                                int[] vec = new int[3]
                                {
                                    Math.Abs(scanner.triples[i].x - scanner.triples[j].x),
                                    Math.Abs(scanner.triples[i].y - scanner.triples[j].y),
                                    Math.Abs(scanner.triples[i].z - scanner.triples[j].z)
                                };
                                beaconators.Add((vec[p % 3], vec[3 - (p % 3 + (2 - p / 2))], vec[2 - (p / 2)]));
                            }
                        }
                        BeaconVectorsIn.Add(beaconators);
                    }

                    foreach (var vecListIn in BeaconVectorsIn)
                    {
                        int res = vecListThis.Where(x => vecListIn.Any(y => y == x)).Count();
                        //foreach (var vec in vecListIn)
                        //{
                        //    if (vecListThis.Any(x => x == vec)) res++;
                        //}
                        result = Math.Max(result, res);
                        if (res == 11)
                        {
                            neighbours.Add(scanner.id);
                            scanner.neighbours.Add(this.id);
                            var offsetVector = vecListThis.Where(x => vecListIn.Any(y => y == x)).First();
                            var indexThis = vecListThis.IndexOf(offsetVector) + 1;
                            var indexIn = vecListIn.IndexOf(offsetVector) + 1;
                            if (scanner.position == (0, 0, 0))
                            {
                                if (p != 0)
                                {
                                    List<(int x, int y, int z)> newTriples = new List<(int x, int y, int z)>();
                                    foreach (var t in scanner.triples)
                                    {
                                        var beaconIn = new int[] { t.x, t.y, t.z };
                                        (int x, int y, int z) newBeaconIn = (beaconIn[p % 3], beaconIn[3 - (p % 3 + (2 - p / 2))], beaconIn[2 - (p / 2)]);
                                        newTriples.Add(newBeaconIn);
                                    }
                                    scanner.triples = newTriples;
                                }
                                var beaconThis = triples[indexThis];
                                var beaconInPermutation = scanner.triples[indexIn];
                                //var beaconIn = new int[] { scanner.triples[indexIn].x, scanner.triples[indexIn].y, scanner.triples[indexIn].z };
                                //(int x, int y, int z) beaconInPermutation = (beaconIn[p % 3], beaconIn[3 - (p % 3 + (2 - p / 2))], beaconIn[2 - (p / 2)]);
                                scanner.position = (
                                    this.position.x - (Math.Abs(beaconInPermutation.x) - (beaconThis.x)),
                                    this.position.y - (Math.Abs(beaconInPermutation.y) - (beaconThis.y)),
                                    this.position.z - (Math.Abs(beaconInPermutation.z) - (beaconThis.z)));
                                //-460,603,-452
                                //528,-643,409
                            }
                            else
                            {
                                var beaconIn = new int[] { scanner.triples[indexIn].x, scanner.triples[indexIn].y, scanner.triples[indexIn].z };
                                var beaconThis = triples[indexThis];
                                (int x, int y, int z) beaconInPermutation = (beaconIn[p % 3], beaconIn[3 - (p % 3 + (2 - p / 2))], beaconIn[2 - (p / 2)]);
                                this.position = (
                                    scanner.position.x - (beaconThis.x - beaconInPermutation.x),
                                    scanner.position.y - (beaconThis.y - beaconInPermutation.y),
                                    scanner.position.z - (beaconThis.z - beaconInPermutation.z));
                            }
                            return 11;
                        }
                    }
                }
            }
            return result;
        }
    }
}
