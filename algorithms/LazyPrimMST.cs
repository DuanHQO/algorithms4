using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class LazyPrimMST {

        private bool[] marked;
        public Queue<Edge> mst{ get; private set; }
        private SortedSet<Edge> pq;

        public LazyPrimMST(EdgeWeightedGraph G) {
            pq = new SortedSet<Edge>();
            marked = new bool[G.V];
            mst = new Queue<Edge>();

            //假设G是连通的
            Visit(G, 0);
            while (pq.Count > 0) {
                var e = pq.Min;
                pq.Remove(e);

                var v = e.Either();
                var w = e.Other(v);

                if (marked[v] && marked[w])
                    continue;
                mst.Enqueue(e);
                if (!marked[v]) Visit(G, v);
                if (!marked[w]) Visit(G, w);
            }
        }

        private void Visit(EdgeWeightedGraph g, int v) {
            marked[v] = true;
            foreach (var e in g.Adj[v]) {
                if (!marked[e.Other(v)])
                    pq.Add(e);
            }
        }


    }
}