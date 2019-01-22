using System;

namespace Trees
{
    /// <summary>
    /// Building block of <see cref="BinarySearchTree{T}"/> data structure.
    /// It has a value and a pointers to the left and right sub-tree's nodes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T>
        where T : IComparable<T>
    {
        public Node(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
    }
}