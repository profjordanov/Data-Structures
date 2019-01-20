using System;
using System.Collections.Generic;

namespace Trees
{
    /// <summary>
    /// Custom data structure that implements
    /// binary search trees (BST),also called ordered or sorted binary trees.
    /// - Elements in left subtree of x are less than x.
    /// - Elements in right subtree of x are > x.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinarySearchTree<T>
        where T : IComparable<T>
    {
        private Node<T> _root;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void Insert(T value)
        {
            if (_root == null)
            {
                _root = new Node<T>(value);
                return;
            }

            Node<T> parent = null;
            Node<T> current = _root;
            while (current != null)
            {
                parent = current;
                if (current.Value.CompareTo(value) > 0)
                {
                    current = current.Left;
                }
                else if(current.Value.CompareTo(value) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    return;
                }
            }

            Node<T> newNode = new Node<T>(value);
            if (value.CompareTo(parent.Value) < 0)
            {
                parent.Left = newNode;
            }
            else
            {
                parent.Right = newNode;
            }
        }

        /// <summary>
        /// Starts at the root and compare the searched value with the roots value.
        /// If the searched value is less it could be in the left subtree,
        /// if it is greater it could be in the right subtree.
        /// </summary>
        /// <param name="value"></param>
        public bool Contains(T value)
        {
            Node<T> current = _root;
            while (current != null)
            {
                if (value.CompareTo(current.Value) < 0)
                {
                    current = current.Left;
                }
                else if (value.CompareTo(current.Value) > 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current != null;
        }

        public void DeleteMin()
        {
            throw new NotImplementedException();
        }

        public BinarySearchTree<T> Search(T item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Recursively iterates over the tree nodes.
        /// </summary>
        /// <param name="action"></param>
        public void EachInOrder(Action<T> action) =>
            EachInOrder(_root, action);

        private static void EachInOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            EachInOrder(node.Left, action);
            action(node.Value);
            EachInOrder(node.Right, action);
        }
    }

    public class Launcher
    {
        public static void Main(string[] args)
        {
        
        }
    }
}