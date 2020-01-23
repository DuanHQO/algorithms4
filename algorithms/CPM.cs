using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class CPM {

        public static void Main(string[] args) {

            var N = Console.Read();
            Console.ReadLine();
            var G = new EdgeWeightedDigraph(2 * N + 2);
            var s = 2 * N;
            var t = 2 * N + 1;
            for (int i = 0; i < N; i++) {
                var a = Console.ReadLine().Split(' ');
                var duation = double.Parse(a[0]);
                G.AddEdge(new DirectedEdge(i, i + N, duation));
                G.AddEdge(new DirectedEdge(s, i, 0.0));

                G.AddEdge(new DirectedEdge(i+N, t, 0.0));
                for (int j = 1; j < a.Length; j++) {
                    var successor = int.Parse(a[j]);
                    G.AddEdge(new DirectedEdge(i+N, successor, 0.0));
                }
            }

            var lp = new AcyclicLP(G, s);
            Console.WriteLine("Start times: ");
            for (int i = 0; i < N; i++) {
                Console.Write("{0} : {1:f1}\n", i, lp.DistToW(i));
            }
            Console.WriteLine("Finish time: {0:f1}", lp.DistToW(t));
        }


    }
}
