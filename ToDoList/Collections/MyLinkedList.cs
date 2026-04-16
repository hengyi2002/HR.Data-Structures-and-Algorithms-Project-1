public class MyLinkedList<T> : IMyCollection<T>
{
    private SingleNode<T> _head;
    private int _count;

    public int Count{get => GetCountFrom(_head);}

    public bool Dirty { get; set; }

    public void AddFirst(T value)
    {
        SingleNode<T> newNode = new SingleNode<T>(value, _head);
        _head = newNode;
        _count++;
    }

    public void Add(T value)
    {
        SingleNode<T> newNode = new SingleNode<T>(value);
        if (_head == null)
        {
            _head = newNode;
        }
        else
        {
            SingleNode<T> current = _head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        _count++;
    }

    public void Remove(T value)
    {
        if (_head == null) return;

        if (_head.Value.Equals(value))
        {
            _head = _head.Next;
            _count--;
            return;
        }

        SingleNode<T> current = _head;
        while (current.Next != null)
        {
            if (current.Next.Value.Equals(value))
            {
                current.Next = current.Next.Next;
                _count--;
                return;
            }
            current = current.Next;
        }
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= _count)
            throw new IndexOutOfRangeException();

        if (index == 0)
        {
            _head = _head.Next;
            _count--;
            return;
        }

        SingleNode<T> current = _head;
        for (int i = 0; i < index - 1; i++)
        {
            current = current.Next;
        }
        current.Next = current.Next.Next;
        _count--;
    }

    public T FindBy<K>(K key, Func<T, K, bool> predicate)
    {
        SingleNode<T> current = _head;
        while (current != null)
        {
            if (predicate(current.Value, key))
                return current.Value;

            current = current.Next;
        }
        return default(T);
    }

    public SingleNode<T> GetLast()
    {
        SingleNode<T> current = _head;
        while (current != null && current.Next != null)
        {
            current = current.Next;
        }
        return current;
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        MyLinkedList<T> result = new MyLinkedList<T>();
        SingleNode<T> current = _head;
        while (current != null)
        {
            if (predicate(current.Value))
                result.Add(current.Value);

            current = current.Next;
        }
        return result;
    }

    public int IndexOf(T item)
    {
        SingleNode<T> current = _head;
        int index = 0;
        while (current != null)
        {
            if (current.Value.Equals(item))
                return index;

            current = current.Next;
            index++;
        }
        return -1;
    }

    public void Sort(Comparison<T> comparison)
    {
        _head = MergeSort(_head, comparison);
    }

    private SingleNode<T> MergeSort(SingleNode<T> head, Comparison<T> comparison)
    {
        if (head == null || head.Next == null)
            return head;

        SingleNode<T> middle = GetMiddle(head);
        SingleNode<T> nextOfMiddle = middle.Next;
        middle.Next = null;

        SingleNode<T> left = MergeSort(head, comparison);
        SingleNode<T> right = MergeSort(nextOfMiddle, comparison);

        return Merge(left, right, comparison);
    }

    private SingleNode<T> GetMiddle(SingleNode<T> head)
    {
        if (head == null) return head;

        SingleNode<T> slow = head, fast = head.Next;
        while (fast != null)
        {
            fast = fast.Next;
            if (fast != null)
            {
                slow = slow.Next;
                fast = fast.Next;
            }
        }
        return slow;
    }

    private SingleNode<T> Merge(SingleNode<T> left, SingleNode<T> right, Comparison<T> comparison)
    {
        if (left == null) return right;
        if (right == null) return left;

        SingleNode<T> result;
        if (comparison(left.Value, right.Value) <= 0)
        {
            result = left;
            result.Next = Merge(left.Next, right, comparison);
        }
        else
        {
            result = right;
            result.Next = Merge(left, right.Next, comparison);
        }
        return result;
    }

    public void Clear()
    {
        _head = null;
        _count = 0;
    }

    public R Reduce<R>(Func<R, T, R> accumulator, R initial)
    {
        R result = initial;
        SingleNode<T> current = _head;
        for (int i = 0; i < _count; i++)
        {
            result = accumulator(result, current.Value);
            current = current.Next;
        }
        return result;
    }

    public IMyIterator<T> GetIterator()
    {
        return new MyLinkedListIterator<T>(_head);
    }

    public IEnumerator<T> GetEnumerator()
    {
        SingleNode<T> current = _head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    public int GetCountFrom(SingleNode<T> start, int acc = 0)
    {
        return start == null ? acc : GetCountFrom(start.Next, acc + 1);
    }

    public int GetCountFrom(SingleNode<T> start)
    {
        if(start == null) return 0;
        return 1+ GetCountFrom(start.Next);
    }

    public int GetCountFrom_(SingleNode<T> start)
    {
        if (start == null) return 0;
        int count = 0;

        while (start != null)
        {
            count++;
            start = start.Next;
        }
        return count;
    }

    public void Update(T task)
    {
        // idk wat je hier wil
    }
}