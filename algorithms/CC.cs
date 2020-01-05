using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class CC {

        private bool[] marked;
        private int[] id;
        private int count;


        public CC(Graph G) {
            marked = new bool[G.V];
            id = new int[G.V];
            for (int s = 0; s < G.V; s++) {
                if (!marked[s]) {
                    DFS(G, s);
                    count++;
                }
            }
        }

        private void DFS(Graph G, int v) {
            marked[v] = true;
            id[v] = count;
            foreach (var item in G.GetAdj(v)) {
                if (!marked[item]) {
                    DFS(G, item);
                }
            }
        }

        public bool Connected(int v, int w) {
            return id[v] == id[w];
        }

        public int GetId(int v) {
            return id[v];
        }

        public int GetCount() {
            return count;
        }

        public static void main(string[] args) {
            Graph G = new Graph(new StreamReader(args[0]));
            CC cc = new CC(G);

            var M = cc.GetCount();
            Console.WriteLine(M + " components"); ;
            var components = new List<int>[M];
            for (int i = 0; i < M; i++) {
                components[i] = new List<int>();
            }
            for (int v = 0; v < G.V; v++) {
                components[cc.GetId(v)].Add(v);
            }
            for (int i = 0; i < M; i++) {
                foreach (var item in components[i]) {
                    Console.WriteLine(item + " ");
                    Console.WriteLine();
                }
            }
        }
    }
}
