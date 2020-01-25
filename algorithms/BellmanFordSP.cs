using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class BellmanFordSP {
        private double[] DistTo;
        private DirectedEdge[] EdgeTo;
        private bool[] OnQ;
        private Queue<int> queue;
        private int cost;
        private IEnumerable<DirectedEdge> Cycle;
        private int s;

        public BellmanFordSP(EdgeWeightedDigraph G, int s) {
            DistTo = new double[G.V];
            EdgeTo = new DirectedEdge[G.V];
            OnQ = new bool[G.V];
            queue = new Queue<int>();
            for (int v = 0; v < G.V; v++) {
                DistTo[v] = double.PositiveInfinity;
            }
            this.s = s;
            DistTo[s] = 0.0;
            queue.Enqueue(s);
            OnQ[s] = true;
            while (queue.Count > 0 && !HasNegativeCycle()) {
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
                //如果不存在从起点S可达的负权重环，算法会在进行v-1轮放松后结束，
                //因为所有最短路含有的边数都不大于v-1，所以进行V轮后就可以检查一次负权重环
                if(++cost % G.V == 0) {
                    FindNegativeCycle();
                }
            }
        }

        public double DistToW(int v) {
            if (HasNegativeCycle()) {
                throw new Exception("Negative cost cycle exists");
            }
            return DistTo[v];
        }

        private void FindNegativeCycle() {
            var V = EdgeTo.Length;
            var spt = new EdgeWeightedDigraph(V);
            for (int v = 0; v < V; v++) {
                if (EdgeTo[v] != null) {
                    spt.AddEdge(EdgeTo[v]);
                }
            }
            var cf = new EdgeWeightedCycleFinder(spt);
            Cycle = cf.Cycle;
        }

        public bool HasPathTo(int v) {
            return DistTo[v] < double.PositiveInfinity;
        }

        //最短路径的查询API
        public IEnumerable<DirectedEdge> PathTo(int v) {
            var path = new Stack<DirectedEdge>();
            for (var x = v; x != s; x = EdgeTo[x].From()) {
                path.Push(EdgeTo[x]);
            }
            return path;
        }

        //获取负权重环、如果没有则返回null
        public IEnumerable<DirectedEdge> NegativeCycle() {
            return Cycle;
        }

        //是否含有负权重环
        public bool HasNegativeCycle() {
            return Cycle != null;
        }

    }
}
