namespace BackgroundJobCodingChallenge.Model;
public class FileProcessorTask
{
    //Model for file processing
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public string Destination { get; set; }
    public string Source { get; set; }
    public string Status { get; set; }
}