using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{ 
    public abstract class Package
    {
        public int version;
        public int type;
        public string content;

        public Package(int v, int t, string c)
        {
            version = v;
            type = t;
        }

        public abstract long GetValueVersions();
        public abstract long GetValue();
    }
}
