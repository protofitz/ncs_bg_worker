using BackgroundJobCodingChallenge.Model;

public class DataSyncTask{
    //model for data sync
    public DBCursor Source {get;set;}
    public guid previousJobId {get;set;}
    
}