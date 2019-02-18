using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Array that holds a set of {key, value} pairs.
/// Implemented by a hash table that uses chaining in a linked list as collision resolution strategy.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
	private const int DefaultCapacity = 16;
	private const float LoadFactor = 0.75f;

	/// <summary>
	/// An array that holds the slots in the hash table.
	/// Each slot is either empty (null) or holds a linked list of elements with the same hash code.
	/// </summary>
	private LinkedList<KeyValue<TKey, TValue>>[] _slots;

	/// <summary>
	/// Allocates the slots.
	/// </summary>
	/// <param name="capacity">
	/// Specified capacity in the underlying array (slots).
	/// 16 is the default initial hash table capacity.
	/// </param>
	public HashTable(int capacity = DefaultCapacity)
	{
		InitializeHashTable(capacity);
	}

	/// <summary>
	/// Holds the number of elements in the hash table.
	/// </summary>
	public int Count { get; private set; }

	/// <summary>
	/// Holds the number of slots in the hash table.
	/// </summary>
	public int Capacity => _slots.Length;

	/// <summary>
	/// Inserts a new element in the hash table.
	/// Throws an exception if key already exists.
	/// Amortized complexity O(1) – constant time.
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	public void Add(TKey key, TValue value)
    {
		// resize to double capacity when the fill factor is too high
		GrowIfNeeded();
		// find the slot that should hold the element to be added
		var slotNumber = FindSlotNumber(key);
		// it is either empty (null) or holds a linked list 
		InitializeElementsInSlot(slotNumber);

		foreach (var element in _slots[slotNumber])
		{
			// checks for duplicated key 
			if(element.Key.Equals(key))
			{
				throw new ArgumentException($"Key already exists: {key}!");
			}
		}

		//appends the new element at the end of the linked list in the target slot of the hash table 
		// and increase the count
		AddElement(key, value, slotNumber);
	}

	/// <summary>
	/// Inserts a new element in the hash table.
	/// If key already exists replaces the value in the element holding the key, with the new value passed as argument.
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public bool AddOrReplace(TKey key, TValue value)
    {
	    GrowIfNeeded();

	    var index = FindSlotNumber(key);
	    InitializeElementsInSlot(index);

	    // Replace element value 
	    var elements = _slots[ index ];
	    foreach(var element in elements)
	    {
		    if (!element.Key.Equals(key))
		    {
			    continue;
		    }
		    element.Value = value;
		    return false;
	    }

	    AddElement(key, value, index);
	    return true;
	}

	/// <summary>
	/// Returns the element by given key.
	/// Throws and exception when the key does not exist. 
	/// </summary>
	/// <param name="key"></param>
	public TValue Get(TKey key)
    {
	    var element = Find(key);
	    if (element == null)
	    {
		    throw new KeyNotFoundException();
	    }

	    return element.Value;
    }

	/// <summary>
	/// Accesses the hash table indexed by key.
	/// </summary>
	/// <param name="key"></param>
	public TValue this[TKey key]
    {
		// returns the value by given key or exception when the key is not found
		get => Get(key);
		// adds or replace the value by given key
		set => AddOrReplace(key, value);
	}

	/// <summary>
	/// Conditional find by key.
	/// Amortized complexity O(1) – constant time.
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	/// <returns>
	///  - true + the value if the ey exists in the hash table.
	///  - false if the key does not exist in the hash table.
	/// </returns>
	public bool TryGetValue(TKey key, out TValue value)
    {
	    var element = Find(key);
	    if (element != null)
	    {
		    value = element.Value;
		    return true;
	    }

	    value = default(TValue);
	    return false;
    }

	/// <summary>
	///		Finds an element by key
	/// </summary>
	/// <param name="key"></param>
	/// <returns>
	///		Either the element by its key or null if the key does not exist.
	/// </returns>
	public KeyValue<TKey, TValue> Find(TKey key)
	{
		// finds the slot holding the specified key (by calculating the hash code modulus the hash table size)
		var slotNumber = FindSlotNumber(key);
		var elements = _slots[slotNumber];
		if (elements == null)
		{
			return null;
		}
		// passes through all elements in the target slot (in its linked list) and compare their key with the target key
		foreach(var element in elements)
		{
			if (element.Key.Equals(key))
			{
				return element;
			}
		}

		return null;
	}

	/// <param name="key"></param>
	/// <returns>Whether the key exists in the hash table.</returns>
	public bool ContainsKey(TKey key) =>
		Find(key) != null;

	/// <summary>
	/// Removes an element by its key (when the key exists).
	/// Amortized complexity O(1) – constant time.
	/// </summary>
	/// <param name="key"></param>
	/// <returns>If key does exist in the hash table.</returns>
	public bool Remove(TKey key)
    {
		// finds the slot that is expected to hold the key, 
		var slotNumber = FindSlotNumber(key);
	    var elements = _slots[slotNumber];
	    if (elements == null)
	    {
		    return false;
	    }

		// traverse the linked list
		foreach(var element in elements)
	    {
		    if (!element.Key.Equals(key))
		    {
			    continue;
		    }
			// remove the element is case the key is found 
			elements.Remove(element);
		    Count--;
		    return true;
	    }

	    return false;
    }

    public void Clear() => InitializeHashTable();

	/// <summary>
	/// Enumerates all keys. 
	/// </summary>
	public IEnumerable<TKey> Keys =>
	    this.Select(element => element.Key);

	/// <summary>
	/// Enumerates all values. 
	/// </summary>
	public IEnumerable<TValue> Values =>
		this.Select(element => element.Value);

    /// <summary>
    /// Method that passed through all elements in the hash table exactly once.
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	/// <summary>
	/// Passes through all slots and through all elements in the linked list in each slot
	/// and returns the elements in a sequence (as a stream).
	/// It uses the yield return construct in C# (generator function) to return the elements "on demand" upon request. 
	/// </summary>
	/// <returns></returns>
	public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator() =>
		_slots
			.Where(elements => elements != null)
			.SelectMany(elements => elements)
			.GetEnumerator();

	// HELPER METHODS
	private void InitializeHashTable(int capacity = DefaultCapacity)
    {
	    _slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
	    Count = 0;
    }

    private float FillFactor => (float) (Count + 1) / Capacity;

	/// <summary>
	/// Checks whether the hash table should grow. 
	/// </summary>
	private void GrowIfNeeded()
    {
		// Hash table should grow when it has filled its capacity to more than 75% (load factor > 75%) and we are trying to add a new element. 
		if(FillFactor > LoadFactor)
	    {
		    Grow();
	    }
    }

	/// <summary>
	/// Allocates a new hash table with double capacity and
	/// adds the old elements in the new hash table,
	/// then replaces the old hash table with the new one.
	/// </summary>
	private void Grow()
	{
		// Double capacity
		var newHashTable = new HashTable<TKey, TValue>(Capacity * 2);

		// Add elements
		foreach(var element in this)
		{
			newHashTable.Add(element.Key, element.Value);
		}

		// Replace hash tables
		_slots = newHashTable._slots;
		Count = newHashTable.Count;
	}

	private void InitializeElementsInSlot(int index)
	{
		if(_slots[ index ] == null)
		{
			_slots[ index ] = new LinkedList<KeyValue<TKey, TValue>>();
		}
	}

	/// <summary>
	/// Finds slot by key. 
	/// </summary>
	/// <param name="key"></param>
	/// <returns>The slot number.</returns>
	private int FindSlotNumber(TKey key) => Math.Abs(key.GetHashCode()) % Capacity;

	/// <summary>
	/// Appends the new element at the end of the linked list in the target slot of the hash table 
	///  and increase the count
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	/// <param name="index"></param>
	private void AddElement(TKey key, TValue value, int index)
	{
		var newElement = new KeyValue<TKey, TValue>(key, value);
		_slots[ index ].AddLast(newElement);
		Count++;
	}
}
