public class MyHashmapIterator<T> : IMyIterator<T> where T : iDatabase
{
    private MyLinkedList<T>[] _buckets;
    private int _bucketIndex;
    private IMyIterator<T>? _currentBucketIterator;

    public MyHashmapIterator(MyLinkedList<T>[] buckets)
    {
        _buckets = buckets;
        _bucketIndex = -1;
        _currentBucketIterator = null!;
    }

    public bool HasNext()
    {
        while (_currentBucketIterator == null || !_currentBucketIterator.HasNext())
        {
            _bucketIndex++;
            if (_bucketIndex >= _buckets.Length)
                return false;

            if (_buckets[_bucketIndex] != null)
                _currentBucketIterator = _buckets[_bucketIndex].GetIterator();
        }
        return true;
    }

    public T Next()
    {
        if (!HasNext())
            throw new InvalidOperationException("No more elements in the collection.");

        return _currentBucketIterator!.Next();
    }

    public void Reset()
    {
        _bucketIndex = -1;
        _currentBucketIterator = null!;
    }
}