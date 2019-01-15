using System;

namespace Trees
{
    /// <summary>
    /// Implements a binary tree node.
    /// Hold: the node value  + left and right child nodes
    /// (both of them are optional and can be null).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryTree<T>
    {
        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }
        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
        {
        }

        public void PrintIndentedPreOrder(int indent = 0)
        {
            throw new NotImplementedException();
        }

        public void EachInOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void EachPostOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}
