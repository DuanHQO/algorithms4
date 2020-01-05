using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class TwoColor {
        private bool[] marked;
        private bool[] color;
        public bool isTwoColorable { get; private set; } = true;

        public TwoColor(Graph G) {
            marked = new bool[G.V];
            color = new bool[G.V];
            for (int s = 0; s < G.V; s++) {
                if (!marked[s])
                    DFS(G, s);
            }
        }

        private void DFS(Graph G, int v) {
            marked[v] = true;
            foreach (var item in G.GetAdj(v)) {
                if (!marked[item]) {
                    color[item] = !color[v];
                    DFS(G, item);
                } else if (color[item] == !color[v]) {
                    isTwoColorable = false;
                }
            }
        }
    }
}
