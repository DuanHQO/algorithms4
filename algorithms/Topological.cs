using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class Topological {

        public IEnumerable<int> Order;

        public Topological(Digraph G) {
            var cycleFinder = new DirectedCycle(G);
            if (!cycleFinder.HasCycle()) {
                var dfs = new DepthFirstOrder(G);
                Order = dfs.ReversePost;
            }
        }

        public Topological(EdgeWeightedDigraph G) {
            var cycleFinder = new DirectedCycle(G);
            if (!cycleFinder.HasCycle()) {
                var dfs = new DepthFirstOrder(G);
                Order = dfs.ReversePost;
            }
        }

        public bool IsDAG() {
            return Order != null;
        }

        public static void Main(string[] args) {
            var filename = args[0];
            var separator = args[1].ToCharArray()[0];

            var sg = new SymbolGraph(filename, separator);
            var top = new Topological(sg.GetDirG());
            foreach (var v in top.Order) {
                Console.WriteLine(sg.GetName(v));
            }
        }
    }
}
