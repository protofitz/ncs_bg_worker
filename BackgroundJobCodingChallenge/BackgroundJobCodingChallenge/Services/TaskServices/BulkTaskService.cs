using BackgroundJobCodingChallenge.Model;

public class BulkTaskService:ITaskService<BulkTask>
{

    public async Task<QueueTask<BulkTask>> ProcessTaskAsync(QueueTask<BulkTask> task, CancellationToken cancellationToken)
    {
         //Not pictured here: a scoped data access object so that we can update the task status with new details as we go. 
        // We'll pass them back in the task as well, but including the DBContext here will allow us to keep the tasks updated for long running operations.

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