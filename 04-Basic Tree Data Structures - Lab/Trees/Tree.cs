using System;
using System.Collections.Generic;

public class Tree<T>
{
    public Tree(T value, params Tree<T>[] children)
    {
    }

    public void Print(int indent = 0)
    {
        throw new NotImplementedException();
    }

    public void Each(Action<T> action)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> OrderDFS()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> OrderBFS()
    {
        throw new NotImplementedException();
    }
}
