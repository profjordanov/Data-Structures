using System;
using System.Collections.Generic;

/// <summary>
/// Modified Binary search tree that stores intervals.
/// - Efficient search for any or all intervals that overlap a given interval.
/// </summary>
public class IntervalTree
{
    private Node _root;

	/// <summary>
	/// Time complexity - O(log n).
	/// </summary>
	/// <param name="lo"></param>
	/// <param name="hi"></param>
	public void Insert(double lo, double hi) => _root = Insert(_root, lo, hi);

	public void EachInOrder(Action<Interval> action) => EachInOrder(_root, action);

	/// <returns>
	/// Any interval that intersects with a given lower and upper bound.
	///  Time complexity - O(log n).
	/// </returns>
	/// <param name="lo"></param>
	/// <param name="hi"></param>
	public Interval SearchAny(double lo, double hi)
	{
		var current = _root;
		while (current != null &&
		       !current.interval.Intersects(lo, hi))
		{
			if (current.left != null &&
			    current.left.max > lo)
			{
				current = current.left;
			}
			else
			{
				current = current.right;
			}
		}

		return current?.interval;
	}

	/// <returns>
	/// All intervals that intersect the given lower and upper bound.
	/// Time complexity - O(log n).
	/// </returns>
	/// <param name="lo"></param>
	/// <param name="hi"></param>
	public IEnumerable<Interval> SearchAll(double lo, double hi)
    {
		var result = new List<Interval>();
		SearchAll(_root, lo, hi, result);
		return result;
    }

	// HELPER METHODS
	// Search In Order
	private void SearchAll(Node node, double lo, double hi, List<Interval> result)
	{
		if (node == null)
		{
			return;
		}

		// Search Left
		if (node.left != null && 
		    node.left.max > lo)
		{
			SearchAll(node.left, lo, hi, result);
		}

		// Search Root
		if (node.interval.Intersects(lo, hi))
		{
			result.Add(node.interval);
		}

		// Search Right
		if (node.right != null && 
		    node.right.interval.Lo < hi)
		{
			SearchAll(node.right, lo, hi, result);
		}
	}

	/// <summary>
	/// Update the max endpoint whenever you insert (or delete/balance) a node.
	/// </summary>
	/// <param name="node"></param>
	/// <returns>Transformed node</returns>
	private Node UpdateMax(Node node)
	{
		var maxChild = GetMax(node.left, node.right);
		node.max = GetMax(node, maxChild).max;
		return node;
	}

	/// <returns>
	/// Node that has greater max endpoint.
	/// </returns>
	/// <param name="a"></param>
	/// <param name="b"></param>
	private static Node GetMax(Node a, Node b)
	{
		// guarding against null values
		if(a == null)
		{
			return b;
		}

		if (b == null)
		{
			return a;
		}

		return a.max > b.max ? a : b;
	}

    private static void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrder(node.left, action);
        action(node.interval);
        EachInOrder(node.right, action);
    }

    private static Node Insert(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        var cmp = lo.CompareTo(node.interval.Lo);
        if (cmp < 0)
        {
            node.left = Insert(node.left, lo, hi);
        }
        else if (cmp > 0)
        {
            node.right = Insert(node.right, lo, hi);
        }
        
        return node;
    }

	/// <summary>
	///  Basic unit  for <see cref="IntervalTree"/>.
	/// </summary>
	private class Node
    {
	    internal Interval interval;
	    internal double max;
	    internal Node right;
	    internal Node left;

	    public Node(Interval interval)
	    {
		    this.interval = interval;
		    max = interval.Hi;
	    }
    }
}
