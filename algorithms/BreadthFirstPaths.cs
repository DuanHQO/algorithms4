using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class BreadthFirstPaths {
        public bool[] marked { get; private set; }
        public int[] edgeTo { get; private set; }
        private int[] distTo;
        private int dis;
        private int s;

        public BreadthFirstPaths(Graph G, int s) {
            marked = new bool[G.V];
            edgeTo = new int[G.V];
            distTo = new int[G.V];
            this.s = s;
            BFS(G, s);
        }

        private void BFS(Graph G, int s) {
            Queue<int> queue = new Queue<int>();
            marked[s] = true;
            distTo[s] = 0;
            queue.Enqueue(s);
            while (queue.Count > 0) {
                var v = queue.Dequeue();
                foreach (var item in G.GetAdj(v)) {
                    if (!marked[item]) {
                        marked[item] = true;
                        edgeTo[item] = v;
                        distTo[item] = distTo[v] + 1;
                        queue.Enqueue(item);
                    }
                }
            }
        }

        public bool HasPathTo(int v) {
            return marked[v];
        }

        public IEnumerable<int> PathTo(int v) {
            if (!HasPathTo(v)) return null;
            Stack<int> path = new Stack<int>();
            for (int x = v; x != s; x = edgeTo[x]) {
                path.Push(x);
            }
            return path;
        }

        /// <summary>
        /// 从起点到目标点的距离
        /// </summary>
        /// <param name="v">目标点</param>
        /// <returns></returns>
        public int DistTo(int v) {
            if (!marked[v]) {
                return -1;
            }
            return distTo[v];
        }
    }
}
