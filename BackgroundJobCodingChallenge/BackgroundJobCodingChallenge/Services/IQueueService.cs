using BackgroundJobCodingChallenge.Model;

namespace BackgroundJobCodingChallenge.Services;

public interface IQueueService
{
	Task EnqueueAsync<T>(QueueTask<T> task, CancellationToken cancellationToken);
	Task<T> DequeueAsync<T>(CancellationToken cancellationToken);
	Task<bool> IsEmptyAsync(CancellationToken cancellationToken);

}
