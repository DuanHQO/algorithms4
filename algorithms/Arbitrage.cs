using System;
using System.IO;

namespace algorithms {
    class Arbitrage {
        public static void Main(string[] args) {
            //using (var reader = new StreamReader("rates.txt")) {
            using (var reader = new StreamReader(args[0])) {
                var V = int.Parse(reader.ReadLine().Split(' ')[0]);
                var name = new string[V];
                var G = new EdgeWeightedDigraph(V);
                for (int v = 0; v < V; v++) {
                    var line = reader.ReadLine().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    name[v] = line[0];
                    for (int w = 0; w < V; w++) {
                        var rate = double.Parse(line[w + 1]);
                        if(rate != 1) {
                            var e = new DirectedEdge(v, w, -Math.Log(rate));
                            Console.WriteLine("{0}-->{1} : {2}", v, w, -Math.Log(rate));
                            G.AddEdge(e);
                        }
                    }
                }
                var spt = new BellmanFordSP(G, 0);
                if (spt.HasNegativeCycle()) {
                    var stake = 1000.0;
                    foreach (var e in spt.NegativeCycle()) {
                        Console.Write("{0:f5} {1} ", stake, name[e.From()]);
                        stake *= Math.Exp(-e.Weight);
                        Console.WriteLine("= {0:f5} {1}", stake, name[e.To()]);
                    }
                } else {
                    Console.WriteLine("No arbitrage opportunity");
                }
                //Console.Read();
            }
        }
    }
}
