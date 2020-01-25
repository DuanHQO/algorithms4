using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class EdgeWeightedCycleFinder {
        private bool[] marked;
        private DirectedEdge[] EdgeTo;
        private bool[] OnStack;
        public Stack<DirectedEdge> Cycle { get; private set; }

        public EdgeWeightedCycleFinder(EdgeWeightedDigraph G) {
            marked = new bool[G.V];
            OnStack = new bool[G.V];
            EdgeTo = new DirectedEdge[G.V];
            for (int v = 0; v < G.V; v++) {
                if (!marked[v]) {
                    DFS(G, v);
                }
            }
        }

        private void DFS(EdgeWeightedDigraph G, int v) {
            OnStack[v] = true;
            marked[v] = true;
            foreach (var e in G.adj[v]) {
                var w = e.To();
                if (HasCycle()) {
                    return;
                } else if (!marked[w]) {
                    EdgeTo[w] = e;
                    DFS(G, w);
                } else if (OnStack[w]) {
                    Cycle = new Stack<DirectedEdge>();
                    var x = e;
                    while (x.From() != w) {
                        Cycle.Push(x);
                        x = EdgeTo[x.From()];
                    }
                    Cycle.Push(x);
                    return;
                }
            }
        }

        private bool HasCycle() {
            return Cycle != null;
        }
    }
}
