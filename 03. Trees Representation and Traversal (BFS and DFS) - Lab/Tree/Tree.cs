namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Runtime.InteropServices;
    using System.Transactions;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }

        public Tree<T> Parent { get; private set; }

        public bool IsRootDeleted { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public ICollection<T> OrderBfs()
        {
            if (this.IsRootDeleted)
            {
                return new T[0];
            }

            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();

                result.Add(subtree.Value);

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();

            this.Dfs(this, result);

            return result;
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var searchedNode = this.FindBfs(parentKey);

            this.CheckEmptyNode(searchedNode);

            searchedNode._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            var searchedNode = this.FindBfs(nodeKey);

            this.CheckEmptyNode(searchedNode);

            foreach (var child in searchedNode.Children)
            {
                child.Parent = null;
            }

            searchedNode._children.Clear();

            var parentNode = searchedNode.Parent;

            if (parentNode == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                parentNode._children.Remove(searchedNode);
            }

            searchedNode.Value = default;
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstTree = this.FindBfs(firstKey);
            var secondTree = this.FindBfs(secondKey);

            this.CheckEmptyNode(firstTree);
            this.CheckEmptyNode(secondTree);

            var firstParent = firstTree.Parent;
            var secondParent = secondTree.Parent;

            if (firstParent == null)
            {
                SwapRoot(secondTree);
                return;
            }

            if (secondParent == null)
            {
                SwapRoot(firstTree);
                return;
            }

            firstTree.Parent = secondParent;
            secondTree.Parent = firstParent;

            var indexOfFirst = firstParent._children.IndexOf(firstTree);
            var indexOfSecond = secondParent._children.IndexOf(secondTree);

            firstParent._children[indexOfFirst] = secondTree;
            secondParent._children[indexOfSecond] = firstTree;
        }

        private void SwapRoot(Tree<T> tree)
        {
            this.Value = tree.Value;
            this._children.Clear();
            foreach (var child in tree.Children)
            {
                this._children.Add(child);
            }
        }

        private void Dfs(Tree<T> parent, List<T> result)
        {
            foreach (var child in parent.Children)
            {
                this.Dfs(child, result);
            }

            result.Add(parent.Value);
        }

        private Tree<T> FindBfs(T key)
        {
            if (this.Value.Equals(key))
            {
                return this;
            }

            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();

                if (subtree.Value.Equals(key))
                {
                    return subtree;
                }

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void CheckEmptyNode(Tree<T> node)
        {
            if (node == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
