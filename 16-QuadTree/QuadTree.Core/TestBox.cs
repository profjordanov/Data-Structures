using System;

namespace QuadTree.Core
{
	public class TestBox : IBoundable, IComparable<TestBox>
	{
		public const int Width = 10;
		public const int Height = 10;

		public static int id;

		public TestBox(int x, int y, int width = Width, int height = Height)
		{
			Id = id;
			id++;
			Bounds = new Rectangle(x, y, width, height);
		}

		public int Id { get; set; }

		public Rectangle Bounds { get; set; }

		public int CompareTo(TestBox other)
		{
			return Id.CompareTo(other.Id);
		}
	}
}
