using QuickDataStructures;

namespace QuickDataStructuresTests;

public class QuickPushDataStructureTests
{
    [Fact]
    public void PushAndPop_SingleElement()
    {
        // Arrange
        var quickPush = new QuickPushDataStructure<int>();

        // Act
        quickPush.Push(1);

        // Assert
        Assert.Equal(1, quickPush.Pop());
    }

    [Fact]
    public void PushAndPop_MultipleElements()
    {
        // Arrange
        var quickPush = new QuickPushDataStructure<int>();

        // Act
        quickPush.Push(3);
        quickPush.Push(6);
        quickPush.Push(7);
        quickPush.Push(2);
        quickPush.Push(4);

        // Assert
        Assert.Equal(7, quickPush.Pop());
        Assert.Equal(6, quickPush.Pop());
        Assert.Equal(4, quickPush.Pop());
        Assert.Equal(3, quickPush.Pop());
        Assert.Equal(2, quickPush.Pop());
    }

    [Fact]
    public void Pop_EmptyStructure_ThrowsException()
    {
        // Arrange
        var quickPush = new QuickPushDataStructure<int>();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => quickPush.Pop());
    }

    [Fact]
    public void Push_MultipleThreads()
    {
        // Arrange
        var quickPush = new QuickPushDataStructure<int>();

        // Act
        Parallel.Invoke(
            () => quickPush.Push(1),
            () => quickPush.Push(2),
            () => quickPush.Push(3),
            () => quickPush.Push(4),
            () => quickPush.Push(5)
        );

        var results = new int[5];

        Parallel.Invoke(
            () => results[0] = quickPush.Pop(),
            () => results[1] = quickPush.Pop(),
            () => results[2] = quickPush.Pop(),
            () => results[3] = quickPush.Pop(),
            () => results[4] = quickPush.Pop()
        );

        // Assert
        Array.Sort(results);
        Assert.Equal([1, 2, 3, 4, 5], results);
    }

    [Fact]
    public void Pop_MultipleThreads()
    {
        // Arrange
        var quickPush = new QuickPushDataStructure<int>();

        // Act
        for (var i = 1; i <= 5; i++) 
        {
            quickPush.Push(i);
        }

        var results = new int[5];

        Parallel.Invoke(
            () => results[0] = quickPush.Pop(),
            () => results[1] = quickPush.Pop(),
            () => results[2] = quickPush.Pop(),
            () => results[3] = quickPush.Pop(),
            () => results[4] = quickPush.Pop()
        );

        // Assert
        Array.Sort(results);
        Assert.Equal([1, 2, 3, 4, 5], results);
    }
}
