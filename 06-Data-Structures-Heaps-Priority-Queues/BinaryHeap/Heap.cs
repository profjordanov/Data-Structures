using System;
using System.Collections.Generic;

namespace BinaryHeap
{
    public static class Heap<T> 
        where T : IComparable<T>
    {
        /// <summary>
        /// Performs an in-place sort of the given array
        /// in O(N logN) time complexity .
        /// </summary>
        /// <param name="arr"></param>
        public static void Sort(T[] arr)
        {
            var arrLen = arr.Length;
            for (var i = arrLen / 2; i > 0; i--)
            {
                // heapfy down element at i
                Down(array: arr,
                    current: i,
                    border: arrLen);
            }

            for (var i = arrLen - 1; i > 0; i--)
            {
                // swaps 0 with i
                Swap(arr, 0, i);
                // heapfy down element at 0
                Down(arr, 0, i);
            }
        }



        private static void Down(T[] array, int current, int border)
        {
            while (current < border / 2)
            {
                // gets the greater child
                var child = Left(current);

                if (child + 1 < border && IsLess(array,child,child + 1))
                {
                    child = child + 1;
                }

                // if current is greater than greater child 
                if (IsLess(array, child, current))
                {
                    break;
                }

                // swaps elements
                Swap(array,current,child);
                // updates index
                current = child;
            }
        }

        private static void Swap(T[] arr, int index, int other)
        {
            T temp = arr[index];
            arr[index] = arr[other];
            arr[other] = temp;
        }

        private static int Left(int index) => index * 2 + 1;

        private static bool IsLess(IReadOnlyList<T> arr, int other, int index) =>
            arr[other].CompareTo(arr[index]) < 0;
    }
}
