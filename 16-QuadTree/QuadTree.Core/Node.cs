using System;
using System.Collections.Generic;

namespace QuadTree.Core
{
	/// <summary>
	/// Holds node data in our <see cref="QuadTree{T}"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Node<T>
	{
		public const int MaxItemCount = 4;

		public Node(int x, int y, int width, int height)
		{
			Bounds = new Rectangle(x, y, width, height);
			Items = new List<T>();
		}

		public Rectangle Bounds { get; set; }

		public List<T> Items { get; set; }

		public Node<T>[] Children { get; set; }

		public bool ShouldSplit =>
			Items.Count == MaxItemCount &&
			Children == null;

		public override string ToString() =>
			Bounds.ToString();
	}
}
