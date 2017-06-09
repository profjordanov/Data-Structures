using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedStack
{
    public class LinkedStack<T>
    {
        private Node<T> firstNode;
        public int Count { get; private set; }

        public void Push(T element)
        {
            this.firstNode = new Node<T>(element, this.firstNode);
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Empty Stak!");
            }
            T newNode = firstNode.value;
            firstNode = firstNode.NextNode;
            return newNode;

        }

        public T[] ToArray()
        {
            T[] array = new T[this.Count];
            Node<T> current = this.firstNode;
            int index = this.Count - 1;
            while (current != null)
            {
                array[index--] = current.value;
                current = current.NextNode;
            }
            return array;
        }

        private class Node<T>
        {
            public T value;
            public Node<T> NextNode { get; set; }

            public Node(T value, Node<T> nextNode = null)
            {
                this.value = value;
                this.NextNode = nextNode;
            }

        }
    }
}
