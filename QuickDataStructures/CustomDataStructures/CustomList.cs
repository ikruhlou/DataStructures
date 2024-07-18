namespace QuickDataStructures.CustomDataStructures;

public class CustomList<T>
{
    public T Value { get; set; }
    public CustomList<T> Next { get; set; }

    public CustomList(T value)
    {
        Value = value;
        Next = null;
    }
}
