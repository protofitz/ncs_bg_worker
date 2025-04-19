using BackgroundJobCodingChallenge.Model;

public class FileProcessorTaskService : ITaskService<FileProcessorTask> 
{

    public async Task<QueueTask<FileProcessorTask>> ProcessTaskAsync(QueueTask<FileProcessorTask> task, CancellationToken cancellationToken)
    {

        ///Do data sync task
        return task;
    }
}