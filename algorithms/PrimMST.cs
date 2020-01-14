using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class PrimMST {

        private Edge[] EdgeTo;
        private double[] DistTo;
        private bool[] Marked;
        private SortedDictionary<int, double> pq;

        public PrimMST(EdgeWeightedGraph G) {
            EdgeTo = new Edge[G.V];
            DistTo = new double[G.V];
            Marked = new bool[G.V];
            for (int v = 0; v < G.V; v++) {
                DistTo[v] = double.MaxValue;
            }
            pq = new SortedDictionary<int, double>();
            DistTo[0] = 0.0;
            
            while (pq.Count > 0) {
                var min = pq.Min();
                pq.Remove(min.Key);

                Visit(G, min.Key);
            }
        }

        private void Visit(EdgeWeightedGraph G, int v) {
            Marked[v] = true;
            foreach (var e in G.Adj[v]) {
                var w = e.Other(v);
                if (Marked[w]) continue;
                if (e.Weight < DistTo[w]) {
                    EdgeTo[w] = e;
                    DistTo[w] = e.Weight;
                    if (pq.ContainsKey(w)) {
                        pq[w] = DistTo[w];
                    } else {
                        pq.Add(w, DistTo[w]);
                    }
                }
            }
        }


    }
}
