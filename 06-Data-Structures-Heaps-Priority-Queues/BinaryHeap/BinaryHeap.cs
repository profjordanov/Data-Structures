using System;
using System.Collections.Generic;

namespace BinaryHeap
{
    public class BinaryHeap<T> 
        where T : IComparable<T>
    {
        private List<T> _heap;

        public BinaryHeap()
        {
            // TODO
        }

        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// O(logN)
        /// </summary>
        /// <param name="item"></param>
        public void Insert(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// O(1)
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            throw new NotImplementedException();
        }

        public T Pull()
        {
            throw new NotImplementedException();
        }
    }
}
