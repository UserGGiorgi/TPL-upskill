using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Tpl.Tests;

[TestFixture]
public class TplUnitTest
{
    [TestCase]
    public void TestForTaskCreated()
    {
        // Act
        var actual = StudentLogic.TaskCreated();

        // Assert
        Assert.That(TaskStatus.Created, Is.EqualTo(actual.Status));
    }

    [TestCase]
    public void TestForWaitingForActivation()
    {
        // Act
        var actual = StudentLogic.WaitingForActivation();

        // Assert
        Assert.That(TaskStatus.WaitingForActivation, Is.EqualTo(actual.Status));
    }

    [TestCase]
    public void TestForWaitingToRun()
    {
        // Act
        var actual = StudentLogic.WaitingToRun();

        // Assert
        Assert.That(TaskStatus.WaitingToRun, Is.EqualTo(actual.Status));
    }

    [TestCase]
    public async Task TestForRunning()
    {
        // Act
        var task = StudentLogic.Running();

        // Wait for the task to start running
        await Task.Delay(100); // Small delay to give the task time to start

        // Assert
        Assert.That(task.Status, Is.EqualTo(TaskStatus.Running));
    }

    [TestCase]
    public void TestForRanToCompletion()
    {
        // Act
        var actual = StudentLogic.RanToCompletion();

        // Assert
        Assert.That(TaskStatus.RanToCompletion, Is.EqualTo(actual.Status));
    }

    [TestCase]
    public async Task TestForWaitingForChildrenToComplete()
    {
        // Act
        var task = StudentLogic.WaitingForChildrenToComplete();

        // Await the task to ensure it completes
        await task;

        // Assert
        Assert.That(task.Status, Is.EqualTo(TaskStatus.RanToCompletion));
    }

    [TestCase]
    public void TestShouldReturnTrueIfCompleted()
    {
        // Act
        var actual = StudentLogic.IsCompleted();

        // Assert
        Assert.That(actual.IsCompleted);
    }

    [TestCase]
    public void TestShouldReturnTrueIfCancelled()
    {
        // Act
        var actual = StudentLogic.IsCancelled();

        // Assert
        Assert.That(actual.IsCanceled);
    }

    [TestCase]
    public void TestShouldReturnTrueIfFaulted()
    {
        // Act
        var actual = StudentLogic.IsFaulted();

        // Assert
        Assert.That(actual.IsFaulted);
    }

    [TestCase]
    public void TestShouldPassIfSequenceOfResultDosentMatch()
    {
        // Arrange
        var testList = Enumerable.Range(1, 300).ToList();
        var plinqResult = testList.Where(x => x > 0);

        // Act
        var result = StudentLogic.ForceParallelismPlinq();
        var ans = Enumerable.SequenceEqual(plinqResult, result);

        // Assert
        Assert.That(!ans);
    }
}
