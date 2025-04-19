using BackgroundJobCodingChallenge.Model;

public interface ITaskService<T>
{
    Task<QueueTask<T>> ProcessTaskAsync(QueueTask<T> task, CancellationToken cancellationToken);
}