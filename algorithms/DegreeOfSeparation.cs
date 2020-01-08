using System;

namespace algorithms {
    class DegreeOfSeparation {
        public static void Main(string[] args) {
            var sg = new SymbolGraph(args[0], args[1].ToCharArray()[0]);
            var G = sg.GetG();

            var source = args[2];
            if (!sg.Contains(source)) {
                Console.WriteLine(source + " not in database.");
                return;
            }

            var s = sg.GetIndex(source);
            var bfs = new BreadthFirstPaths(G, s);

            var sink = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(sink)) {
                if (sg.Contains(sink)) {
                    var t = sg.GetIndex(sink);
                    if (bfs.HasPathTo(t)) {
                        foreach (var item in bfs.PathTo(t)) {
                            Console.WriteLine("  " + sg.GetName(item));
                        }
                    }
                } else {
                    Console.WriteLine("Not in database");
                }
                sink = Console.ReadLine();
            }
        }
    }
}
