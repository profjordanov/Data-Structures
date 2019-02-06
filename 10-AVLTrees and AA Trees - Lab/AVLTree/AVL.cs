using System;

namespace AVLTree
{
	/// <summary>
	/// AVL tree is a self-balancing binary-search tree.
	/// Height of two subtrees can differ by at most 1.
	/// Height difference is measured by a balance factor (BF).
	/// - BF(Tree) = Height(Left) – Height(Right).
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AVL<T> 
		where T : IComparable<T>
	{
		/// <summary>
		/// Returns the root of the AVL tree. 
		/// </summary>
		public Node<T> Root { get; private set; }

		/// <summary>
		/// Checks if an element exists.
		/// </summary>
		/// <param name="item"></param>
		public bool Contains(T item)
		{
			var node = Search(Root, item);
			return node != null;
		}

		/// <summary>
		/// Inserts an item into the tree.
		/// Running time: O(log n).
		/// </summary>
		/// <param name="item"></param>
		public void Insert(T item) => Root = Insert(Root, item);

		/// <summary>
		/// Performs an action in order on each element. 
		/// </summary>
		/// <param name="action"></param>
		public void EachInOrder(Action<T> action) => EachInOrder(Root, action);

		private static Node<T> Insert(Node<T> node, T item)
		{
			if(node == null)
			{
				return new Node<T>(item);
			}

			var cmp = item.CompareTo(node.Value);
			if(cmp < 0)
			{
				node.Left = Insert(node.Left, item);
			}
			else if(cmp > 0)
			{
				node.Right = Insert(node.Right, item);
			}
			// Rebalance
			node = Balance(node);

			// Update height
			UpdateHeight(node);

			return node;
		}

		/// <summary>
		/// Running time: O(log n).
		/// </summary>
		/// <param name="node"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		private static Node<T> Search(Node<T> node, T item)
		{
			if (node == null)
			{
				return null;
			}

			int cmp = item.CompareTo(node.Value);
			if (cmp < 0)
			{
				return Search(node.Left, item);
			}

			if (cmp > 0)
			{
				return Search(node.Right, item);
			}

			return node;
		}

		private static void EachInOrder(Node<T> node, Action<T> action)
		{
			if (node == null)
			{
				return;
			}

			EachInOrder(node.Left, action);
			action(node.Value);
			EachInOrder(node.Right, action);
		}

		// Finds a node's height
		private static int Height(Node<T> node) => node?.Height ?? 0;

		// Updates a node's height 
		private static int UpdateHeight(Node<T> node) => node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;

		// Rotations
		private static Node<T> RotateLeft(Node<T> node)
		{
			var right = node.Right;
			node.Right = right.Left;
			right.Left = node;

			UpdateHeight(node);

			return right;
		}

		private static Node<T> RotateRight(Node<T> node)
		{
			var left = node.Left;
			node.Left = left.Right;
			left.Right = node;

			UpdateHeight(node);

			return left;
		}

		// Balancing
		private static Node<T> Balance(Node<T> node)
		{
			var balance = Height(node.Left) - Height(node.Right);
			if (balance < -1) // right child is heavy
			{
				// rotate node left
				var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
				if (childBalance > 0)
				{
					node.Right = RotateRight(node.Right);
				}

				node = RotateLeft(node);
			}
			else if (balance > 1) // left child is heavy
			{
				// rotate node right
				var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
				if (childBalance < 0)
				{
					node.Left = RotateLeft(node.Left);
				}

				node = RotateRight(node);
			}

			return node;
		}
	}
}
