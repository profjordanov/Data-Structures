using System;

namespace Lists
{
	public class ArrayList<T>
	{
		private const int InitialCapacity = 2;

		private T[] _items;

		public ArrayList()
		{
			_items = new T[InitialCapacity];
		}

		public int Count { get; private set; }

		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException();
				}

				return _items[index];
			}

			set
			{
				if(index < 0 || index >= Count)
				{
					throw new ArgumentOutOfRangeException();
				}

				_items[index] = value;
			}
		}

		public void Add(T item)
		{
			if (Count == _items.Length)
			{
				Resize();
			}

			_items[Count++] = item;
		}

		private void Resize()
		{
			var copy = new T[_items.Length * 2];

			Array.Copy(
				sourceArray: _items,
				destinationArray: copy,
				length: Count);

			_items = copy;
		}

		public T RemoveAt(int index)
		{
			if (index < 0 || index >= Count)
			{
				throw new ArgumentOutOfRangeException();
			}

			var element = _items[index];
			_items[index] = default(T);
			Shift(index);
			Count--;
			if (Count <= _items.Length / 4)
			{
				Shrink();
			}

			return element;
		}

		private void Shrink()
		{
			var copy = new T[_items.Length / 2];
			Array.Copy(
				sourceArray: _items,
				destinationArray: copy,
				length: Count);
			_items = copy;
		}

		private void Shift(int index)
		{
			for (var i = index; i < Count; i++)
			{
				_items[i] = _items[i + 1];
			}
		}
	}
}
