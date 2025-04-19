using System;
using System.ComponentModel;
using 
using BackgroundJobCodingChallenge.Model;
using BackgroundJobCodingChallenge.Services;

public class QueueProcessingService : BackgroundService{
    //Some kind of logger
    private readonly ILogger<QueueProcessingService> _logger;

    private readonly IQueueService _queueService;
    
    private TaskType tryParseResult;
    private readonly IDatabaseService _databaseService;

    public QueueProcessingService(ILogger<QueueProcessingService> logger, IQueueService queueService, IDatabaseService databaseService)
    {
        _logger = logger;
        _queueService = queueService;
        _databaseService = databaseService;
    }


    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("QueueProcessingService starting at: {time}", DateTimeOffset.Now);
            
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("QueueProcessingService running at: {time}", DateTimeOffset.Now);
                
                
                Task currentTask;
                //this runs until we stop it
                while(!stoppingToken.IsCancellationRequested)
                {
                    //get a task
                    var task = _queueService.DequeueAsync<QueueTask<object>>(stoppingToken).Result;
                    if (task == null)
                    {
                        //if the task is null, sleeep for a minute and then check again
                        _logger.LogInformation("No tasks in the queue. Sleeping for 5 seconds");
                        Task.Delay(5000, stoppingToken).Wait();
                        continue;
                    }
                    try
                    {
                        
                        // Process the task
                        _logger.LogInformation("Processing task {taskId} of type {taskType}", task.TaskId, task.TaskType);
                        
                        // Simulate processing
                        //switching on task type. Handlers call out to their respective services to process the task
                        if (Enum.TryParse<TaskType>(task.TaskType, out var parsedTaskType))
                        {
                            switch (parsedTaskType)
                            {
                                //We probably need some scoped data access and services here, I'm not sure of the syntax on that, so I'm just calling it out.
                                case TaskType.Email:
                                    // Process Email task
                                    var emailTaskService = new EmailService();
                                    currentTask = RunEmailTaskAsync(emailTaskService, task, stoppingToken);
                                    break;
                                case TaskType.Bulk:
                                    // Process Bulk task
                                    var bulkTaskService = new BulkTaskService();
                                    currentTask = RunBulkTaskAsync(bulkTaskService, task, stoppingToken);
                                    break;
                                case TaskType.DataSync:
                                    // Process DataSync task
                                    var dataSyncTaskService = new DataSyncTaskService();
                                    currentTask = RunDataSyncTaskAsync(dataSyncTaskService, task, stoppingToken);
                                    break;
                                case TaskType.FileProcessor:
                                    // Process CSVProcessor task    
                                    var fileProcessorTaskService = new FileProcessorTaskService();
                                    currentTask = RunFileProcessorTaskAsync(fileProcessorTaskService, task, stoppingToken);
                                    break;
                                default:
                                    throw new NotSupportedException($"Task type {task.TaskType} is not supported.");
                            }
                        }
                        else
                        {
                            throw new ArgumentException($"Invalid task type: {task.TaskType}");
                        }

                        // Hang a continueWith off of the task so that we can update the status when it completes but we can continue dispatching for now.
                        _ = currentTask.ContinueWith(async t => {await handleTaskCompletion(t, task);}, TaskContinuationOptions.ExecuteSynchronously);
                        

                        //set task as processing
                        task.Status = TaskExecutionStatus.Processing;
                        task.StartedAt = DateTime.UtcNow;
                        
                        await _databaseService.UpdateAsync(task);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing task {taskId}", task.TaskId);
                        
                        // Mark the task as failed
                        task.Status = TaskExecutionStatus.Failed;
                        task.Exception = ex.Message;
                        task.Messages.Add($"Exception Caught! Error: {ex.Message}");
                        
                        //always store the result in the database. 
                        await _databaseService.UpdateAsync(task);
                    }
                }

            }
            
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "Error occurred in QueueProcessingService");
            throw;
        }
    }

    private async Task RunFileProcessorTaskAsync(FileProcessorTaskService fileProcessorTaskService, QueueTask<object> task, CancellationToken stoppingToken)
    {
        //call EmailTaskService
        throw new NotImplementedException();
    }

    private async Task RunDataSyncTaskAsync(DataSyncTaskService dataSyncTaskService, QueueTask<object> task, CancellationToken stoppingToken)
    {
        //call DataSyncTaskService
        throw new NotImplementedException();
    }

    private async Task RunBulkTaskAsync(BulkTaskService bulkTaskService, QueueTask<object> task, CancellationToken stoppingToken)
    {
        //Call BulkTaskService
        throw new NotImplementedException();
    }

    private async Task RunEmailTaskAsync(EmailService emailTaskService, QueueTask<object> task, CancellationToken stoppingToken)
    {
        //Call FileProcessorTaskService
        throw new NotImplementedException();
    }

    private List<QueueTask<object>> GetTasks(CancellationToken cancellationToken)
    {
        // Get tasks from the queue
        Console.WriteLine("Getting tasks from the queue...");

        return new List<QueueTask<object>>();
    }

    private async Task handleTaskCompletion(Task t, QueueTask<object> task)
    {
        if (t.IsFaulted)
        {
            // Handle task failure
            _logger.LogError(t.Exception, "Task {taskId} failed", task.TaskId);
            
            // Mark the task as failed
            task.Status = TaskExecutionStatus.Failed;
            task.Exception = t.Exception?.Message ?? "Execption not found";
            task.Messages.Add($"Error: {t.Exception?.Message ?? "Execption not found"}");
            task.CompletedAt = DateTime.UtcNow;
        }
        else
        {
            // Mark the task as completed
            task.Status = TaskExecutionStatus.Completed;
            task.CompletedAt = DateTime.UtcNow;
        }
        await _databaseService.UpdateAsync(task); //This may not work without some scoping wizardry. I'm not sure
    }
    
}