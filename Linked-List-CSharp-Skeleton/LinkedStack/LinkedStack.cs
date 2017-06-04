using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedStack
{
    public class LinkedStack<T> : IEnumerable<T>
    {
        public int Count { get; set; }

        private StackNode Top;


        public void Push(T element)
        {

            this.Top = new StackNode(element, this.Top);
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            T result = this.Top.Value;
            this.Top = this.Top.Next;
            this.Count--;

            return result;
        }

        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            return this.Top.Value;
        }

        public T[] ToArray()
        {
            T[] array = new T[this.Count];
            StackNode current = this.Top;
            int index = this.Count - 1;

            while (current != null)
            {
                array[index--] = current.Value;
                current = current.Next;

            }

            return array;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class  StackNode
        {
            public T Value { get; set; }

            public StackNode Next { get; set; }

            public StackNode(T value, StackNode next)
            {
                Value = value;
                this.Next = next;
            }
        }
    }
}