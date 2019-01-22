using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryHeap
{
    public class BinaryHeap<T> 
        where T : IComparable<T>
    {
        // container for all the elements
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
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            return _heap[0];
        }

        public T Pull()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Bubble up the element towards the top of the pile.
        /// </summary>
        /// <param name="index">Element index</param>
        private void HeapifyUp(int index)
        {
            //While the index is greater than 0 (the element has a parent)
            //and is greater than its parent, swap child with parent. 
            while (index > 0 && IsLess(Parent(index), index))
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }

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
