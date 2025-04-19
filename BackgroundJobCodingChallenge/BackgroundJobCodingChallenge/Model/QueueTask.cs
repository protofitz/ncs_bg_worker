using System;
using BackgroundJobCodingChallenge.Enum;

namespace BackgroundJobCodingChallenge.Model
{
    /// <summary>
    /// Represents a task in the background processing queue
    /// </summary>
    public class QueueTask <T>
    {
        public Context Context {get;set;}
        public int ContextId {get;set;}
        public Guid TaskId { get; set; }

        public string TaskType { get; set; } = string.Empty;

        public T? TaskData { get; set; }

        public TaskExecutionStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ScheduledFor { get; set; }

        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public int RetryCount { get; set; }

        public string Exception { get; set; } = string.Empty;
        public List<string> Messages {get;set;} = new List<string>();
    }

    public enum TaskExecutionStatus
    {
        Queued,
        Scheduled,
        Processing,
        Completed,
        Failed,
        Cancelled
    }

    public enum TaskPriority
    {
        Low,
        Normal,
        High,
        Critical
    }

    public enum TaskType 
    {
        Bulk,
        DataSync,
        FileProcessor,
        Email
    }
}
