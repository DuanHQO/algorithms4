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
                //if (!Marked[v])
                    //DFS(G, v);
            }
        }

        public DirectedCycle(EdgeWeightedDigraph G) {
            Marked = new bool[G.V];
            EdgeTo = new int[G.V];
            OnStack = new bool[G.V];
            for (int v = 0; v < G.V; v++) {
                if (!Marked[v])
                    DFS(G, v);
            }
        }

        private void DFS(EdgeWeightedDigraph g, int v) {
            Marked[v] = true;
            OnStack[v] = true;
            foreach (var w in g.adj[v]) {
                if (HasCycle()) {
                    return;
                } else if (!Marked[w.To()]) {
                    EdgeTo[w.To()] = v;
                    DFS(g, w.To());
                } else if (OnStack[w.To()]) {
                    Cycle = new Stack<int>();
                    for (int x = v; x != w.To(); x = EdgeTo[x]) {
                        Cycle.Push(x);
                    }
                    Cycle.Push(w.To());
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
