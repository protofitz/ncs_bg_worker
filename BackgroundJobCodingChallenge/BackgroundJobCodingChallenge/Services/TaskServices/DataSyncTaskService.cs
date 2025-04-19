using BackgroundJobCodingChallenge.Model;

public class DataSyncTaskService : ITaskService <DataSyncTask>
{

    public async Task<QueueTask<DataSyncTask>> ProcessTaskAsync(QueueTask<DataSyncTask> task, CancellationToken cancellationToken)
    {

        ///Do data sync task
        return task;
    }
}