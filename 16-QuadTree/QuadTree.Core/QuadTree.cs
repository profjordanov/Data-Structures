using System;
using System.Collections.Generic;
using System.Linq;

namespace QuadTree.Core
{
	/// <summary>
	/// Space-partitioning tree that divides 2D space into quads (regions of 2D space). 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>
	/// - The root represents the whole 2D world.
	/// - A node has either 0 or 4 children.
	/// </remarks>
	public class QuadTree<T> 
		where T : IBoundable
	{
		public const int DefaultMaxDepth = 5;
		private const int ChildrenCount = 4;

		public readonly int MaxDepth;

		private readonly Node<T> _root;

		public QuadTree(int width, int height, int maxDepth = DefaultMaxDepth)
		{
			_root = new Node<T>(0, 0, width, height);
			Bounds = _root.Bounds;
			MaxDepth = maxDepth;
		}

		public int Count { get; private set; }

		public Rectangle Bounds { get; private set; }

		public void ForEachDfs(Action<List<T>, int, int> action) => ForEachDfs(_root, action);

		public bool Insert(T item)
		{
			// Item is outside quadtree bounds
			if(!item.Bounds.IsInside(Bounds))
			{
				return false;
			}

			// Add item to deepest node
			var deepestNode = GetDeepestNode(item.Bounds, out int depth);
			deepestNode.Items.Add(item);
			Count++;

			// Split deepest node if needed
			TrySplitNode(deepestNode, depth);

			return true;
		}
		public List<T> Report(Rectangle bounds)
		{
			var collisions = new List<T>();
			GetCollisions(_root, bounds, collisions);
			return collisions;
		}

		private Node<T> GetDeepestNode(Rectangle itemBounds, out int depth)
		{
			var deepestNode = _root; // deepest node that contains item
			depth = 1;

			while (deepestNode.Children != null)
			{
				var quadrant = GetQuadrant(deepestNode, itemBounds);

				if (quadrant == -1)
				{
					break;
				}

				deepestNode = deepestNode.Children[quadrant];
				depth++;
			}

			return deepestNode;
		}

		/// <param name="node"></param>
		/// <param name="itemBounds"></param>
		/// <returns>Respective quadrant (from 0 to 3).</returns>
		private static int GetQuadrant(Node<T> node, Rectangle itemBounds)
		{
			var nodeBounds = node.Bounds;
			var verticalMidpoint = nodeBounds.MidX;
			var horizontalMidpoint = nodeBounds.MidY;

			var inTopQuadrant = nodeBounds.Y1 <= itemBounds.Y1 && 
			                    itemBounds.Y2 <= horizontalMidpoint;
			var inBottomQuadrant = horizontalMidpoint <= itemBounds.Y1 && 
			                       itemBounds.Y2 <= nodeBounds.Y2;
			var inLeftQuadrant = nodeBounds.X1 <= itemBounds.X1 && 
			                     itemBounds.X2 <= verticalMidpoint;
			var inRightQuadrant = verticalMidpoint <= itemBounds.X1 && 
			                      itemBounds.X2 <= nodeBounds.X2;

			// Clock-wise quadrants
			if(inRightQuadrant)
			{
				if (inTopQuadrant)
				{
					return 0;
				}

				if (inBottomQuadrant)
				{
					return 1;
				}
			}

			if (!inLeftQuadrant)
			{
				return -1; // does not fit any quadrant
			}

			if (inBottomQuadrant)
			{
				return 2;
			}

			if (inTopQuadrant)
			{
				return 3;
			}

			return -1; // does not fit any quadrant
		}

		/// <summary>
		/// Splits the node into 4 quadrants if the node has reached its max capacity for items.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="nodeDepth"></param>
		private void TrySplitNode(Node<T> node, int nodeDepth)
		{
			if (!(nodeDepth < MaxDepth &&
			      node.ShouldSplit))
			{
				return;
			}

			// Split node in 4 children & transfer items to children
			SplitNode(node);
			TransferItemsToChildren(node);
				
			// Split children if they contain max items 
			foreach(var child in node.Children)
			{
				TrySplitNode(child, nodeDepth + 1);
			}
		}

		private static void SplitNode(Node<T> node)
		{
			var nodeBounds = node.Bounds;
			var leftWidth = nodeBounds.Width / 2;
			var rightWidth = nodeBounds.Width - leftWidth;
			var topHeight = nodeBounds.Height / 2;
			var bottomHeight = nodeBounds.Height - topHeight;

			// Split node in 4 quadrants & initialize children (clock-wise)
			node.Children = new Node<T>[ ChildrenCount ];
			node.Children[ 0 ] = new Node<T>(nodeBounds.MidX, nodeBounds.Y1, rightWidth, topHeight);
			node.Children[ 1 ] = new Node<T>(nodeBounds.MidX, nodeBounds.MidY, rightWidth, bottomHeight);
			node.Children[ 2 ] = new Node<T>(nodeBounds.X1, nodeBounds.MidY, leftWidth, bottomHeight);
			node.Children[ 3 ] = new Node<T>(nodeBounds.X1, nodeBounds.Y1, leftWidth, topHeight);
		}

		private static void TransferItemsToChildren(Node<T> node)
		{
			for (var i = 0; i < node.Items.Count; i++)
			{
				var item = node.Items[i];
				var quadrant = GetQuadrant(node, item.Bounds);

				// If item fits in child node => remove item from parent & transfer to child
				if (quadrant == -1)
				{
					continue;
				}
				node.Items.Remove(item);
				node.Children[ quadrant ].Items.Add(item);
				i--;
			}
		}

		private static void ForEachDfs(Node<T> node, Action<List<T>, int, int> action, int depth = 1, int quadrant = 0)
		{
			if (node == null)
			{
				return;
			}

			if (node.Items.Any())
			{
				action(node.Items, depth, quadrant);
			}

			if (node.Children == null)
			{
				return;
			}

			for (var i = 0; i < node.Children.Length; i++)
			{
				ForEachDfs(node.Children[i], action, depth + 1, i);
			}
		}

		private static void GetCollisions(Node<T> node, Rectangle itemBounds, List<T> results)
		{
			var quadrant = GetQuadrant(node, itemBounds);

			// Item does not fit in any quadrant => return current node collision candidates
			if(quadrant == -1)
			{
				GetSubtreeItems(node, itemBounds, results);
			}
			else
			{
				// Check for collision candidates in child
				if(node.Children != null)
				{
					GetCollisions(node.Children[ quadrant ], itemBounds, results);
				}

				// Add all items that do not fit in any child node to collision candidates
				results.AddRange(node.Items);
			}
		}

		private static void GetSubtreeItems(Node<T> node, Rectangle itemBounds, List<T> results)
		{
			if(node.Children != null)
			{
				foreach(var child in node.Children)
				{
					if(child.Bounds.Intersects(itemBounds))
					{
						GetSubtreeItems(child, itemBounds, results);
					}
				}
			}

			results.AddRange(node.Items);
		}
	}
}
