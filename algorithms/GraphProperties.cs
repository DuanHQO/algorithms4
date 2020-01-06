using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorithms {
    class GraphProperties {
        private Graph G;


        public GraphProperties(Graph G) {
            this.G = G;
        }

        //v的离心率
        public int Eccentricity(int v) {
            return -1;
        }

        //G的直径
        public int Diameter() {
            return -1;
        }

        //G的半径
        public int Radius() {
            return -1;
        }

        //G的某个中点
        public int Center() {
            return -1;
        }
    }
}
