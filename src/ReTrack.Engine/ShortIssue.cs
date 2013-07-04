namespace ReTrack
{
    /// <summary>
    /// ID and summary - two things the user actually gets to see.
    /// </summary>
    public class ShortIssue
    {
        public string ID { get; set; }
        public string Summary { get; set; }

        public ShortIssue(string id, string summary)
        {
            ID = id;
            Summary = summary;
        }
    }
}