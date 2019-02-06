using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hierarchy.Core
{
    /// <summary>
    /// Data structure that stores elements in a hierarchical order. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Hierarchy<T> : IHierarchy<T>
    {
        private readonly Node<T> _root;
        private readonly IDictionary<T, Node<T>> _nodes = new Dictionary<T, Node<T>>();

        public Hierarchy(T rootValue)
        {
            _root = new Node<T>(rootValue);
            _nodes[rootValue] = _root;
        }

        /// <summary>
        /// Returns the count of all elements in the hierarchy.
        /// </summary>
        public int Count => _nodes.Count;

        /// <summary>
        /// Adds child to the hierarchy as a child of element.
        /// o	Throws an exception if element does not exist in the hierarchy.
        /// o	Throws an exception if child already exists (duplicates are not allowed).
        /// </summary>
        /// <param name="parentValue"></param>
        /// <param name="childValue"></param>
        public void Add(T parentValue, T childValue)
        {
            if (!Contains(parentValue))
            {
                throw new ArgumentException($"Element {parentValue} does not exist in the hierarchy.");
            }

            if (Contains(childValue))
            {
                throw new ArgumentException($"Element {childValue} already exists - " +
                                            "Duplicates are not allowed.");
            }

            var parentNode = _nodes[parentValue];
            var childNode = new Node<T>(childValue,parentNode);
            parentNode.Children.Add(childNode);

            _nodes[childValue] = childNode;
        }

        /// <summary>
        /// Removes the element from the hierarchy. 
        /// </summary>
        /// <param name="element"></param>
        public void Remove(T element)
        {
            if (!Contains(element))
            {
                throw new ArgumentException($"Element {element} does not exist in the hierarchy.");
            }

            var nodeToRemove = _nodes[element];
            // if element is root node
            if (nodeToRemove.Parent == null)
            {
                throw new InvalidOperationException("Root node cannot be removed.");
            }

            var parent = nodeToRemove.Parent;
            var children = nodeToRemove.Children;

            // remove element
            parent.Children.Remove(nodeToRemove);
            _nodes.Remove(element);

            // if it has children, they become children of the element's parent.
            children.ForEach(node => node.Parent = parent);
            parent.Children.AddRange(children);
        }

        /// <param name="item"></param>
        /// <returns>
        /// Collection of all direct children of the element in order of their addition.
        /// </returns>
        public IEnumerable<T> GetChildren(T item)
        {
            if (!Contains(item))
            {
                throw new ArgumentException($"Element {item} does not exist in the hierarchy.");
            }

            return _nodes[item]
                .Children
                .Select(node => node.Value);
        }

        /// <summary>
        /// Returns the parent of the element. 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public T GetParent(T item)
        {
            if (!Contains(item))
            {
                throw new ArgumentException($"Element {item} does not exist in the hierarchy.");
            }

            var parentNode = _nodes[item].Parent;

            // Returns the default value for the type
            // (e.g. int → 0, string → null, etc.) if element has no parent.

            return parentNode == null
                ? default(T)
                : parentNode.Value;
        }

        /// <summary>
        /// Determines whether the element is present in the hierarchy. 
        /// </summary>
        /// <param name="value"></param>
        public bool Contains(T value) => _nodes.ContainsKey(value);

        /// <param name="other"></param>
        /// <returns>
        /// collection of all elements that are present in both hierarchies (order does not matter). 
        /// </returns>
        public IEnumerable<T> GetCommonElements(Hierarchy<T> other) =>
            _nodes.Keys.Where(other.Contains);

        /// <inheritdoc />
        /// <summary>
        /// enumerates over all elements in the hierarchy by levels. 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(_root);
            while (queue.Any())
            {
                var current = queue.Dequeue();
                yield return current.Value;

                current
                    .Children
                    .ForEach(ch => queue.Enqueue(ch));
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}