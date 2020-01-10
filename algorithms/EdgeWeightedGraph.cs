using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    /// <summary>
    /// 加权边的API
    /// </summary>
    public class Edge : IComparable<Edge> {
        public int V { get; private set; }  //顶点之一
        public int W { get; private set; }  //另一个顶点
        public double Weight { get; private set; }  //边的权重


        public Edge(int v, int w, double weight) {
            V = v;
            W = w;
            Weight = weight;
        }

        public int Either() {
            return V;
        }

        public int Other(int vertex) {
            if (vertex == V)
                return W;
            else if (vertex == W)
                return V;
            else
                throw new Exception("Inconsistent edge");
        }

        public int CompareTo(Edge that) {
            if (this.Weight < that.Weight)
                return -1;
            else if (this.Weight > that.Weight)
                return 1;
            else
                return 0;
        }

        public override string ToString() {
            return string.Format("{0}-{1} {2:f2}", V, W, Weight);
        }
    }

    /// <summary>
    /// 加权无向图的API
    /// </summary>
    class EdgeWeightedGraph {


        public int V { get; private set; }
        public int E { get; private set; }
        public List<Edge>[] Adj { get; set; }

        public EdgeWeightedGraph(int v) {
            this.V = v;
            this.E = 0;
            Adj = new List<Edge>[v];
            for (int i = 0; i < v; i++) {
                Adj[i] = new List<Edge>();
            }
        }

        public EdgeWeightedGraph(StreamReader reader) {

        }

        //添加一条权重边
        private void AddEdge(Edge e) {
            var v = e.Either();
            var w = e.Other(v);
            Adj[v].Add(e);
            Adj[w].Add(e);
            E++;
        }

        public IEnumerable<Edge> GetEdges() {
            var b = new List<Edge>();
            for (int v = 0; v < V; v++) {
                foreach (var e in Adj[v]) {
                    if (e.Other(v) > v)
                        b.Add(e);
                }
            }
            return b;
        }

        public static void Main(string[] args) {
            var reader = new StreamReader(args[0]);
            var G = new EdgeWeightedGraph(reader);

            var mst = new MST(G);
            foreach (var e in mst.GetEdges()) {
                Console.WriteLine(e);
            }
            Console.WriteLine(mst.Weight());
        }
    }
}
