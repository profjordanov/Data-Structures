using System;
using System.Collections.Generic;
using System.Linq;

public class PriorityQueue<T> where T : IComparable<T>
{
    private readonly List<T> _heap;

    public PriorityQueue()
    {
        _heap = new List<T>();
    }

    public int Count => _heap.Count;

    public void Enqueue(T item)
    {
        _heap.Add(item);
        HeapifyUp(_heap.Count - 1);
    }

    public T Peek()
    {
        return _heap[0];
    }

    public T Dequeue()
    {
        if (Count <= 0)
        {
            throw new InvalidOperationException();
        }

        T item = _heap[0];

        Swap(0, _heap.Count() - 1);
        _heap.RemoveAt(_heap.Count() - 1);
        HeapifyDown(0);

        return item;
    }

    public void DecreaseKey(T item)
    {
        var index = _heap.IndexOf(item);

        HeapifyUp(index);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && IsLess(index, Parent(index)))
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void HeapifyDown(int index)
    {
        while (index < _heap.Count / 2)
        {
            int child = Left(index);
            if (HasChild(child + 1) && IsLess(child + 1, child))
            {
                child = child + 1;
            }

            if (IsLess(index, child))
            {
                break;
            }

            Swap(index, child);
            index = child;
        }
    }

    private bool HasChild(int child)
    {
        return child < _heap.Count;
    }

    private static int Parent(int index)
    {
        return (index - 1) / 2;
    }

    private static int Left(int index)
    {
        return 2 * index + 1;
    }

    private static int Right(int index)
    {
        return Left(index) + 1;
    }

    private bool IsLess(int a, int b)
    {
        return _heap[a].CompareTo(_heap[b]) < 0;
    }

    private void Swap(int a, int b)
    {
        T temp = _heap[a];
        _heap[a] = _heap[b];
        _heap[b] = temp;
    }
}
