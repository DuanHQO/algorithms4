using System.Collections.Generic;
using System.IO;

namespace algorithms {
    class Digraph {

        public int V { get; private set; }
        public int E { get; private set; }
        public int[] InDegree { get; private set; }
        public int[] OutDegree { get; private set; }
        private List<int>[] adj;

        public Digraph(int v) {
            this.V = v;
            this.E = 0;
            InDegree = new int[v];
            OutDegree = new int[v];
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
                //不允许自环和平行边
                if(v != w && !adj[v].Contains(w)) {
                    AddEdge(v, w);
                }
            }
        }

        //v->w
        public void AddEdge(int v, int w) {
            adj[v].Add(w);
            InDegree[w] += 1;
            OutDegree[v] += 1;
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
                foreach (var w in adj[v]) {
                    r.AddEdge(w, v);
                }
            }
            return r;
        }

        public bool HasEdge(int v, int w) {
            foreach (var item in adj[v]) {
                if (item == w) return true;
            }
            return false;
        }

        public IEnumerable<int> Sources() {
            var sources = new List<int>();
            for (int i = 0; i < InDegree.Length; i++) {
                if(InDegree[i] == 0) {
                    sources.Add(i);
                }
            }
            return sources;
        }

        public IEnumerable<int> Sinks() {
            var sinks = new List<int>();
            for (int i = 0; i < OutDegree.Length; i++) {
                if (OutDegree[i] == 0) {
                    sinks.Add(i);
                }
            }
            return sinks;
        }

        public bool IsMap() {
            return false;
        }

        public override string ToString() {
            return "";
        }
    }
}
