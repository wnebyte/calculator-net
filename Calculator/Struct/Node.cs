using System;
using System.Collections.Generic;

namespace Calculator.Struct
{
    public class Node<E>
    {
        public E Data { get; set; }

        public Node<E> Parent { get; set; }

        private readonly IList<Node<E>> children;

        public Node(E data) : this(data, null) { }

        public Node(E data, Node<E> parent)
        {
            this.Data = data;
            this.Parent = parent;
            this.children = new List<Node<E>>();
        }

        public void AddChild(Node<E> child)
        {
            child.Parent = this;
            children.Add(child);
        }

        public void AddChildren(IEnumerable<Node<E>> children)
        {
            foreach (Node<E> child in children)
            {
                AddChild(child);
            }
        }

        public bool IsRoot()
        {
            return (Parent == null);
        }

        public bool IsLeaf()
        {
            return (children.Count == 0);
        }

        public int GetHeight()
        {
            if (IsLeaf())
            {
                return 0;
            }

            var h = 0;
            foreach (Node<E> node in children)
            {
                h = Math.Max(h, node.GetHeight());
            }

            return h + 1;
        }

        public int GetDepth()
        {
            if (IsRoot())
            {
                return 0;
            }

            var d = 0;
            d = Math.Max(d, Parent.GetDepth());
            return d + 1;
        }

    }
}
