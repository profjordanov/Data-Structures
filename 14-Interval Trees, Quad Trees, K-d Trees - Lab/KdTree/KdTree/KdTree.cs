using System;

public class KdTree
{
	private const int K = 2; // 2D tree

    public Node Root { get; private set; }

    public bool Contains(Point2D point)
    {
	    var current = Root;
	    var depth = 0;

	    while (current != null)
	    {
		    if (point.CompareTo(current.Point) == 0)
		    {
			    return true;
		    }

		    var compare = CompareByDimension(current, point, depth);
		    if (compare < 0)
		    {
			    current = current.Left;
		    }
		    else if(compare > 0)
		    {
			    current = current.Right;
		    }

		    depth++;
	    }

	    return false;
    }

    public void Insert(Point2D point) =>
		Root = Insert(Root, point, 0);

    public void EachInOrder(Action<Point2D> action) =>
	    EachInOrder(Root, action);

	private Node Insert(Node node, Point2D point, int depth)
    {
	    if(node == null)
	    {
		    return new Node(point);
	    }

	    var compare = CompareByDimension(node, point, depth);
	    if(compare < 0)
	    {
		    node.Left = Insert(node.Left, point, depth + 1);
	    }
	    else if(compare > 0)
	    {
		    node.Right = Insert(node.Right, point, depth + 1);
	    }

	    return node;
	}

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrder(node.Left, action);
        action(node.Point);
        EachInOrder(node.Right, action);
    }

    private int CompareByDimension(Node node, Point2D point, int depth)
    {
	    var compare = depth % K;

	    compare = compare == 0 ? point.X.CompareTo(node.Point.X) : point.Y.CompareTo(node.Point.Y);

	    return compare;
    }
}
