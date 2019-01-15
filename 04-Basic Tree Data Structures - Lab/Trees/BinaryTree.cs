using System;
using static System.Console;

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

        public BinaryTree(
            T value, 
            BinaryTree<T> leftChild = null, 
            BinaryTree<T> rightChild = null)
        {
            Value = value;
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        /// <summary>
        /// Prints the tree in pre-order (root; left; right).
        /// Works recursively.
        /// </summary>
        /// <param name="indent"></param>
        public void PrintIndentedPreOrder(int indent = 0)
        {
            Write(new string(' ', indent * 2));
            // Prints the current node value 
            WriteLine(Value);
            // Calls the PrintIndentedPreOrder() method
            //recursively to print the left child of the current node (when exists)
            LeftChild?.PrintIndentedPreOrder(indent + 1);
            // Calls the PrintIndentedPreOrder() method
            //recursively to print the right child of the current node (when exists)
            RightChild?.PrintIndentedPreOrder(indent + 1);
        }

        /// <summary>
        /// Traverses the binary tree in in-order (left; root; right).
        /// Works recursively.
        /// </summary>
        /// <param name="action"></param>
        public void EachInOrder(Action<T> action)
        {
            // Processes the left child
            LeftChild?.EachInOrder(action);

            // Processes the current value
            action(Value);

            // Processes the right child
            RightChild?.EachInOrder(action);
        }

        /// <summary>
        /// Traverses the binary tree in in-order (left; right; root;).
        /// Works recursively.
        /// </summary>
        /// <param name="action"></param>
        public void EachPostOrder(Action<T> action)
        {
            // Processes the left child
            LeftChild?.EachPostOrder(action);

            // Processes the right child
            RightChild?.EachPostOrder(action);

            // Processes the current value
            action(Value);
        }
    }
}
