using System;
using System.Collections.Generic;

using static System.Console;

namespace Trees
{
    /// <summary>
    /// Implements a tree (a node that holds a value and multiple child nodes). 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T>
    {
        public T Value { get; set; }

        public IList<Tree<T>> Children { get; private set; }

        public Tree(T value, params Tree<T>[] children)
        {
            Value = value;
            Children = new List<Tree<T>>(children);
        }

        /// <summary>
        /// Displays values of the tree nodes on the Console. 
        /// </summary>
        /// <param name="indent"></param>
        public void Print(int indent = 0)
        {
            // Prints the current node value (indented a few spaces on the right).
            Write(new string(' ', indent * 2));
            WriteLine(Value);
            // Calls the Print() method recursively to print all child nodes of the current node.
            foreach (var child in Children)
            {
                child.Print(indent + 1);
            }
        }

        /// <summary>
        /// Traverses the tree recursively from its root to its leaves
        /// and invokes the provided action function for each visited tree node. 
        /// </summary>
        /// <param name="action"></param>
        public void Each(Action<T> action)
        {
            // Process the current node value 
            action(Value);
            // Calls the Each() method recursively to process
            // all child nodes of the current node
            foreach (var child in Children)
            {
                child.Each(action);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>Result List</returns>
        public IEnumerable<T> OrderDfs()
        {
            var result = new List<T>();
            Dfs(this, result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Result list.</returns>
        public IEnumerable<T> OrderBfs()
        {
            //collection for the algorithm 
            var result = new List<T>();
            //collection for the result
            var queue = new Queue<Tree<T>>();
            //enqueue the root node 
            queue.Enqueue(this);
            // while the queue is not empty
            while (queue.Count > 0)
            {
                //Dequeue nodded
                var current = queue.Dequeue();
                //Adds the node to the result list
                result.Add(current.Value);
                //adds all of current node children to the end of the queue
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;
        }

        private static void Dfs(Tree<T> tree, ICollection<T> result)
        {
            foreach (var child in tree.Children)
            {
                Dfs(child, result);
            }

            result.Add(tree.Value);
        }
    }
}
