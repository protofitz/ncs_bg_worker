using System.Collections.Concurrent;
using BackgroundJobCodingChallenge.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BackgroundJobCodingChallenge.Services
{
    public class QueueService : IQueueService
    {
        //I know the syntax in here is all wrong but you get the idea. 
        private readonly UnboundedChannel<QueueTask> _queue = Channel.CreateUnbounded<QueueTask>();
        public QueueService(UnboundedChannel<QueueTask> queue)
        {
            _queue = queue;
        }


        public void Enqueue(QueueTask item)
        {
            _queue.Writer.TryWrite(item);
        }

        public QueueTask<object> Dequeue()
        {
            //stubbed implementation
            return new QueueTask<object>();
          
            throw new InvalidOperationException("Queue is empty.");
        }

        public bool IsEmpty()
        {
            return _queue.IsEmpty;
        }
    }
}