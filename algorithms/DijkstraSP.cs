using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {

    public class DijkstraAllPairsSP {
        private DijkstraSP[] all;
        public DijkstraAllPairsSP(EdgeWeightedDigraph G) {
            all = new DijkstraSP[G.V];
            for (int v = 0; v < G.V; v++) {
                all[v] = new DijkstraSP(G, v);
            }
        }

        public IEnumerable<DirectedEdge> Path(int s, int t) {
            return all[s].PathTo(t);
        }

        public double DistToT(int s, int w) {
            return all[s].DistToW(w);
        }
    }

    class DijkstraSP {
        private DirectedEdge[] EdgeTo;
        private double[] DistTo;
        private SortedDictionary<int, double> pq;

        public DijkstraSP(EdgeWeightedDigraph G, int s) {
            EdgeTo = new DirectedEdge[G.V];
            DistTo = new double[G.V];
            pq = new SortedDictionary<int, double>();
            for (int v = 0; v < G.V; v++) {
                DistTo[v] = double.PositiveInfinity;
            }
            DistTo[0] = 0.0;
            pq.Add(s, 0.0);
            //核心就是每次选出权重最小点来继续搜索
            while (pq.Count > 0) {
                var min = pq.Min();
                Relax(G, min.Key);
            }
        }

        private void Relax(EdgeWeightedDigraph g, int v) {
            foreach (var e in g.adj[v]) {
                var w = e.To();
                if(DistTo[w] > DistTo[v] + e.Weight) {
                    DistTo[w] = DistTo[v] + e.Weight;
                    EdgeTo[w] = e;
                    if (pq.ContainsKey(w)) {
                        pq[w] = DistTo[w];
                    } else {
                        pq.Add(w, DistTo[w]);
                    }
                }
            }
        }

        public double DistToW(int v) {
            return DistTo[v];
        }

        public bool HasPathTo(int v) {
            return DistTo[v] < double.PositiveInfinity;
        }

        //获取从起点到目标点的路径
        public IEnumerable<DirectedEdge> PathTo(int v) {
            var path = new Stack<DirectedEdge>();
            for (int x = v; DistTo[v] != 0; x = EdgeTo[x].From()) {
                path.Push(EdgeTo[x]);
            }
            return path;
        }
    }
}
