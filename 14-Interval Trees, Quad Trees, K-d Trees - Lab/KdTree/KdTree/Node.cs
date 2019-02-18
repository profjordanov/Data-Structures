public class Node
{
	public Node(Point2D point)
	{
		Point = point;
	}

	public Point2D Point { get; set; }
	public Node Left { get; set; }
	public Node Right { get; set; }
}