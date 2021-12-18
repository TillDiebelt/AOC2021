using System;

namespace Day16
{
    public class ValuePackage : Package
    {
        public long value;
        public ValuePackage(int v, int t, string c) : base(v, t,c)
        {
            version = v;
            type = t;
            content = c;
            string val = "";
            for (int i = 0; i < content.Length; i++)
            {
                if (i % 5 != 0)
                    val += content[i];
            }
            this.value = Convert.ToInt64(val,2);
        }

        public override long GetValue()
        {
            return this.value;
        }

        public override long GetValueVersions()
        {
            return version;
        }
    }
}
