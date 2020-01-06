using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class Graph {
    public int V { get; set; }
    public int E { get; set; }
    private List<int>[] adj;

    public Graph(int V) {
        this.V = V;
        adj = new List<int>[V];
        for (int v = 0; v < V; v++) {
            adj[v] = new List<int>();
        }
        
    }

    public Graph(StreamReader reader) : this(reader.Read()) {
        int e = reader.Read();
        for (int i = 0; i < e; i++) {
            var v = reader.Read();
            var w = reader.Read();
            AddEdge(v, w);
        }
    }

    public void AddEdge(int v, int w) {
        //不允许平行边或者自环
        if (v == w || HasEdge(v, w)) {
            return;
        }
        adj[v].Add(w);
        adj[w].Add(v);
        E++;
    }

    public List<int> GetAdj(int v) {
        return adj[v];
    }

    public bool HasEdge(int v, int w) {
        foreach (var item in adj[v]) {
            if (item == w) return true;
        }
        return false;
    }
}