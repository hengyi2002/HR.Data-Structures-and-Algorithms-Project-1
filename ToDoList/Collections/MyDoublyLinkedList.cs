public class MyDoublyLinkedList<T> : IMyCollection<T>
{
    public DoubleNode<T> Head, Tail;
    public MyDoublyLinkedList()
    {
        Head = Tail = null;
    }

    public int Count => throw new NotImplementedException();

    public bool Dirty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public DoubleNode<T> Search(T value)
    {
        if (Head == null && Tail == null)
            return null;

        var currentNode = Head;
        while (currentNode != null)
        {
            if (currentNode.Value.Equals(value))
                return currentNode;

            currentNode = currentNode.Next;
        }
        return currentNode;
    } 

    public void Add(T item)
    {
        throw new NotImplementedException();
    }

    public void AddFirst(T value)
    {
        var newNode = new DoubleNode<T>(value, Head, null);
        if (Head == null && Tail == null)
        {
            Head = newNode;
            Tail = Head;
        }
        else if (Head != null && Tail != null)
        {
            Head.Previous = newNode;
            Head = newNode;
        }
    }

    public void AddLast(T value)
    {
        var newNode = new DoubleNode<T>(value, null, Tail);
        if (Head == null && Tail == null)
        {
            Head = newNode;
            Tail = Head;
        }
        else
        {
            Tail.Next = newNode;
            Tail = newNode;
        }
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public T FindBy<K>(K key, Func<T, K, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        DoubleNode<T> current = Head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    public IMyIterator<T> GetIterator()
    {
        throw new NotImplementedException();
    }

    public R Reduce<R>(Func<R, T, R> accumulator, R initial)
    {
        throw new NotImplementedException();
    }

    public void Remove(T item)
    {
        throw new NotImplementedException();
    }

    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}