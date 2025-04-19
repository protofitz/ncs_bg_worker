using BackgroundJobCodingChallenge.Model;

public class BulkTaskService:ITaskService<BulkTask>
{

    public async Task<QueueTask<BulkTask>> ProcessTaskAsync(QueueTask<BulkTask> task, CancellationToken cancellationToken)
    {
        if (task.TaskData is not BulkTask bulkTask)
        {
            throw new ArgumentException("Invalid task data type");
        }
        var taskChunks = getTaskChuncks(task.TaskData);

        // Process the bulk task
        var tasks = new List<Task>();
        foreach (var item in taskChunks)
        {
            DoWork();
        }

        await Task.WhenAll(tasks);
        return task;
    }

    private List<object> getTaskChuncks(BulkTask taskData)
    {
        throw new NotImplementedException();
    }

    private Task DoWork(){
        throw new NotImplementedException();
    }

   
}