using JetBrains.Application.Communication;
using JetBrains.Application.Settings;
using ReTrack.Engine;

namespace ReTrack
{
    [SettingsKey(typeof(InternetSettings), "ReTrack Settings")]
    public class ReTrackSettingsReSharper : ReTrackSettings
    {
        [SettingsEntry("", "YouTrack Username")]
        public new string YouTrackUsername
        {
            get { return base.Username; }
            set { base.Username = value; }
        }

        [SettingsEntry("", "YouTrack Password")]
        public new string YouTrackPassword
        {
            get { return base.Password; }
            set { base.Password = value; }
        }

        [SettingsEntry("", "YouTrack server URL")]
        public new string YouTrackUrl
        {
            get { return base.Url; }
            set { base.Url = value; }
        }
    }
}