namespace BackgroundJobCodingChallenge.Model;
public class EmailTask
{
    //model for email task
    public List<string> Emails { get; set; } = new List<string>();
    public string Subject { get; set; }
    public string Body { get; set; }
}