using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_WindowsForms
{
    public class Vertex
    {
        public string label;
        public bool isInTree; //is part of the tree of vertices whose shortest paths have been determined
        public Vertex(string lab) { label = lab; isInTree = false; }
    }
}
