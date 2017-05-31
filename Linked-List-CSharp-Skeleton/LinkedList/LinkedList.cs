using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }
        public T Value { get; set; }
        public Node Next { get; set; }
    }

    public Node Head { get; private set; }
    public Node Tail { get; private set; }

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        Node old = Head;
        this.Head = new Node(item);
        this.Head.Next = old;
        if (IsEmpty())
        {
            Tail = Head;
        }

        Count++;
    }

    public void AddLast(T item)
    {
        Node old = Tail;
        this.Tail = new Node(item);
        if (IsEmpty())
        {
            this.Tail = this.Head;
        }
        else
        {
            old.Next = Tail;
        }

        Count++;
    }

    public T RemoveFirst()
    {
        // TODO: Throw exception if the list is empty
        throw new NotImplementedException();
    }

    public T RemoveLast()
    {
        // TODO: Throw exception if the list is empty
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        // TODO
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        // TODO
        throw new NotImplementedException();
    }
}
