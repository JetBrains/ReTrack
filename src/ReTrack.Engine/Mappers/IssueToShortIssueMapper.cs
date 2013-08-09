using ReTrack.Engine.Models;
using YouTrackSharp.Issues;

namespace ReTrack.Engine.Mappers
{
    public class IssueToShortIssueMapper
        : IMapper<Issue, ShortIssue>
    {
        public string BaseUrl { get; set; }

        public IssueToShortIssueMapper(string baseUrl)
        {
            if (baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.TrimEnd('/');
            }
            BaseUrl = baseUrl;
        }

        public ShortIssue Map(Issue origin)
        {
            dynamic issue = origin;

            // todo: analyze the incoming object and make sure these values can be provided from some property
            return new ShortIssue(origin.Id, issue.summary.ToString(), issue.type.ToString(), issue.state.ToString(), string.Format("{0}/issue/{1}", BaseUrl, origin.Id));
        }
    }
}