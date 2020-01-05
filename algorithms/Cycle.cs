using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class Cycle {
        private bool[] marked;
        public bool HasCycle { get; private set; }

        public Cycle(Graph G) {
            marked = new bool[G.V];
            for (int s = 0; s < G.V; s++) {
                if (!marked[s])
                    DFS(G, s, s);
            }
        }

        private void DFS(Graph G, int v, int u) {
            marked[v] = true;
            foreach (var item in G.GetAdj(v)) {
                if (!marked[item])
                    DFS(G, item, u);
                else if (item != u)
                    HasCycle = true;

            }
        }
    }
}
