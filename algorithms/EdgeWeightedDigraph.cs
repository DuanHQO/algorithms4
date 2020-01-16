using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {

    public class DirectedEdge {
        private int v;
        private int w;
        public double Weight { get; private set; }

        public DirectedEdge(int v, int w, double weight) {
            this.v = v;
            this.w = w;
            this.Weight = weight;
        }
        public int From() {
            return v;
        }

        public int To() {
            return w;
        }

        public override string ToString() {
            return string.Format("{0}->{1} {2:f2}", GetV(), W, Weight);
        }
    }

    class EdgeWeightedDigraph {
        public int V { get; private set; }
        public int E { get; private set; }
        public List<DirectedEdge>[] adj { get; private set; }

        public EdgeWeightedDigraph(int v) {
            this.V = v;
            this.E = 0;
            adj = new List<DirectedEdge>[v];
            for (int i = 0; i < v; i++) {
                adj[i] = new List<DirectedEdge>();
            }
        }

        public EdgeWeightedDigraph(StreamReader reader) {

        }

        public void AddEdge(DirectedEdge e) {
            adj[e.From()].Add(e);
            E++;
        }

        public IEnumerable<DirectedEdge> Edges() {
            var bag = new List<DirectedEdge>();
            for (int v = 0; v < V; v++) {
                foreach (var e in adj[v]) {
                    bag.Add(e);
                }
            }
            return bag;
        }
    }
}
