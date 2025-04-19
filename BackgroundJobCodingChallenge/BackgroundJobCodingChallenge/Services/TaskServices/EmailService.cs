using BackgroundJobCodingChallenge.Model;

public class EmailService : ITaskService <EmailTask>
{
    public async Task<QueueTask<EmailTask>> ProcessTaskAsync(QueueTask<EmailTask> task, CancellationToken cancellationToken)
    {
       
            task.Status = TaskExecutionStatus.Processing;
            task.Messages.Add("Begin Processing email task...");
            //set task pid to Task.CurrentId
            
            foreach(var email in task.TaskData.Emails)
            {
                var response = SendEmailAsync(email, task.TaskData.Subject, task.TaskData.Body);
                await response;
            }

        return task;
    }

    private async Task<string> SendEmailAsync(string email, string subject, string body)
    {
        // Simulate sending an email
        var send  = await Task.Run(() =>  $"Sending email to {email} with subject {subject} and body {body}. Processed by Task ID: {Task.CurrentId}");
        return send;
    }
}

