using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    public class OperatorPackage : Package
    {

        public int lengthtype;
        public int subbitlenght;
        public List<Package> subPackages;

        public OperatorPackage(int v, int t, int l, int s, string c) : base(v, t, c)
        {
            lengthtype = l;
            subbitlenght = s;
            content = c;
            this.subPackages = PackageFactory.GeneratePackages(content).ToList();
        }

        public override long GetValue()
        {
            switch (this.type)
            {
                case 0:
                    return subPackages.Aggregate(0, (long res, Package cur) => res + cur.GetValue());
                case 1:
                    return subPackages.Aggregate(1, (long res, Package cur) => res * cur.GetValue());
                case 2:
                    return subPackages.Aggregate(int.MaxValue, (long res, Package cur) => Math.Min(res , cur.GetValue()));
                case 3:
                    return subPackages.Aggregate(0, (long res, Package cur) => Math.Max(res, cur.GetValue()));
                case 5:
                    return subPackages.First().GetValue() > subPackages.Last().GetValue() ? 1 : 0;
                case 6:
                    return subPackages.First().GetValue() < subPackages.Last().GetValue() ? 1 : 0;
                case 7:
                    return subPackages.First().GetValue() == subPackages.Last().GetValue() ? 1 : 0;
                default:
                    return 0;
            }
        }

        public override long GetValueVersions()
        {
            return subPackages.Aggregate(0, (long res, Package cur) => res + cur.GetValueVersions()) + version;
        }
    }
}
