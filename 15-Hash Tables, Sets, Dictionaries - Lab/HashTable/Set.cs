using System.Collections.Generic;
using System.Linq;

namespace Hash_Table
{
	/// <summary>
	/// Abstract data type that keeps a set of elements with no duplicates.
	/// </summary>
	public class Set<TKey>
	{
		private readonly HashTable<TKey, TKey> _table = new HashTable<TKey, TKey>();

		public Set(IEnumerable<KeyValue<TKey, TKey>> enumerable = null)
		{
			if (enumerable == null)
			{
				return;
			}

			foreach (var keyValue in enumerable)
			{
				_table.AddOrReplace(keyValue.Key, keyValue.Key);
			}
		}

		public void Add(TKey key) =>
			_table.AddOrReplace(key, key);

		public bool Contains(TKey key) =>
			_table.ContainsKey(key);

		public Set<TKey> ExceptWith(Set<TKey> other)
		{
			var keys = _table
				.Where(kvp => !other.Contains(kvp.Key));

			return new Set<TKey>(keys);
		}

		public Set<TKey> IntersectWith(Set<TKey> other)
		{
			var keys = _table
				.Where(kvp => other.Contains(kvp.Key));

			return new Set<TKey>(keys);
		}

		public Set<TKey> UnionWith(Set<TKey> other)
		{
			var keys = _table
				.Concat(other._table)
				.Distinct();
			
			return new Set<TKey>(keys);
		}

		public Set<TKey> SymmetricExceptWith(Set<TKey> other) =>
			UnionWith(other).ExceptWith(IntersectWith(other));

		public override string ToString()
			=> string.Join(" ", _table.Keys.OrderBy(x => x));
	}
}