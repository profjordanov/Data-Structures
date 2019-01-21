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

        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root) => Copy(root);

        /// <summary>
        /// Inserts {T} in <see cref="BinarySearchTree{T}"/>
        /// </summary>
        /// <param name="value"></param>
        public Node<T> Insert(T value)
        {
            // check if there are any elements in the tree
            if (_root == null)
            {
                // set the root
                _root = new Node<T>(value);
                return _root;
            }

            // traverse the tree, by holding reference
            // to both current node and its parent
            Node<T> parent = null;
            Node<T> current = _root;
            while (current != null)
            {
                // finds insertion node
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
                    return current;
                }
            }

            // inserts the new node
            Node<T> newNode = new Node<T>(value);
            if (value.CompareTo(parent.Value) < 0)
            {
                parent.Left = newNode;
            }
            else
            {
                parent.Right = newNode;
            }

            return newNode;
        }

        /// <summary>
        /// Starts at the root and compare the searched value with the roots value.
        /// If the searched value is less it could be in the left subtree,
        /// if it is greater it could be in the right subtree.
        /// </summary>
        /// <param name="value"></param>
        public bool Contains(T value)
        {
            Node<T> current = FindNodeByValue(value);
            return current != null;
        }

        /// <summary>
        /// Deletes node with minimum value.
        /// </summary>
        public void DeleteMin()
        {
            // Check if the root is null. 
            if (_root == null)
            {
                return;
            }

            // finds the parent of the min element
            Node<T> parent = null;
            Node<T> min = _root;

            while (min.Left != null)
            {
                parent = min;
                min = min.Left;
            }

            // sets parent left child to be the min's right child.
            if (parent == null)
            {
                _root = min.Right;
            }
            else
            {
                parent.Left = min.Right;
            }
        }

        /// <summary>
        /// is very similar to <see cref="Contains"/>. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public BinarySearchTree<T> Search(T item)
        {
            Node<T> current = FindNodeByValue(item);
            return current == null ? null : new BinarySearchTree<T>(current);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="endRange"></param>
        /// <returns></returns>
        public IEnumerable<T> Range(T startRange, T endRange)
        {
            Queue<T> queue = new Queue<T>();
            Range(_root, queue, startRange, endRange);
            return queue;
        }

        private void Range(Node<T> node, Queue<T> queue, T startRange, T endRange)
        {
            // bottom of the recursion
            if (node == null)
            {
                return;
            }

            //finds if the node is in the lower and higher range borders
            var nodeInLowerRange = startRange.CompareTo(node.Value);
            var nodeInUpperRange = endRange.CompareTo(node.Value);

            // in-order traversal for the items that are in the range
            // node value is bigger than the lower range
            if (nodeInLowerRange < 0)
            {
                // recursively goes to the left
                Range(node.Left, queue, startRange, endRange);
            }

            //If the element is between the recursive calls, adds it to the queue
            if (nodeInLowerRange <= 0 && nodeInUpperRange >= 0)
            {
                queue.Enqueue(node.Value);
            }

            // node value is smaller than the higher range border
            if (nodeInUpperRange > 0)
            {
                // go to the right
                Range(node.Right, queue, startRange, endRange);
            }
        }

        /// <summary>
        /// Recursively iterates over the tree nodes.
        /// </summary>
        /// <param name="action"></param>
        public void EachInOrder(Action<T> action) =>
            EachInOrder(_root, action);

        /// <summary>
        /// Recursively copy the elements in exactly the same way in
        /// which they exist in the parent tree (Pre-Order traversal):
        /// </summary>
        /// <param name="node"></param>
        private void Copy(Node<T> node)
        {
            if (node == null)
            {
                return;;
            }

            Insert(node.Value);
            Copy(node.Left);
            Copy(node.Right);
        }

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

        private Node<T> FindNodeByValue(T item)
        {
            Node<T> current = _root;
            // finds the element
            while (current != null)
            {
                if (item.CompareTo(current.Value) > 0)
                {
                    current = current.Right;
                }
                else if (item.CompareTo(current.Value) < 0)
                {
                    current = current.Left;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

    }

    public class Launcher
    {
        public static void Main(string[] args)
        {
        
        }
    }
}