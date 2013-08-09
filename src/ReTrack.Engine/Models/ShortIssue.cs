namespace ReTrack.Engine.Models
{
    public class ShortIssue
    {
        public string ID { get; set; }
        public string Summary { get; set; }        
        public string Type { get; set; }        
        public string State { get; set; }
        public string Url { get; set; }

        public ShortIssue(string id, string summary, string type, string state, string url)
        {
            ID = id;
            Summary = summary;
            Type = type;
            State = state;
            Url = url;
        }
    }
}