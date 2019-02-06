using System;
using System.Collections.Generic;

namespace Trie
{
	/// <summary>
	/// Trie (prefix tree) is an ordered tree data structure.
	/// - Special tree structure used for fast multi-pattern matching.
	/// - Used to store a dynamic set where the keys are usually strings.
	/// Performance:
	/// - Fast search by prefix
	/// - High memory footprint
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	public class Trie<TValue>
	{
		private Node _root;

		public TValue GetValue(string key)
		{
			var x = GetNode(_root, key, 0);
			if(x == null || !x.IsTerminal)
			{
				throw new InvalidOperationException();
			}

			return x.Val;
		}

		public bool Contains(string key)
		{
			var node = GetNode(_root, key, 0);
			return node != null && node.IsTerminal;
		}

		public void Insert(string key, TValue val)
		{
			_root = Insert(_root, key, val, 0);
		}

		public IEnumerable<string> GetByPrefix(string prefix)
		{
			var results = new Queue<string>();
			var x = GetNode(_root, prefix, 0);

			Collect(x, prefix, results);

			return results;
		}

		private static Node GetNode(Node x, string key, int d)
		{
			if(x == null)
			{
				return null;
			}

			if(d == key.Length)
			{
				return x;
			}

			Node node = null;
			char c = key[ d ];

			if(x.Next.ContainsKey(c))
			{
				node = x.Next[ c ];
			}

			return GetNode(node, key, d + 1);
		}

		/// <summary>
		/// Recursive insertion functionality of the trie. 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="key"></param>
		/// <param name="val"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private static Node Insert(Node node, string key, TValue val, int index)
		{
			// checks if the given node is null
			if(node == null)
			{
				// create and assign a new node 
				node = new Node();
			}

			// checks if you are at the last symbol of the key 
			if(key.Length == index)
			{
				// makes the node terminal
				node.IsTerminal = true;
				// assigns the value
				node.Val = val;
				// returns the node
				return node;
			}

			var currentSymbol = key[ index ];
			var nextNode = GetNextNode(node, currentSymbol);

			node.Next[ currentSymbol ] = Insert(nextNode, key, val, index + 1);

			return node;
		}

		private static Node GetNextNode(Node node, char currentSymbol) =>
			node.Next.ContainsKey(currentSymbol) ? node.Next[ currentSymbol ] : null;

		private static void Collect(Node x, string prefix, Queue<string> results)
		{
			if(x == null)
			{
				return;
			}

			if(x.Val != null && x.IsTerminal)
			{
				results.Enqueue(prefix);
			}

			foreach(var c in x.Next.Keys)
			{
				Collect(x.Next[ c ], prefix + c, results);
			}
		}

		private class Node
		{
			public TValue Val;
			public bool IsTerminal;
			public readonly Dictionary<char, Node> Next = new Dictionary<char, Node>();
		}
	}
}