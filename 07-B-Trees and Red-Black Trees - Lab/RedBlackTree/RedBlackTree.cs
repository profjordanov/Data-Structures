using System;
using System.Collections.Generic;

namespace RedBlackTree
{
    public class RedBlackTree<T> : IBinarySearchTree<T>
        where T : IComparable
    {
        // Constants 
        private const bool Red = true;
        private const bool Black = false;


        private Node _root;

        private Node FindElement(T element)
        {
            Node current = _root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            Insert(node.Value);
            PreOrderCopy(node.Left);
            PreOrderCopy(node.Right);
        }

        private Node Insert(T element, Node node)
        {
            // Locate the node position
            if (node == null)
            {
                // Creates new red node
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                // Add the new node to the tree
                node.Left = Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = Insert(element, node.Right);
            }

            // Balances the tree if needed
            if (IsRed(node.Right) && !IsRed(node.Left))
            {
                node = RotateLeft(node);
            }

            if (IsRed(node.Right) && !IsRed(node.Left))
            {
                node = RotateLeft(node);
            }

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }

            if (IsRed(node.Left) && IsRed(node.Right))
            {
                node = FlipColors(node);
            }

            node.Count = 1 + Count(node.Left) + Count(node.Right);
            return node;
        }

        private void Range(Node node, Queue<T> queue, T startRange, T endRange)
        {
            if (node == null)
            {
                return;
            }

            int nodeInLowerRange = startRange.CompareTo(node.Value);
            int nodeInHigherRange = endRange.CompareTo(node.Value);

            if (nodeInLowerRange < 0)
            {
                Range(node.Left, queue, startRange, endRange);
            }
            if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
            {
                queue.Enqueue(node.Value);
            }
            if (nodeInHigherRange > 0)
            {
                Range(node.Right, queue, startRange, endRange);
            }
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            EachInOrder(node.Left, action);
            action(node.Value);
            EachInOrder(node.Right, action);
        }

        private int Count(Node node) => node?.Count ?? 0;

        private RedBlackTree(Node node)
        {
            PreOrderCopy(node);
        }

        public RedBlackTree()
        {
        }

        public void Insert(T element)
        {
            _root = Insert(element, _root);
        }

        public bool Contains(T element)
        {
            Node current = FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action)
        {
            EachInOrder(_root, action);
        }

        public IBinarySearchTree<T> Search(T element)
        {
            Node current = FindElement(element);

            return new RedBlackTree<T>(current);
        }

        public void DeleteMin()
        {
            if (_root == null)
            {
                throw new InvalidOperationException();
            }

            _root = DeleteMin(_root);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = DeleteMin(node.Left);
            node.Count = 1 + Count(node.Left) + Count(node.Right);

            return node;
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            Queue<T> queue = new Queue<T>();

            Range(_root, queue, startRange, endRange);

            return queue;
        }

        public virtual void Delete(T element)
        {
            if (_root == null)
            {
                throw new InvalidOperationException();
            }
            _root = Delete(element, _root);
        }

        private Node Delete(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }

            int compare = element.CompareTo(node.Value);

            if (compare < 0)
            {
                node.Left = Delete(element, node.Left);
            }
            else if (compare > 0)
            {
                node.Right = Delete(element, node.Right);
            }
            else
            {
                if (node.Right == null)
                {
                    return node.Left;
                }
                if (node.Left == null)
                {
                    return node.Right;
                }

                Node temp = node;
                node = FindMin(temp.Right);
                node.Right = DeleteMin(temp.Right);
                node.Left = temp.Left;

            }
            node.Count = Count(node.Left) + Count(node.Right) + 1;

            return node;
        }

        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return FindMin(node.Left);
        }

        public void DeleteMax()
        {
            if (_root == null)
            {
                throw new InvalidOperationException();
            }

            _root = DeleteMax(_root);
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }

            node.Right = DeleteMax(node.Right);
            node.Count = 1 + Count(node.Left) + Count(node.Right);

            return node;
        }

        public int Count() => Count(_root);

        public int Rank(T element)
        {
            return Rank(element, _root);
        }

        private int Rank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }

            int compare = element.CompareTo(node.Value);

            if (compare < 0)
            {
                return Rank(element, node.Left);
            }

            if (compare > 0)
            {
                return 1 + Count(node.Left) + Rank(element, node.Right);
            }

            return Count(node.Left);
        }

        public T Select(int rank)
        {
            Node node = Select(rank, _root);
            if (node == null)
            {
                throw new InvalidOperationException();
            }

            return node.Value;
        }

        private Node Select(int rank, Node node)
        {
            if (node == null)
            {
                return null;
            }

            int leftCount = Count(node.Left);
            if (leftCount == rank)
            {
                return node;
            }

            if (leftCount > rank)
            {
                return Select(rank, node.Left);
            }

            return Select(rank - (leftCount + 1), node.Right);
        }

        public T Ceiling(T element)
        {

            return Select(Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return Select(Rank(element) - 1);
        }

        /// Helper methods
        private bool IsRed(Node node) => node != null && node.Color;

        /// Rotation methods
        /// <summary>
        /// - Rotations are used to correct the balance of a tree.
        /// - Balance can be measured in height, depth, size etc. of subtrees.
        /// - Right subtree weights more.
        /// </summary>

        // Orient a right-leaning red link to lean left
        private Node RotateLeft(Node node)
        {
            Node temp = node.Right;
            node.Right = temp.Left;
            node.Left = node;

            temp.Color = node.Color;
            node.Color = Red;
            node.Count = 1 + Count(node.Left) + Count(node.Right);

            return temp;
        }

        //performs right rotation on a given node
        private Node RotateRight(Node node)
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;

            temp.Color = node.Color;
            node.Color = Red;
            node.Count = 1 + Count(node.Left) + Count(node.Right);

            return temp;
        }

        // preserves perfect black balance in the tree
        private Node FlipColors(Node node)
        {
            node.Left.Color = Black;
            node.Right.Color = Black;
            node.Color = Red;

            return node;
        }


        /// <summary>
        /// 
        /// </summary>
        private class Node
        {
            public Node(T value, bool color = Red)
            {
                Value = value;
                Color = color;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public int Count { get; set; }

            // Represents a color bit 
            public bool Color { get; set; }
        }       
    }

    public class Launcher
    {
        public static void Main(string[] args)
        {

        }
    }
}