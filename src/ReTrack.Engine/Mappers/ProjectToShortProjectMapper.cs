using ReTrack.Engine.Models;
using YouTrackSharp.Projects;

namespace ReTrack.Engine.Mappers
{
    public class ProjectToShortProjectMapper
        : IMapper<Project, ShortProject>
    {
        public ShortProject Map(Project origin)
        {
            return new ShortProject(origin.ShortName, origin.Name);
        }
    }
}