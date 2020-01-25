using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class DepthFirstOrder {
        public bool[] Marked;
        public Queue<int> Pre { get; private set; }
        public Queue<int> Post { get; private set; }
        public Stack<int> ReversePost { get; private set; }

        public DepthFirstOrder(EdgeWeightedDigraph G) {
            Pre = new Queue<int>();
            Post = new Queue<int>();
            ReversePost = new Stack<int>();

            Marked = new bool[G.V];
            for (int v = 0; v < G.V; v++) {
                if (!Marked[v])
                    DFS(G, v);
            }
        }

        public DepthFirstOrder(Digraph G) {
            Pre = new Queue<int>();
            Post = new Queue<int>();
            ReversePost = new Stack<int>();

            Marked = new bool[G.V];
            //for (int v = 0; v < G.V; v++) {
            //    if (!Marked[v])
            //        DFS(G, v);
            //}
        }

        private void DFS(EdgeWeightedDigraph g, int v) {
            Pre.Enqueue(v);
            Marked[v] = true;
            foreach (var w in g.adj[v]) {
                if (!Marked[w.To()])
                    DFS(g, w.To());
            }
            Post.Enqueue(v);
            ReversePost.Push(v);
        }

    }
}
