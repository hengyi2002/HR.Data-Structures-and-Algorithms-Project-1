using Microsoft.EntityFrameworkCore.Storage;

public class MyBinarySearchTree<T> : IMyCollection<T> where T : IDatabase
{
    public int Count => throw new NotImplementedException();

    public bool Dirty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Add(T item)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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

    public void Update(T task)
    {
        throw new NotImplementedException();
    }
}