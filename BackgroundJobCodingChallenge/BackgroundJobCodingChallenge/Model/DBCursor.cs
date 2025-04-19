namespace BackgroundJobCodingChallenge.Model
{
    /// <summary>
    /// Represents a database cursor with identification information
    /// May also need some kind of field/table listing in order to actually map out to different DB Settings.
    /// </summary>
    public class DBCursor
    {
        public string Id { get; set; }

        public string DbId { get; set; }

        public string TenantId { get; set; }
    }
}