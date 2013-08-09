using System;

namespace ReTrack.Engine.Models
{
    public class ShortComment
    {
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }

        public ShortComment(string author, DateTime created, string text)
        {
            Author = author;
            Created = created;
            Text = text;
        }
    }
}