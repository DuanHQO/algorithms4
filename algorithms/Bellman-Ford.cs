using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class Bellman_Ford {
        private double[] DistTo;
        private DirectedEdge[] EdgeTo;
        private bool[] OnQ;
        private Queue<int> queue;
        private int cost;
        private IEnumerable<DirectedEdge> Cycle;

        public Bellman_Ford(EdgeWeightedDigraph G, int s) {
            DistTo = new double[G.V];
            EdgeTo = new DirectedEdge[G.V];
            OnQ = new bool[G.V];
            queue = new Queue<int>();
            for (int v = 0; v < G.V; v++) {
                DistTo[v] = double.PositiveInfinity;
            }
            DistTo[s] = 0.0;
            queue.Enqueue(s);
            OnQ[s] = true;
            while (queue.Count > 0  ) {
                var v = queue.Dequeue();
                OnQ[v] = false;
                Relax(G, v);
            }
        }

        private void Relax(EdgeWeightedDigraph G, int v) {
            foreach (var e in G.adj[v]) {
                var w = e.To();
                if (DistTo[w] > DistTo[v] + e.Weight) {
                    DistTo[w] = DistTo[v] + e.Weight;
                    EdgeTo[w] = e;
                    if (!OnQ[w]) {
                        queue.Enqueue(w);
                        OnQ[w] = true;
                    }
                }
                if(cost ++ % G.V == 0) {
                    FindNegativeCycle();
                }
            }
        }

        public double DistToW(int v) {
            throw new NotImplementedException();
        }

        private void FindNegativeCycle() {
            throw new NotImplementedException();
        }

        public bool HasPathTo(int v) {
            throw new NotImplementedException();
        }

        public IEnumerable<DirectedEdge> PathTo(int v) {
            throw new NotImplementedException();
        }

        public IEnumerable<Edge> NegativeCycle() {
            throw new NotImplementedException();
        }

        public bool HasNegativeCycle() {
            throw new NotImplementedException();
        }

    }
}
