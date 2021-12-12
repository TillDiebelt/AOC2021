using System;
using System.Collections.Generic;
using System.Text;

namespace Day12
{
    public class Cave
    {
        public bool visited;
        public List<string> connected;
        public string id;
        public bool smallCave;

        public Cave(string id)
        {
            this.id = id;
            this.connected = new List<string>();
            if (id[0] > 95)
                this.smallCave = true;
        }

    }
}
