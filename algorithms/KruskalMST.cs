using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class KruskalMST {

        public Queue<Edge> mst { get; private set;}

        public KruskalMST(EdgeWeightedGraph G) {
            mst = new Queue<Edge>();
            SortedSet<Edge> pq = new SortedSet<Edge>();
            foreach (var e in G.GetEdges()) {
                pq.Add(e);
            }

            var uf = new WeightedQuickUnionUF(G.V);
            while (pq.Count > 0 && mst.Count < G.V - 1) {
                var e = pq.Min;
                pq.Remove(e);
                var v = e.Either();
                var w = e.Other(v);
                if (uf.Connected(v, w)) continue;
                uf.Union(v, w);
                mst.Enqueue(e);
            }
        }

    }
}
