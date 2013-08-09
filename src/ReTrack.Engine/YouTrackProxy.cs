using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ReTrack.Engine.Mappers;
using ReTrack.Engine.Models;
using YouTrackSharp.Infrastructure;
using YouTrackSharp.Issues;
using YouTrackSharp.Projects;

namespace ReTrack.Engine
{
    public class YouTrackProxy : IDisposable
    {
        private readonly IConnection connection;
        public string BaseUrl { get; set; }
        private const int sensibleQueryLimit = 20;

        public YouTrackProxy(ReTrackSettings s) :
            this(s.Username, s.Password, new Uri(s.Url))
        {
            BaseUrl = s.Url;
        }

        public YouTrackProxy(string username, string password, Uri url)
        {
            connection = new Connection(url.Host, url.Port, url.Scheme == "https", url.PathAndQuery);
            connection.Authenticate(username, password);
        }

        /// <summary>
        /// Simple query, yields <c>ShortProject</c> objects. Can drill down later if needed.
        /// </summary>
        /// <param name="startsWith"></param>
        /// <returns></returns>
        public IList<ShortProject> Projects(string startsWith)
        {
            startsWith = startsWith.ToLowerInvariant();

            var pm = new ProjectManagement(connection);
            var mapper = new ProjectToShortProjectMapper();
            return pm.GetProjects().Where(p => p.Name.ToLowerInvariant().StartsWith(startsWith) || p.ShortName.ToLowerInvariant().StartsWith(startsWith))
                .Select(mapper.Map)
                .ToList();
        }

        /// <summary>
        /// Simple query, yields <c>ShortIssue</c> objects. Can drill down later if needed.
        /// </summary>
        /// <param name="projectShortName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<ShortIssue> Query(string projectShortName, string query)
        {
            var im = new IssueManagement(connection);
            if (!string.IsNullOrEmpty(projectShortName))
            {
                query = string.Format("project: {0} {1}", projectShortName, query);
            }

            var mapper = new IssueToShortIssueMapper(BaseUrl);
            return im.GetIssuesBySearch(query, sensibleQueryLimit, 0)
                .Select(mapper.Map)
                .ToList();
        }

        /// <summary>
        /// Simple query, yields <c>ShortComment</c> objects.
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns></returns>
        public IList<ShortComment> QueryComments(string issueId)
        {
            var im = new IssueManagement(connection);

            var mapper = new CommentToShortCommentMapper();
            return im.GetCommentsForIssue(issueId)
                .Select(mapper.Map)
                .ToList();
        }

        public void Dispose()
        {
            if (connection.IsAuthenticated) connection.Logout();
        }
    }
}