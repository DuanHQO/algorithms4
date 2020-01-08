using System.Collections.Generic;

namespace algorithms {
    class DirectedCycle {
        public bool[] Marked { get; private set; }
        public int[] EdgeTo { get; private set; }
        public Stack<int> Cycle { get; private set; }
        private bool[] OnStack;

        public DirectedCycle(Digraph G) {
            Marked = new bool[G.V];
            EdgeTo = new int[G.V];
            OnStack = new bool[G.V];
            for (int v = 0; v < G.V; v++) {
                if (!Marked[v])
                    DFS(G, v);
            }
        }

        private void DFS(Digraph g, int v) {
            Marked[v] = true;
            OnStack[v] = true;
            foreach (var w in g.Adj(v)) {
                if (HasCycle()) {
                    return;
                } else if (!Marked[w]) {
                    EdgeTo[w] = v;
                    DFS(g, w);
                } else if (OnStack[w]) {
                    Cycle = new Stack<int>();
                    for (int x = v; x != w; x = EdgeTo[x]) {
                        Cycle.Push(x);
                    }
                    Cycle.Push(w);
                    Cycle.Push(v);
                }
            }
            OnStack[v] = false;
        }

        public bool HasCycle() {
            return Cycle != null;
        }

        
    }
}
