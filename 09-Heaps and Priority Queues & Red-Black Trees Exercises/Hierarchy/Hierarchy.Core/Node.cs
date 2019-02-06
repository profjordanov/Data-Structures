using System.Collections.Generic;

namespace Hierarchy.Core
{
    /// <summary>
    /// Building block of <see cref="Hierarchy{T}"/> data structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class Node<T>
    {
        public Node(T value, Node<T> parent = null)
        {
            Value = value;
            Parent = parent;
        }

        public T Value { get; set; }

        public Node<T> Parent { get; set; }

        public List<Node<T>> Children { get; set; } = new List<Node<T>>();
    }
}