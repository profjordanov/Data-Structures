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
            // TODO
        }

        /// <summary>
        /// Returns the size of the underlying data structure. 
        /// </summary>
        public int Count => _heap.Count;

        /// <summary>
        /// Adds an element.
        /// Time complexity is O(logN).
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            _heap.Add(item);

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

        private void HeapifyUp(int index)
        {
            while (true)
            {
                
            }
        }

        private bool IsLess(int other, int index) =>
            _heap[other].CompareTo(_heap[index]) < 0;
    }
}
