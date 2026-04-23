using System.ComponentModel;
using System.Dynamic;

public class MyHashmap<T>: IMyCollection<T>
{
    private MyLinkedList<T>[] _buckets;

    public int Count {get; private set;}

    public bool Dirty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public MyHashmap(int capacity = 10)
    {
        Count = capacity;
        _buckets = new MyLinkedList<T>[capacity];
    }

    public int GetIndex(T key)
    {
        return Math.Abs(key.GetHashCode()) % _buckets.Length;
    }

    public void Add(T item)
    {
        int index = GetIndex(item);

        _buckets[index] ??= new MyLinkedList<T>();

        if(Check()){
            Rehash();
        }
        
        foreach (var kvp in _buckets[index])
        {
            if (kvp.Equals(item))
            {
                throw new ArgumentException($"Key '{item}' already exists in the hashmap.");
            }
        }
        _buckets[index].Add(item);
    }
    public bool Check()
    {
        int IsUsed = 0;
        for(int i = 0; i < _buckets.Length; i++)
        {
            if(_buckets[i] != null && _buckets[i].Count != 0)
            {
                IsUsed++;
            }
        }
        int bucketlength = _buckets.Length;
        if(IsUsed <= bucketlength/2) 
        {
            return true;
        }
        return false;
    }

    public void Rehash()
    {
        MyLinkedList<T>[] oldBuckets = _buckets;
        int newCapacity = Count * 2;
        MyLinkedList<T>[] newBuckets = new MyLinkedList<T>[newCapacity];

        foreach (var bucket in oldBuckets)
        {
            if (bucket != null)
            {
                foreach (var kvp in bucket)
                {
                    int newIndex = GetIndex(kvp);
                    newBuckets[newIndex] ??= new MyLinkedList<T>();
                    newBuckets[newIndex].Add(kvp);  
                }
            }
        }

        _buckets = newBuckets;
        Count = newCapacity;
    }

    public void Remove(T item)
    {
        int index = GetIndex(item);
        if (_buckets[index] == null)
        {
            throw new KeyNotFoundException($"Key '{item}' not found in the hashmap.");
        }
         _buckets[index].Remove(item);
    }

    public void Update(T item)
    {
        int index = GetIndex(item);
        if (_buckets[index] == null)
        {
            throw new KeyNotFoundException($"Key '{item}' not found in the hashmap.");
        }

        T current = FindBy(t => t.Equals(item) ? 0 : -1);
        if (current != null)
        {
            _buckets[index].Remove(current);
            _buckets[index].Add(item);
        }
        else
        {
            throw new KeyNotFoundException($"Key '{item}' not found in the hashmap.");
        }
    }

    public T FindBy(Func<T, int> predicate)
    {
        foreach (var bucket in _buckets)
        {
            if (bucket != null)
            {
                foreach (var kvp in bucket)
                {
                    if (predicate(kvp) == 0)
                    {
                        return kvp;
                    }
                }
            }
        }
        return default(T);
    }


    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        MyHashmap<T> result = new MyHashmap<T>(Count);
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

    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        for (int i = 0; i < _buckets.Length; i++)
        {
            _buckets[i] = null;
        }
    }

    public R Reduce<R>(Func<R, T, R> accumulator, R initial)
    {
        R result = initial;
        foreach (var bucket in _buckets)
        {
            if (bucket != null)
            {
                foreach (var kvp in bucket)
                {
                    result = accumulator(result, kvp);
                }
            }
        }
        return result;
    }

    public IMyIterator<T> GetIterator()
    {
        return new MyHashmapIterator<T>(_buckets);
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var bucket in _buckets)
        {
            if (bucket != null)
            {
                foreach (var kvp in bucket)
                {
                    yield return kvp;
                }
            }
        }
    }
}


