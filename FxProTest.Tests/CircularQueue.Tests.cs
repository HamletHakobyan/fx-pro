namespace FxProTest.Tests;

using FxProTest.Lib; 

public class CircularQueueTests
{
    [Fact]
    public void CircularQueue_Can_Create()
    {
        var circularQueue = new CircularQueue<int>(10);
    }

    [Fact]
    public void CircularQueue_Should_Throw_When_Empty()
    {
        var circularQueue = new CircularQueue<int>(10);
        Assert.Throws<InvalidOperationException>(() => circularQueue.Dequeue());
    }

    [Fact]
    public void CircularQueue_Count_Should_Be_1_When_Enqueued()
    {
        var circularQueue = new CircularQueue<int>(10);
        circularQueue.Enqueue(1);
        Assert.Equal(1, circularQueue.Count);
    }

    [Fact]
    public void CircularQueue_Deque_Should_Return_The_Item_When_Enqueued()
    {
        var circularQueue = new CircularQueue<int>(10);
        circularQueue.Enqueue(1);
        var actual = circularQueue.Dequeue();
        Assert.Equal(1, actual);
    }
    
    [Fact]
    public void CircularQueue_Is_FIFO()
    {
        var circularQueue = new CircularQueue<int>(10);
        circularQueue.Enqueue(1);
        circularQueue.Enqueue(2);
        var actual = circularQueue.Dequeue();
        Assert.Equal(1, actual);
    }

    [Fact]
    public void CircularQueue_Is_Circular_FIFO()
    {
        var circularQueue = new CircularQueue<int>(2);
        circularQueue.Enqueue(0);
        circularQueue.Enqueue(1);
        circularQueue.Enqueue(2);
        var actual = circularQueue.Dequeue();
        
        Assert.Equal(1, actual);
        
    }
    
    [Fact]
    public void CircularQueue_Is_Circular_FIFO_In_More_Than_One_Cycle()
    {
        var circularQueue = new CircularQueue<int>(2);
        circularQueue.Enqueue(0);
        circularQueue.Enqueue(1);
        circularQueue.Enqueue(2);
        circularQueue.Enqueue(3);
        circularQueue.Enqueue(4);

        var actual = circularQueue.Dequeue();
        
        Assert.Equal(3, actual);
    }
    
    [Theory]
    [InlineData(3, 13)]
    [InlineData(3, 14)]
    [InlineData(3, 15)]
    [InlineData(3, 16)]
    [InlineData(3, 17)]
    [InlineData(3, 2)]
    [InlineData(8, 3)]
    [InlineData(0, 0)]
    [InlineData(1, 0)]
    public void CircularQueue_Circular_FIFO_Should_Dequeue_All_In_Right_Order(int capacity, int n)
    {
        var circularQueue = new CircularQueue<int>(capacity);
        for (var i = 0; i < n; ++i)
        {
            circularQueue.Enqueue(i);
        }

        var arraySize = Math.Min(capacity, n);
        var actual = new int[arraySize];
        for (var i = 0; i < actual.Length; i++)
        {
            actual[i] = circularQueue.Dequeue();
        }
        
        Assert.Equal(Enumerable.Range(n < capacity ? 0 : (n - capacity), arraySize), actual);
        Assert.Equal(0, circularQueue.Count);
    }
}