using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class BiDictionary<K, V>
{
    Dictionary<K, V> forward = new();
    Dictionary<V, K> reverse = new();

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

    public V GetByKey(K key) => forward[key];
    public K GetByValue(V value) => reverse[value];
}