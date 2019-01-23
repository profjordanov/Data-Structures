using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryHeap
{
    /// <summary>
    /// Implementation of basic Binary heap.
    /// Binary heap is a complete binary tree.
    /// Every level, except the last, is completely filled.
    /// Last is filled from left to right.
    /// Efficiently stored in a resizing array <see cref="List{T}"/>.
    /// - Parent(i) = (i - 1) / 2
    /// - Left(i) = 2 * i + 1
    /// - Right(i) = 2 * i + 2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryHeap<T> 
        where T : IComparable<T>
    {
        // container for all elements
        private List<T> _heap;

        public BinaryHeap()
        {
            _heap = new List<T>();
        }

        /// <summary>
        /// Returns the size of the underlying data structure. 
        /// </summary>
        public int Count => _heap.Count;

        /// <summary>
        /// Adds an element at the end and then bubble it up to its correct position.
        /// Time complexity is O(logN).
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            _heap.Add(item);
            HeapifyUp(_heap.Count - 1);
        }

        /// <summary>
        /// Returns the maximum element without removing it.
        /// In a max heap, the max element should always stay at index 0
        /// Time complexity is O(1).
        /// Throws an <see cref="InvalidOperationException"/>
        /// if there is no elements.
        /// </summary>
        public T Peek()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException();
            }

            return _heap[0];
        }

        /// <summary>
        /// Heap Deletion.
        /// Time complexity is O(logN).
        /// Throws an Invalid Operation Exception.
        /// </summary>
        /// <returns></returns>
        public T Pull()
        {
            // check if there are elements in the heap
            if (Count <= 0)
            {
                throw new InvalidOperationException();
            }
            // saves the element on the top of the heap (index 0)
            T item = _heap[0];
            // swap the first and last elements
            Swap(0,Count - 1);
            // exclude the last element
            _heap.RemoveAt(Count - 1);
            // demote the one at the top until it has correct position 
            HeapifyDown(0);

            return item;
        }

        /// <summary>
        /// Bubble up the element towards the top of the pile.
        /// </summary>
        /// <param name="index">Element index</param>
        protected virtual void HeapifyUp(int index)
        {
            //While the index is greater than 0 (the element has a parent)
            //and is greater than its parent, swap child with parent. 
            while (index > 0 && IsLess(Parent(index), index))
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

        /// <summary>
        /// Demotes the element at a given index until it
        /// has no children or it is greater than its both children.
        /// </summary>
        /// <param name="index"></param>
        protected virtual void HeapifyDown(int index)
        {
            while (index < Count / 2)
            {
                var child = Left(index);

                if (HasChild(child + 1) && IsLess(child, child + 1))
                {
                    child = child + 1;
                }

                if (IsLess(child,index))
                {
                    break;
                }

                Swap(index, child);
                index = child;
            }
        }

        private bool HasChild(int childIndex) => childIndex < Count;

        private static int Left(int index) => index * 2 + 1;

        private bool IsLess(int other, int index) =>
            _heap[other].CompareTo(_heap[index]) < 0;

        private static int Parent(int index) => (index - 1) / 2;

        private void Swap(int index, int other)
        {
            T temp = _heap[index];
            _heap[index] = _heap[other];
            _heap[other] = temp;
        }
    }
}
