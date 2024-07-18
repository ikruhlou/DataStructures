using QuickDataStructures;

namespace QuickDataStructuresTests;

public class QuickPopDataStructureTests
{
    [Fact]
    public void PushAndPop_SingleElement()
    {
        // Arrange
        var quickPop = new QuickPopDataStructure<int>();

        // Act
        quickPop.Push(1);

        // Assert
        Assert.Equal(1, quickPop.Pop());
    }

    [Fact]
    public void PushAndPop_MultipleElements()
    {
        // Arrange
        var quickPop = new QuickPopDataStructure<int>();

        // Act
        quickPop.Push(3);
        quickPop.Push(6);
        quickPop.Push(7);
        quickPop.Push(2);
        quickPop.Push(4);

        // Assert
        Assert.Equal(7, quickPop.Pop());
        Assert.Equal(6, quickPop.Pop());
        Assert.Equal(4, quickPop.Pop());
        Assert.Equal(3, quickPop.Pop());
        Assert.Equal(2, quickPop.Pop());
    }

    [Fact]
    public void Pop_EmptyStructure_ThrowsException()
    {
        // Arrange
        var quickPop = new QuickPopDataStructure<int>();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() => quickPop.Pop());
    }

    [Fact]
    public void Push_MultipleThreads()
    {
        // Arrange
        var quickPop = new QuickPopDataStructure<int>();

        // Act
        Parallel.Invoke(
            () => quickPop.Push(1),
            () => quickPop.Push(2),
            () => quickPop.Push(3),
            () => quickPop.Push(4),
            () => quickPop.Push(5)
        );

        // Assert
        for (var i = 5; i >= 1; i--)
        {
            Assert.Equal(i, quickPop.Pop());
        }
    }

    [Fact]
    public void Pop_MultipleThreads()
    {
        // Arrange
        var quickPop = new QuickPopDataStructure<int>();

        // Act
        for (var i = 1; i <= 5; i++)
        {
            quickPop.Push(i);
        }

        var results = new int[5];

        Parallel.Invoke(
            () => results[0] = quickPop.Pop(),
            () => results[1] = quickPop.Pop(),
            () => results[2] = quickPop.Pop(),
            () => results[3] = quickPop.Pop(),
            () => results[4] = quickPop.Pop()
        );

        // Assert
        Array.Sort(results);
        Assert.Equal([1, 2, 3, 4, 5], results);
    }
}
