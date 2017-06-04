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
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }

        T item = this.Head.Value;

        this.Head = this.Head.Next;

        this.Count--;

        if (IsEmpty())
        {
            this.Tail = null;
        }

        return item;
    }

    public T RemoveLast()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }

        T item = this.Tail.Value;

        if (this.Count == 1)
        {
            this.Head = this.Tail = null;
        }
        else
        {
            Node newTail = this.GetSecondToLast();
            newTail.Next = null;
            this.Tail = newTail;
        }

        this.Count--;


        return item;
    }

    private Node GetSecondToLast()
    {
         Node current = this.Head;
         while (current.Next != this.Tail)
         {
             current = current.Next;
         }

        return current;
    }

    private bool IsEmpty()
    {
        if (this.Count == 0)
        {
            return true;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node current = this.Head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
