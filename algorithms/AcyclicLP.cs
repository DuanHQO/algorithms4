using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class AcyclicLP {
        private DirectedEdge[] EdgeTo;
        private double[] DistTo;

        public AcyclicLP(EdgeWeightedDigraph G, int s) {
            EdgeTo = new DirectedEdge[G.V];
            DistTo = new double[G.V];
            for (int v = 0; v < G.V; v++) {
                DistTo[v] = double.NegativeInfinity;
            }
            DistTo[s] = 0.0;

            var top = new Topological(G);
            foreach (var v in top.Order) {
                Relax(G, v);
            }
        }

        private void Relax(EdgeWeightedDigraph g, int v) {
            foreach (var e in g.adj[v]) {
                var w = e.To();
                if (DistTo[w] < DistTo[v] + e.Weight) {
                    DistTo[w] = DistTo[v] + e.Weight;
                    EdgeTo[w] = e;
                }
            }
        }

        public double DistToW(int w) {
            return DistTo[w];
        }
    }
}
