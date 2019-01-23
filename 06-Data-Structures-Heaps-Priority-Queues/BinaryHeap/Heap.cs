using System;

namespace BinaryHeap
{
    public static class Heap<T> 
        where T : IComparable<T>
    {
        public static void Sort(T[] arr)
        {
            var arrLen = arr.Length;
            for (var i = arrLen / 2; i > 0; i--)
            {
                Down(array: arr,
                    index: i,
                    border: arrLen);
            }
        }

        private static void Down(T[] array, int index, int border)
        {
            throw new NotImplementedException();
        }


    }
}
