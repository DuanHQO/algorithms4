using System.Collections.Generic;

namespace algorithms {
    class DepthFirstSearch {

        private bool[] marked;
        private int[] edgeTo;
        private int s;
        public int count { get; set; }

        public DepthFirstSearch(Graph G, int s) {
            marked = new bool[G.V];
            edgeTo = new int[G.V];
            this.s = s;
            DFS(G, s);
        }

        private void DFS(Graph G, int v) {
            marked[v] = true;
            count++;
            foreach (var item in G.GetAdj(v)) {
                if (!marked[item]) {
                    edgeTo[item] = v;
                    DFS(G, item);
                }
            }
        }

        public bool HasPathTo(int v) {
            return marked[v];
        }

        public IEnumerable<int> PathTo(int v) {
            if (!HasPathTo(v)) return null;
            Stack<int> path = new Stack<int>();
            for (int x = v; x != s; x = edgeTo[x]) {
                path.Push(x);
            }
            return path;
        }
    }
}
