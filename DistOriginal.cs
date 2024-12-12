using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_WindowsForms
{
    public class DistOriginal //store the shortest distance for a given vertex
    {
        public int distance;
        public int parentVert;
        public DistOriginal(int pv, int d)
        {
            distance = d; parentVert = pv;
        }
    }
}
