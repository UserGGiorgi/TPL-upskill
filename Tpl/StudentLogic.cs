namespace Tpl;

public static class StudentLogic
{
    public static Task TaskCreated()
    {
        return new Task(() => Console.Write("something"));
    }

    public static Task WaitingForActivation()
    {
        var tcs = new TaskCompletionSource<object>();
        return tcs.Task;
    }

    public static Task WaitingToRun()
    {
        return Task.Factory.StartNew(
       () =>
       {
           // Task does nothing here
       },
       CancellationToken.None,
       TaskCreationOptions.None,
       TaskScheduler.Default);
    }

    public static Task Running()
    {
        var task = Task.Run(() =>
        {
            Console.WriteLine("Task is working");
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();
        });

        return task;
    }

    public static Task RanToCompletion()
    {
        return Task.FromResult("Completed");
    }

    public static Task WaitingForChildrenToComplete()
    {
        var childTask1 = Task.Run(() => Task.Delay(1000));
        var childTask2 = Task.Run(() => Task.Delay(5000));

        return Task.WhenAll(childTask1, childTask2);
    }

    public static Task IsCompleted()
    {
        return Task.FromResult(true);
    }

    public static Task IsCancelled()
    {
        using var cts = new CancellationTokenSource();
        _ = cts.CancelAsync();
        var task = Task.Delay(Timeout.Infinite, cts.Token);
        return task;
    }

    public static Task IsFaulted()
    {
        return Task.FromException(new InvalidOperationException("Faulted task"));
    }

    public static List<int> ForceParallelismPlinq()
    {
        var testList = Enumerable.Range(1, 300).ToList();

        // Use PLINQ to process the list in parallel
        var result = testList
            .AsParallel()
            .Where(x => x > 0)
            .OrderBy(x => Guid.NewGuid())
            .ToList();
        return result;
    }
}
