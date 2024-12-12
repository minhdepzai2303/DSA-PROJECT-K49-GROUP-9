using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_WindowsForms
{
    public class Graph
    {
        private const int max_verts = 20;
        int infinity = 90000;
        Vertex[] vertexList; //hold all vertices in the graph
        int[,] adjMat;
        int nVerts; int nTree;
        DistOriginal[] sPath; //store the shortest paths from the source vertex to each vertex
        int currentVert; int startToCurrent;
        public Graph()
        {
            vertexList = new Vertex[max_verts];
            adjMat = new int[max_verts, max_verts];
            nVerts = 0; nTree = 0;
            for (int j = 0; j <= max_verts - 1; j++)
                for (int k = 0; k <= max_verts - 1; k++)
                    adjMat[j, k] = infinity;
            sPath = new DistOriginal[max_verts];
        }
        public void AddVertex(string lab)
        {
            vertexList[nVerts] = new Vertex(lab);
            nVerts++;
        }
        public void AddEdge(int start, int theEnd, int weight)
        {
            adjMat[start, theEnd] = weight;
            adjMat[theEnd, start] = weight;

        }
        public int GetWeights(int i, int j)
        {
            return adjMat[i, j];
        }

        public int GetMin() //find the vertex with the smallest distance that hasnt been included
        {
            int minDist = infinity;
            int indexMin = 0;
            for (int j = 0; j <= nVerts - 1; j++)
                if (!(vertexList[j].isInTree) && sPath[j].distance < minDist)
                {
                    minDist = sPath[j].distance; indexMin = j;
                }
            return indexMin;
        }
        public void AdjustShortPath()
        {
            int column = 0;
            while (column < nVerts)
            {
                if (vertexList[column].isInTree)
                    column++;
                else
                {
                    int currentToFring = adjMat[currentVert, column];
                    int startToFringe = startToCurrent + currentToFring;
                    int sPathDist = sPath[column].distance;
                    if (startToFringe < sPathDist)
                    {
                        sPath[column].parentVert = currentVert;
                        sPath[column].distance = startToFringe;
                    }
                    column++;
                }
            }
        }
        public int Path(int start, int end)
        {
            int startTree = start;
            int endTree = end;
            vertexList[startTree].isInTree = true;
            nTree = 1;
            for (int j = 0; j <= nVerts - 1; j++)
            {
                int tempDist = adjMat[startTree, j];
                sPath[j] = new DistOriginal(startTree, tempDist);
            }
            while (nTree < nVerts)
            {
                int indexMin = GetMin();
                int minDist = sPath[indexMin].distance;
                currentVert = indexMin;
                startToCurrent = sPath[indexMin].distance;
                vertexList[currentVert].isInTree = true;
                nTree++;
                AdjustShortPath();
            }
            nTree = 0;
            for (int j = 0; j <= nVerts - 1; j++)
                vertexList[j].isInTree = false;

            return sPath[endTree].distance;
        }
        public void PathTracking(int start, int end, List<int> parentVer, List<int> currentVer)
        {
            parentVer.Clear();
            currentVer.Clear();

            int current = end;

            while (current != start)
            {
                int parent = sPath[current].parentVert;

                if (parent == -1) // No valid path
                {
                    return;
                }

                parentVer.Add(parent);
                currentVer.Add(current);
                current = parent;
            }
        }

    }
}
