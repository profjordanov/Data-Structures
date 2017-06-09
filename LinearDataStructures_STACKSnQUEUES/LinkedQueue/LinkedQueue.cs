using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedQueue
{
   public class LinkedQueue<T>
    {
        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            throw new NotImplementedException();
        }

        public T Dequeue()
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        private class QueueNode<T>
        {
        
             public T Value { get; private set; }
        
             public QueueNode<T> NextNode { get; set; }
        
             public QueueNode<T> PrevNode { get; set; }
        
        }

    }
}
