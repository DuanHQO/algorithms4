using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class KosarajuSCC {

        public bool[] Marked;
        private int[] id;
        private int count;

        public KosarajuSCC(Digraph G) {
            Marked = new bool[G.V];
            id = new int[G.V];
            var order = new DepthFirstOrder(G.Reverse());

            foreach (var w in order.ReversePost) {
                if (!Marked[w]) {
                    DFS(G, w);
                    count++;
                }
            }
        }

        private void DFS(Digraph G, int v) {
            Marked[v] = true;
            id[v] = count;
            foreach (var w in G.Adj(v)) {
                if (!Marked[w])
                    DFS(G, w);
            }
        }

        public bool StronglyConnected(int v, int w) {
            return id[v] == id[w];
        }


        public int Count() {
            return count;
        }
    }
}
