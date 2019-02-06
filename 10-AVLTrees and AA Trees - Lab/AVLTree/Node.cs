using System;

namespace AVLTree
{
	public class Node<T>
		where T : IComparable<T>
	{
		public T Value;
		public Node<T> Left;
		public Node<T> Right;
		public int Height;

		public Node(T value)
		{
			Value = value;
			Height = 1;
		}
	}
}
