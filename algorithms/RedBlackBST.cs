using System;
using System.Collections.Generic;

namespace algorithms
{
    class RedBlackBST<Key, Value> where Key : IComparable<Key> {
        private Node root;

        private const bool RED = true;
        private const bool BLACK = false;

        private class Node {
            public Key key;
            public Value value;
            public Node left, right;
            public int N;
            public bool color;

            public Node(Key key, Value value, int N, bool color) {
                this.key = key;
                this.value = value;
                this.N = N;
                this.color = color;
            }
        }

        public int size() {
            return size(root);
        }

        private int size(Node x) {
            if (x == null) {
                return 0;
            } else {
                return x.N;
            }
        }

        private bool IsRed(Node x) {
            if (x == null) {
                return false;
            }
            return x.color == RED;
        }

        private Node RotateLeft(Node h) {
            var x = h.right;
            h.right = x.left;
            x.left = h;
            x.color = h.color;
            h.color = RED;
            x.N = h.N;
            h.N = 1 + size(h.left) + size(h.right);
            return x;
        }

        private Node RotateRight(Node h) {
            var x = h.left;
            h.left = x.right;
            x.right = h;
            x.color = h.color;
            h.color = RED;
            x.N = h.N;
            h.N = 1 + size(h.left) + size(h.right);
            return x;
        }

        private void FlipColors(Node h) {
            h.color = !h.color;
            h.left.color = !h.left.color;
            h.right.color = !h.right.color;
        }

        public void Put(Key key, Value value) {
            root = Put(root, key, value);
            root.color = BLACK;
        }

        private Node Put(Node h, Key key, Value value) {
            if (h == null) {
                return new Node(key, value, 1, RED);
            }

            int cmp = key.CompareTo(h.key);
            if (cmp < 0) {
                h.left = Put(h.left, key, value);
            } else if (cmp > 0) {
                h.right = Put(h.right, key, value);
            } else {
                h.value = value;
            }

            if (IsRed(h.right) && !IsRed(h.left)) {
                h = RotateLeft(h);
            }

            if (IsRed(h.left) && IsRed(h.left.left)) {
                h = RotateRight(h);
            }

            if (IsRed(h.left) && IsRed(h.right)) {
                FlipColors(h);
            }

            h.N = 1 + size(h.left) + size(h.right);
            return h;
        }

        public Value Get(Key key) {
            return Get(root, key);
        }

        private Value Get(Node x, Key key) {
            if (x == null) {
                return default(Value);
            }

            var cmp = key.CompareTo(x.key);
            if (cmp < 0) {
                return Get(x.left, key);
            } else if (cmp > 0) {
                return Get(x.right, key);
            } else {
                return x.value;
            }
        }

        public Key Max() {
            return Max(root).key;
        }

        private Node Max(Node root) {
            if (root.right == null) {
                return root;
            }
            return Max(root.right);
        }

        public Key Min() {
            return Min(root).key;
        }

        private Node Min(Node root) {
            if (root.left == null) {
                return root;
            }
            return Min(root.left);
        }

        public Key Floor(Key key) {
            var x = Floor(root, key);
            if (x == null) {
                return default(Key);
            }
            return x.key;
        }

        private Node Floor(Node x, Key key) {
            if (x == null) {
                return null;
            }
            var cmp = key.CompareTo(x.key);
            if (cmp == 0) {
                return x;
            }

            if (cmp < 0) {
                return Floor(x.left, key);
            }
            var t = Floor(x.right, key);
            if (t != null) {
                return t;
            } else {
                return x;
            }
        }

        public Key Ceiling(Key key) {
            var x = Ceiling(root, key);
            if (x == null) {
                return default(Key);
            }
            return x.key;
        }

        private Node Ceiling(Node x, Key key) {
            if (x == null) {
                return null;
            }
            var cmp = key.CompareTo(x.key);
            if (cmp == 0) {
                return x;
            }

            if (cmp > 0) {
                return Ceiling(x.right, key);
            }

            var t = Ceiling(x.left, key);
            if (t != null) {
                return t;
            } else {
                return x;
            }
        }

        public Key Select(int k) {
            return Select(root, k).key;
        }

        private Node Select(Node x, int k) {
            if (x == null) {
                return null;
            }
            var t = size(x.left);
            if (t > k) {
                return Select(x.left, k);
            } else if (t < k) {
                return Select(x.right, k - t - 1);
            } else {
                return x;
            }
        }

        public int Rank(Key key) {
            return Rank(root, key);
        }

        private int Rank(Node x, Key key) {
            if (x == null) {
                return 0;
            }

            var cmp = key.CompareTo(x.key);
            if (cmp < 0) {
                return Rank(x.left, key);
            } else if (cmp > 0) {
                return 1 + size(x.left) + Rank(x.right, key);
            } else {
                return size(x.left);
            }
        }

        public IEnumerable<Key> Keys() {
            return Keys(Min(), Max());
        }

        private IEnumerable<Key> Keys(Key lo, Key hi) {
            Queue<Key> queue = new Queue<Key>();
            Keys(root, queue, lo, hi);
            return queue;
        }

        private void Keys(Node x, Queue<Key> queue, Key lo, Key hi) {
            if (x == null) {
                return;
            }

            var cmplo = lo.CompareTo(x.key);
            var cmphi = hi.CompareTo(x.key);
            if (cmplo < 0) {
                Keys(x.left, queue, lo, hi);
            }

            if (cmplo <= 0 && cmphi >= 0) {
                queue.Enqueue(x.key);
            }

            if (cmphi > 0) {
                Keys(x.right, queue, lo, hi);
            }
        }

        public bool IsBST() {
            return IsBST(root, default(Key), default(Key));
        }

        private bool IsBST(Node x, Key min, Key max) {
            if (x == null) {
                return true;
            }
            if (min != null && x.key.CompareTo(min) <= 0) {
                return false;
            }
            if (max != null && x.key.CompareTo(max) >= 0) {
                return false;
            }
            return IsBST(x.left, min, x.key) && IsBST(x.right, max, x.key);
        }

        private bool Is23() {
            return Is23(root);
        }

        private bool Is23(Node x) {
            if (x == null) {
                return false;
            }
            if (IsRed(x.right)) {
                return false;
            }
            if (x != root && IsRed(x) && IsRed(x.left)) {
                return false;
            }
            return Is23(x.left) && Is23(x.right);
        }

        public bool IsBalanced() {
            int black = 0;
            for (Node x = root; x != null; x = x.left) {
                if (!IsRed(x)) {
                    black++;
                }
            }
            return IsBalanced(root, black);
        }

        //任何一个叶节点到根节点的路径长度相同
        private bool IsBalanced(Node x, int black) {
            if (x == null) {
                return black == 0;
            }
            if (!IsRed(x)) {
                black--;
            }
            return IsBalanced(x.left, black) && IsBalanced(x.right, black);
        }

        public bool IsRedBlackBST() {
            return Is23() && IsBalanced() && IsBST();
        }

        private void MoveFlipColor(Node h) {
            h.color = BLACK;
            h.left.color = RED;
            h.right.color = RED;
        }

        private Node MoveRedLeft(Node h) {
            /**
            * 当前节点的左右子节点都是2-节点，左右节点需要从父节点中借一个节点
            * 如果该节点的右节点的左节点是红色节点，说明兄弟节点不是2-节点，可以从兄弟节点中借一个
            */
            MoveFlipColor(h); //从父节点中借一个
            if (IsRed(h.right.left)) { //判断兄弟节点，如果是非2-节点，也从兄弟节点中借一个
                h.right = RotateRight(h.right);
                h = RotateLeft(h);
                MoveFlipColor(h); //在兄弟那里借了一个之后，就需要还一个给父节点了，以为一开始从父节点借了一个
            }
            return h;
        }

        public void DeleteMin() {
            if (!IsRed(root.left) && !IsRed(root.right)) {
                root.color = RED; //如果根节点是2-节点，我们可以将根节点设为红色，这样才能进行后面的RemoveRedLeft操作，因为左子要从根节点借一个
            }
            root = DeleteMin(root);
            if (root != null) {
                root.color = BLACK; //借完后，我们将根节点的颜色复原
            }
        }

        private Node DeleteMin(Node h) {
            if (h.left == null) {
                return null;
            }
            if (!IsRed(h.left) && !IsRed(h.left.left)) {
                h = MoveRedLeft(h); //判断x的左节点是不是2-节点
            }
            h.left = DeleteMin(h.left);
            return Balance(h); //解除临时组成的4-节点
        }

        private Node Balance(Node h) {
            if (IsRed(h.right)) {
                h = RotateLeft(h);
            }
            if (IsRed(h.left) && IsRed(h.left.right)) {
                h = RotateRight(h);
            }
            if (IsRed(h.left) && IsRed(h.right)) {
                FlipColors(h);
            }
            h.N = size(h.left) + size(h.right) + 1;
            return h;
        }

        private Node MoveRedRight(Node h) {
            FlipColors(h);
            if (IsRed(h.left.left)) {
                h = RotateRight(h);
            }
            return h;
        }

        public void DeleteMax() {
            if (!IsRed(root.left) && !IsRed(root.right)) {
                root.color = RED;
            }
            root = DeleteMax(root);
            if (root != null) {
                root.color = BLACK;
            }
        }

        private Node DeleteMax(Node h) {
            if (IsRed(h.left)) {
                h = RotateRight(h);
            }

            if (h.right == null) {
                return null;
            }

            if (!IsRed(h.right) && !IsRed(h.right.left)) {
                h = MoveRedRight(h);
            }
            h.right = DeleteMax(h.right);
            return Balance(h);
        }

        public void Delete(Key key) {
            if (!IsRed(root.left) && !IsRed(root.right)) {
                root.color = RED;
            }
            root = Delete(root, key);
            if (root != null) {
                root.color = BLACK;
            }
        }

        private Node Delete(Node h, Key key) {
            if (key.CompareTo(h.key) < 0) {
                if (!IsRed(h.left) && !IsRed(h.left.left)) {
                    h = MoveRedLeft(h);
                }
                h.left = Delete(h.left, key);
            } else {
                if (IsRed(h.left)) {
                    h = RotateRight(h);
                }
                if (key.CompareTo(h.key) == 0 && h.right == null) {
                    return null;
                }
                if (!IsRed(h.right) && !IsRed(h.right.left)) {
                    h = MoveRedRight(h);
                }
                if (key.CompareTo(h.key) == 0) {
                    h.value = Get(h.right, Min(h.right).key);
                    h.key = Min(h.right).key;
                    h.right = DeleteMin(h.right);
                } else {
                    h.right = Delete(h.right, key);
                }
            }
            return Balance(h);
        }
    }
}
