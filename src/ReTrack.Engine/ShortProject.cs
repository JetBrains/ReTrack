namespace ReTrack
{
    /// <summary>
    /// Short project
    /// </summary>
    public class ShortProject
    {
        public string ShortName { get; set; }
        public string Name { get; set; }

        public ShortProject(string shortName, string name)
        {
            ShortName = shortName;
            Name = name;
        }
    }
}