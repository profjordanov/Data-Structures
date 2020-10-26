/// <summary>
/// Class that holds hash table's elements (key-value pairs). 
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class KeyValue<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }

    public KeyValue(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    public override bool Equals(object other)
    {
        KeyValue<TKey, TValue> element = (KeyValue<TKey, TValue>)other;
        bool equals = Equals(Key, element.Key) && Equals(Value, element.Value);
        return equals;
    }

    public override int GetHashCode() =>
	    CombineHashCodes(Key.GetHashCode(), Value.GetHashCode());

	private static int CombineHashCodes(int h1, int h2)
    {
        return ((h1 << 5) + h1) ^ h2;
    }

    public override string ToString()
    {
        return $" [{Key} -> {Value}]";
    }
}
