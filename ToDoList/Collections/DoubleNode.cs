public class DoubleNode<T>
{
    public T Value;
    public DoubleNode<T> Next;
    public DoubleNode<T> Previous;

    public DoubleNode(T value, 
                      DoubleNode<T>? next = null, 
                      DoubleNode<T>? previous = null)
    {
        Value = value;
        Next = next;
        Previous = previous;
    }
}