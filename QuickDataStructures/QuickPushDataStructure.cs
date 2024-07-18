using QuickDataStructures.CustomDataStructures;

namespace QuickDataStructures;

public class QuickPushDataStructure<T> where T : IComparable<T>
{
    private CustomList<T> currentElement;
    private readonly object lockObject = new();

    private const string errorMessage = "The data structure is empty.";

    public QuickPushDataStructure()
    {
        currentElement = null;
    }

    public void Push(T item)
    {
        lock (lockObject)
        {
            CustomList<T> newNode = new CustomList<T>(item);
            if (currentElement == null)
            {
                currentElement = newNode;
            }
            else
            {
                newNode.Next = currentElement;
                currentElement = newNode;
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

            CustomList<T> maxNode = currentElement;
            CustomList<T> prev = null;
            CustomList<T> current = currentElement;
            CustomList<T> prevMax = null;

            while (current != null)
            {
                if (current.Value.CompareTo(maxNode.Value) > 0)
                {
                    maxNode = current;
                    prevMax = prev;
                }
                prev = current;
                current = current.Next;
            }

            if (prevMax == null)
            {
                currentElement = maxNode.Next;
            }
            else
            {
                prevMax.Next = maxNode.Next;
            }

            return maxNode.Value;
        }
    }
}
