using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class Digraph {

        public int V { get; private set; }
        public int E { get; private set; }
        private List<int>[] adj;

        public Digraph(int v) {
            this.V = v;
            this.E = 0;
            adj = new List<int>[v];
            for (int i = 0; i < v; i++) {
                adj[i] = new List<int>();
            }
        }

        public Digraph(StreamReader reader) : this(reader.Read()) {
            var e = reader.Read();
            for (int i = 0; i < e; i++) {
                var v = reader.Read();
                var w = reader.Read();
                AddEdge(v, w);
            }
        }

        //v->w
        public void AddEdge(int v, int w) {
            adj[v].Add(w);
            E++;
        }

        //返回由每个定点指出的边所连接的定点
        public IEnumerable<int> Adj(int v) {
            return adj[v];
        }

        //返回一个图的副本，但将其中所有边的方向反转
        public Digraph Reverse() {
            var r = new Digraph(V);
            for (int v = 0; v < V; v++) {
                foreach (var item in adj[v]) {
                    r.AddEdge(item, v);
                }
            }
            return r;
        }

        public override string ToString() {
            return "";
        }
    }
}
