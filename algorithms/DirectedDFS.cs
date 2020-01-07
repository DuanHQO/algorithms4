using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class DirectedDFS {
        public bool[] Marked { get; private set; }

        public DirectedDFS(Digraph G, int s) {
            Marked = new bool[G.V];
            DFS(G, s);
        }

        public DirectedDFS(Digraph G, IEnumerable<int> sources) {
            Marked = new bool[G.V];
            foreach (var item in sources) {
                if (!Marked[item])
                    DFS(G, item);
            }
        }

        private void DFS(Digraph G, int s) {
            Marked[s] = true;
            foreach (var item in G.Adj(s)) {
                if (!Marked[item])
                    DFS(G, item);
            }
        }

        public static void Main(string[] args) {
            var G = new Digraph(new StreamReader(args[0]));
            var sources = new List<int>();
            for (int i = 1; i < args.Length; i++) {
                sources.Add(int.Parse(args[i]));
            }
            var reachable = new DirectedDFS(G, sources);
            for (int v = 0; v < G.V; v++) {
                if(reachable.Marked[v])
                    Console.Write(v + " ");
                Console.WriteLine();
            }
        }
    }
}
