using System;
using ReTrack.Engine.Models;
using YouTrackSharp.Issues;

namespace ReTrack.Engine.Mappers
{
    public class CommentToShortCommentMapper
        : IMapper<Comment, ShortComment>
    {
        public ShortComment Map(Comment origin)
        {
            var created = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            created = created.AddMilliseconds(origin.Created).ToUniversalTime();

            return new ShortComment(origin.Author, created, origin.Text);
        }
    }
}