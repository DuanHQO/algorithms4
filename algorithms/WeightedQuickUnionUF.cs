using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class WeightedQuickUnionUF {
        private int[] id;
        private int[] sz;
        public int Count { get; private set; }

        public WeightedQuickUnionUF(int N) {
            Count = N;
            id = new int[N];
            for (int i = 0; i < N; i++) {
                id[i] = i;
            }
            sz = new int[N];
            for (int i = 0; i < N; i++) {
                sz[i] = 1;
            }
        }

        public bool Connected(int p, int q) {
            return Find(p) == Find(q);
        }

        private int Find(int p) {
            while (p != id[p]) {
                p = id[p];
            }
            return p;
        }

        public void Union(int p, int q) {
            var i = Find(p);
            var j = Find(q);
            if (i == j) return;

            if (sz[i] < sz[j]) {
                id[i] = id[j];
                sz[j] += sz[i];
            } else {
                id[j] = id[i];
                sz[i] += sz[j];
            }
            Count--;
        }
    }
}
