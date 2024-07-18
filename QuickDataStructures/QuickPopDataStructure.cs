using QuickDataStructures.CustomDataStructures;

namespace QuickDataStructures;

public class QuickPopDataStructure<T> where T : IComparable<T>
{
    private CustomList<T> currentElement;
    private CustomList<T> maxElement;
    private readonly object lockObject = new();

    private const string errorMessage = "The data structure is empty.";

    public QuickPopDataStructure()
    {
        currentElement = null;
        maxElement = null;
    }

    public void Push(T item)
    {
        lock (lockObject)
        {
            CustomList<T> newElement = new(item);

            if (currentElement == null)
            {
                currentElement = newElement;
                maxElement = newElement;
            }
            else
            {
                newElement.Next = currentElement;
                currentElement = newElement;

                if (item.CompareTo(maxElement.Value) > 0)
                {
                    maxElement = newElement;
                }
            }
        }
    }

    public T Pop()
    {
        lock (lockObject)
        {
            if (currentElement == null)
            {
                throw new InvalidOperationException(errorMessage);
            }

            T result = maxElement.Value;

            if (currentElement == maxElement)
            {
                currentElement = currentElement.Next;
            }
            else
            {
                CustomList<T> current = currentElement;

                while (current.Next != maxElement)
                {
                    current = current.Next;
                }
                current.Next = maxElement.Next;
            }

            maxElement = currentElement;
            CustomList<T> temp = currentElement;

            while (temp != null)
            {
                if (maxElement == null || temp.Value.CompareTo(maxElement.Value) > 0)
                {
                    maxElement = temp;
                }
                temp = temp.Next;
            }

            return result;
        }
    }
}
