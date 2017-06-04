using System;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 4;

    private const int InitialCapacity = 16;


    private T[] elements;

    private int startIndex = 0;

    private int endIndex = 0;

    public int Count { get; private set; }

    public CircularQueue(int capacity = InitialCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.elements.Length)
        {
            this.Grow();
        }
        this.elements[this.endIndex] = element;
        this.endIndex = (this.endIndex + 1) % this.elements.Length;
        this.Count++;
    }

    private void Grow()
    {
        var newElements = new T[2 * this.elements.Length];
        this.CopyAllElementsTo(newElements);
        this.elements = newElements;
        this.startIndex = 0;
        this.endIndex = this.Count;
    }

    private void CopyAllElementsTo(T[] resultArr)
    {
        int sourceIndex = this.startIndex;
        int destinationIndex = 0;
        for (int i = 0; i < this.Count; i++)
        {
            resultArr[destinationIndex] = this.elements[sourceIndex];
            sourceIndex = (sourceIndex + 1) % this.elements.Length;
            destinationIndex++;
        }

    }

    private void Resize()
    {
        // TODO
        throw new NotImplementedException();
    }

    private void CopyAllElements(T[] newArray)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw  new InvalidOperationException("The queue is empty!");
        }

        var result = this.elements[startIndex];
        this.startIndex = (this.startIndex + 1) % this.elements.Length;
        this.Count--;
        return result;
    }

    public T[] ToArray()
    {
        var resultArr = new T[this.Count];
        CopyAllElementsTo(resultArr);
        return resultArr;
    }
}


public class Example
{
    public static void Main()
    {

        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
