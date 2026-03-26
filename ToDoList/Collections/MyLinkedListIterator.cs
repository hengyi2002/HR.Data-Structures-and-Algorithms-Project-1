public class MyLinkedListIterator<T> : IMyIterator<T>
{
    private SingleNode<T> _current;

    public MyLinkedListIterator(SingleNode<T> head)
    {
        _current = head;
    }

    public bool HasNext()
    {
        return _current != null;
    }

    public T Next()
    {
        if (!HasNext())
            throw new InvalidOperationException("No more elements in the collection.");

        T value = _current.Value;
        _current = _current.Next;
        return value;
    }

    public void Reset()
    {
        _current = null;
    }
}