using System;
using System.Collections.Generic;

namespace Trie
{
	public class Trie<TValue>
	{
		private Node _root;

		public TValue GetValue(string key)
		{
			var x = GetNode(_root, key, 0);
			if (x == null || !x.IsTerminal)
			{
				throw new InvalidOperationException();
			}

			return x.Val;
		}

		public bool Contains(string key)
		{
			var node = GetNode(this._root, key, 0);
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

			this.Collect(x, prefix, results);
        
			return results;
		}

		private Node GetNode(Node x, string key, int d)
		{
			if (x == null)
			{
				return null;
			}

			if (d == key.Length)
			{
				return x;
			}

			Node node = null;
			char c = key[d];

			if (x.Next.ContainsKey(c))
			{
				node = x.Next[c];
			}

			return GetNode(node, key, d + 1);
		}

		private Node Insert(Node x, string key, TValue val, int d)
		{
			//ToDo: Create insert
			throw new NotImplementedException();
		}

		private void Collect(Node x, string prefix, Queue<string> results)
		{
			if (x == null)
			{
				return;
			}

			if (x.Val != null && x.IsTerminal)
			{
				results.Enqueue(prefix);
			}

			foreach (var c in x.Next.Keys)
			{
				Collect(x.Next[c], prefix + c, results);
			}
		}

		private class Node
		{
			public TValue Val;
			public bool IsTerminal;
			public Dictionary<char, Node> Next = new Dictionary<char, Node>();
		}
	}
}