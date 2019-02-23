namespace QuadTree.Core
{
	/// <summary>
	/// Holds information about a 2D object (coordinates, size and some helper methods).
	/// </summary>
	public class Rectangle
	{
		public Rectangle(int x1, int y1, int width, int height)
		{
			X1 = x1;
			Y1 = y1;
			X2 = x1 + width;
			Y2 = y1 + height;
		}

		public int Y1 { get; set; }

		public int X1 { get; set; }

		public int Y2 { get; set; }

		public int X2 { get; set; }

		public int Width => X2 - X1;

		public int Height => Y2 - Y1;

		public int MidX => X1 + Width / 2;

		public int MidY => Y1 + Height / 2;

		public bool Intersects(Rectangle other)
		{
			return X1 <= other.X2 &&
			       other.X1 <= X2 &&
			       Y1 <= other.Y2 &&
			       other.Y1 <= Y2;
		}

		public bool IsInside(Rectangle other)
		{
			return X2 <= other.X2 &&
			       X1 >= other.X1 &&
			       Y1 >= other.Y1 &&
			       Y2 <= other.Y2;
		}

		public override string ToString()
		{
			return $"({X1}, {Y1}) .. ({X2}, {Y2})";
		}
	}
}
