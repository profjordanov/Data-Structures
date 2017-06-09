using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayStack
{
    public class ArrayStack<T>
    {
        private T[] elements;

        public int Count { get; private set; }


        private const int InitialCapacity = 16;

        public ArrayStack(int capacity = InitialCapacity)
        {
            this.elements = new T[capacity];
        }

        public void Push(T element)
        {
            if (this.Count >= this.elements.Length)
            {
                this.Grow();
            }
            this.elements[this.Count] = element;
            this.Count++;
        }

        public T Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }
            int index = this.Count - 1;
            T element = this.elements[index];
            this.elements[index] = default(T);
            this.Count--;

            return element;
        }

        public T[] ToArray()
        {
            var newArray = SubArray(this.elements, 0, this.Count - 1);
            return newArray;
        }

        private void Grow()
        {
            var newElements = new T[2 * this.elements.Length];
            Array.Copy(this.elements,newElements,this.Count);
            this.elements = newElements;
        }
        public static T[] SubArray<T>(T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
