using System;

public class MyArray<T> : IMyCollection<T> where T : iDatabase
{
    private T[] _items;
    private int _count;
    private const int DefaultCapacity = 10;

    public int Count => _count;
    public bool IsEmpty => _count == 0;
    public int Capacity => _items.Length;
    public bool Dirty { get; set; }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException($"Index {index} is out of range. Count: {_count}");
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException($"Index {index} is out of range. Count: {_count}");
            _items[index] = value;
        }
    }

    public MyArray()
    {
        _items = new T[DefaultCapacity];
        _count = 0;
    }

    public MyArray(int capacity)
    {
        if (capacity < 0)
            throw new ArgumentException("Capacity cannot be negative");
        _items = new T[capacity];
        _count = 0;
    }

    public void Add(T item)
    {
        if (_count == _items.Length)
        {
            Resize(_items.Length == 0 ? DefaultCapacity : _items.Length * 2);
        }
        _items[_count] = item;
        _count++;
    }

    public void Remove(T item)
    {
        int index = IndexOf(item);
        if (index >= 0)
        {
            RemoveAt(index);
        }
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
            throw new IndexOutOfRangeException();

        for (int i = index; i < _count - 1; i++)
        {
            _items[i] = _items[i + 1];
        }
        _count--;
        _items[_count] = default(T); 
    }

    public void Update(T task)
    {
        T found = FindBy(task.ID, (t, id) => t.ID == id);
        if (found != null)
        {
            int index = IndexOf(found);
            _items[index] = task;
        }
    }

    public T FindBy<K>(K key, Func<T, K, bool> predicate)
    {
        for (int i = 0; i < _count; i++)
        {
            if (predicate(_items[i], key))
            {
                return _items[i];
            }
        }
        return default(T);
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        MyArray<T> result = new MyArray<T>();
        for (int i = 0; i < _count; i++)
        {
            if (predicate(_items[i]))
            {
                result.Add(_items[i]);
            }
        }
        return result;
    }


    
    public int IndexOf(T item)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_items[i] == null && item == null)
                return i;
            if (_items[i] != null && _items[i].Equals(item))
                return i;
        }
        return -1;
    }

    public void Sort(Comparison<T> comparison)
    {
        for (int i = 0; i < _count - 1; i++)
        {
            for (int j = 0; j < _count - i - 1; j++)
            {
                if (comparison(_items[j], _items[j + 1]) > 0)
                {
                    T temp = _items[j];
                    _items[j] = _items[j + 1];
                    _items[j + 1] = temp;
                }
            }
        }
    }

    public bool Contains(T item)
    {
        return IndexOf(item) >= 0;
    }
    
    public void Clear()
    {
        for (int i = 0; i < _count; i++)
        {
            _items[i] = default;
        }
        _count = 0;
    }
    private void Resize(int newCapacity)
    {
        T[] newArray = new T[newCapacity];
        for (int i = 0; i < _count; i++)
        {
            newArray[i] = _items[i];
        }
        _items = newArray;
    }
    public R Reduce<R>(Func<R, T, R> accumulator, R initial)
    {
        R result = initial;
        for (int i = 0; i < _count; i++)
        {
            result = accumulator(result, _items[i]);
        }
        return result;
    }

    public IMyIterator<T> GetIterator()
    {
        return new MyIterator<T>(_items);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _items[i];
        }
    }
}