using BackgroundJobCodingChallenge.Model;

public class FileProcessorTaskService : ITaskService<FileProcessorTask> 
{

    public async Task<QueueTask<FileProcessorTask>> ProcessTaskAsync(QueueTask<FileProcessorTask> task, CancellationToken cancellationToken)
    {
          //Not pictured here: a scoped data access object so that we can update the task status with new details as we go. 
        // We'll pass them back in the task as well, but including the DBContext here will allow us to keep the tasks updated for long running operations.

        /* General steps
            Pull the file from Azure Blob Storage
            Parse the CSV into a data structure
            store that object in the database
            pass the task back with results.
        */
        ///Do data sync task
        return task;
    }
}