using System;
using System.Collections.Generic;

using static System.Console;

namespace BasicTreeDataStructuresExercise
{
    /// <summary>
    /// Basic Tree Data Structure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T>
    {
        public T Value { get; set; }
        public Tree<T> Parent { get; set; }
        public IList<Tree<T>> Children { get; set; }

        public Tree(T value, params Tree<T>[] children)
        {
            Value = value;
            Children = new List<Tree<T>>(children);
        }

        /// <summary>
        /// Prints the tree values on the console.
        /// </summary>
        /// <param name="indent"></param>
        public void Print(int indent = 0)
        {
            Write(new string(' ', indent * 2));
            WriteLine(Value);
            foreach (var child in Children)
            {
                child.Print(indent + 1);
            }
        }

        /// <summary>
        /// Implements an action over the tree structure.
        /// </summary>
        /// <param name="action"></param>
        public void Each(Action<T> action)
        {
            action(Value);

            foreach (var child in Children)
            {
                child.Each(action);
            }
        }

        /// <summary>
        /// Orders the tree by `Depth-first search`.
        /// The algorithm starts at the root node
        /// and explores as far as possible along each branch before backtracking. 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> OrderDfs()
        {
            var result = new List<T>();
            Dfs(this, result);
            return result;
        }

        /// <summary>
        /// Orders the tree by `Breadth-first search`.
        /// It starts at the tree root and explores all of the neighbor nodes
        /// at the present depth prior to moving on to the nodes at the next depth level.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.TryDequeue(out var current))
            {
                result.Add(current.Value);
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
                Dfs(child,result);
            }

            result.Add(tree.Value);
        }

    }
}