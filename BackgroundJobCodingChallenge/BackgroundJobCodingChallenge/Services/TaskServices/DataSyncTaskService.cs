using BackgroundJobCodingChallenge.Model;

public class DataSyncTaskService : ITaskService <DataSyncTask>
{

    public async Task<QueueTask<DataSyncTask>> ProcessTaskAsync(QueueTask<DataSyncTask> task, CancellationToken cancellationToken)
    {

        //Not pictured here: a scoped data access object so that we can update the task status with new details as we go. 
        // We'll pass them back in the task as well, but including the DBContext here will allow us to keep the tasks updated for long running operations.

        /* General steps
            connect to the database
            start chucnking through data according to taskData attributes including chunck size and whatever cursor we're using for this.
            commit our transaction once we've finished the chunk.
            The Task runnable will return the cursor position so that the next task can pick it up for a new chunk.       
            update the task record in the DB with additonal messages between transactions to track progress.
            pass the task back with results.
        */
        return task;
    }
}