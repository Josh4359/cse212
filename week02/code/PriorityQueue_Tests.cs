using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: The Enqueue function shall add an item (which contains both data and priority) to the back of the queue.
    // Expected Result: Items with the specified parameters should be added to the list.
    // Defect(s) Found: None
    public void TestPriorityQueue_1()
    {
        List<PriorityItem> expected = new() { new("a", 0), new("b", 1) };

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 0);
        priorityQueue.Enqueue("b", 1);
        
        if (priorityQueue._queue.Count == expected.Count)
            for (int i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i].ToString(), priorityQueue._queue[i].ToString());
        else
            Assert.Fail("Lists are not equal in size");
    }

    [TestMethod]
    // Scenario: The Dequeue function shall remove the item with the highest priority and return its value.
    // Expected Result: The item with the highest priority should be removed and its value returned.
    // Defect(s) Found: PriorityQueue.Dequeue correctly identifies which item to remove, but does not actually remove it.
    public void TestPriorityQueue_2()
    {
        List<PriorityItem> expected = new() { new("a", 0), new("c", 0) };

        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 0);
        priorityQueue.Enqueue("b", 1);
        priorityQueue.Enqueue("c", 0);
        priorityQueue.Enqueue("d", 1);
        Assert.AreEqual("b", priorityQueue.Dequeue());
        Assert.AreEqual("d", priorityQueue.Dequeue());
        
        if (priorityQueue._queue.Count == expected.Count)
            for (int i = 0; i < expected.Count; i++)
                Assert.AreEqual(expected[i].ToString(), priorityQueue._queue[i].ToString());
        else
            Assert.Fail("Lists are not equal in size");
    }

    [TestMethod]
    // Scenario: If there are more than one item with the highest priority, then the item closest to the front of the queue will be removed and its value returned.
    // Expected Result: The item with (1) the highest priority and (2) lowest index in the list should be removed.
    // Defect(s) Found: None
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 0);
        priorityQueue.Enqueue("b", 1);
        priorityQueue.Enqueue("c", 1);
        Assert.AreEqual(priorityQueue.Dequeue(), "b");
    }

    [TestMethod]
    // Scenario: If the queue is empty, then an error exception shall be thrown. This exception should be an InvalidOperationException with a message of "The queue is empty."
    // Expected Result: Attempting to dequeue from an empty queue should return the specified error.
    // Defect(s) Found: None
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(priorityQueue.Dequeue, "The queue is empty.");
    }
}