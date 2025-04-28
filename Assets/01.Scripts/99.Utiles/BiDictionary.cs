using System.Collections;
using System.Collections.Generic;

public class BiDictionary<K, V> : IEnumerable<KeyValuePair<K, V>>
{
    Dictionary<K, V> forward = new();
    Dictionary<V, K> reverse = new();

    public int Count => forward.Count;

    public void Add(K key, V value)
    {
        forward.Add(key, value);
        reverse.Add(value, key);
    }

    public void RemoveByKey(K key)
    {
        V temp = forward[key];
        forward.Remove(key);
        reverse.Remove(temp);
    }

    public void RemoveByValue(V value)
    {
        K temp = reverse[value];
        reverse.Remove(value);
        forward.Remove(temp);
    }

    public bool ContainsKey(K key)
    {
        return forward.ContainsKey(key);
    }

    public bool ContainsValue(V value)
    {
        return reverse.ContainsKey(value);
    }

    public void Clear()
    {
        forward.Clear();
        reverse.Clear();
    }

    public V GetByKey(K key) => forward[key];
    public K GetByValue(V value) => reverse[value];

    public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
    {
        return forward.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}