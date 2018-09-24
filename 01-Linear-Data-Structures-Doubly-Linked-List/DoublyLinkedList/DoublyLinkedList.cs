using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private ListNode head;
    private ListNode tail;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        var newHead = new ListNode(element);

        var oldHead = this.head;
        this.head = newHead;

        if (this.Count == 0)
        {
            this.tail = newHead;
        }
        else
        {
            this.head.Next = oldHead;
            oldHead.Previous = this.head;
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        var newTail = new ListNode(element);

        var oldTail = this.tail;
        this.tail = newTail;

        if (this.Count == 0)
        {
            this.head = newTail;
        }
        else
        {
            oldTail.Next = this.tail;
            this.tail.Previous = oldTail;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var removedElement = this.head.Value;

        this.head = this.head.Next;
        this.Count--;

        if (this.Count == 0)
        {
            this.tail = null;
        }
        else
        {
            this.head.Previous = null;
        }

        return removedElement;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var removedElement = this.tail.Value;

        this.tail = this.tail.Previous;
        this.Count--;

        if (this.Count == 0)
        {
            this.head = null;
        }
        else
        {
            this.tail.Next = null;
        }

        return removedElement;
    }

    public void ForEach(Action<T> action)
    {
        var currentNode = this.head;

        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.Next;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.head;

        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    public T[] ToArray()
    {
        var array = new T[this.Count];

        var current = this.head;
        for (int i = 0; i < this.Count; i++)
        {
            array[i] = current.Value;
            current = current.Next;
        }

        return array;
    }

    private class ListNode
    {
        public ListNode(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public ListNode Next { get; set; }

        public ListNode Previous { get; set; }
    }
}


class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
