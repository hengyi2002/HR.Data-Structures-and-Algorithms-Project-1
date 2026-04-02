using System.ComponentModel;
using System.Dynamic;

public class MyHashmap<TKey, TValue>: IMyCollection<KeyValuePair<TKey, TValue>>
{
    private readonly MyLinkedList<KeyValuePair<TKey, TValue>>[] _buckets;

    public int Count {get; private set;}

    public bool Dirty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public MyHashmap(int capacity = 10)
    {
        Count = capacity;
        _buckets = new MyLinkedList<KeyValuePair<TKey, TValue>>[capacity];
    }

    public int GetIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode() % Count);
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        int index = GetIndex(item.Key);

        _buckets[index] ??= new MyLinkedList<KeyValuePair<TKey, TValue>>();
        
        foreach (var kvp in _buckets[index])
        {
            if (kvp.Key.Equals(item.Key))
            {
                throw new ArgumentException($"Key '{item.Key}' already exists in the hashmap.");
            }
        }
        _buckets[index].Add(item);
    }

    public void Remove(KeyValuePair<TKey, TValue> item)
    {
        int index = GetIndex(item.Key);
        if (_buckets[index] == null)
        {
            throw new KeyNotFoundException($"Key '{item.Key}' not found in the hashmap.");
        }
         _buckets[index].Remove(item);
    }

    public KeyValuePair<TKey, TValue> FindBy<K>(K key, Func<KeyValuePair<TKey, TValue>, K, bool> predicate)
    {
        int index = GetIndex((TKey)(object)key);
        if (_buckets[index] == null)
        {
            throw new KeyNotFoundException($"Key '{key}' not found in the hashmap.");
        }
        foreach (var kvp in _buckets[index])
        {
            if (predicate(kvp, key))
            {
                return kvp;
            }
        }
        throw new KeyNotFoundException($"Key '{key}' not found in the hashmap.");
    }


    public IMyCollection<KeyValuePair<TKey, TValue>> Filter(Func<KeyValuePair<TKey, TValue>, bool> predicate)
    {
        MyHashmap<TKey, TValue> result = new MyHashmap<TKey, TValue>(Count);
        foreach (var bucket in _buckets)
        {
            if (bucket != null)
            {
                foreach (var kvp in bucket)
                {
                    if (predicate(kvp))
                    {
                        result.Add(kvp);
                    }
                }
            }
        }
        return result;
    }

    public void Sort(Comparison<KeyValuePair<TKey, TValue>> comparison)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public R Reduce<R>(Func<R, KeyValuePair<TKey, TValue>, R> accumulator, R initial)
    {
        throw new NotImplementedException();
    }

    public IMyIterator<KeyValuePair<TKey, TValue>> GetIterator()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
