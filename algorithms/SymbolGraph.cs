using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class SymbolGraph {
        private Dictionary<string, int> dic;
        private string[] keys;
        private Graph G;

        public SymbolGraph(string stream, char sp) {
            dic = new Dictionary<string, int>();
            using (var reader = new StreamReader(stream)) {
                while (reader.Peek() > -1) {
                    var a = reader.ReadLine().Split(sp);
                    for (int i = 0; i < a.Length; i++) {
                        if (!dic.ContainsKey(a[i])) {
                            dic.Add(a[i], dic.Count);
                        }
                    }
                }
                keys = new string[dic.Count];
                foreach (var item in dic) {
                    keys[item.Value] = item.Key;
                }

                G = new Graph(dic.Count);
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                while (reader.Peek() > -1) {
                    var a = reader.ReadLine().Split(sp);
                    var v = dic[a[0]];
                    for (int i = 1; i < a.Length; i++) {
                        G.AddEdge(v, dic[a[i]]);
                    }
                }
            }
        }

        public bool Contains(string s) {
            return dic.ContainsKey(s);
        }

        public int GetIndex(string s) {
            return dic[s];
        }

        public string GetName(int v) {
            return keys[v];
        }

        public Graph GetG() {
            return G;
        }
    }
}
