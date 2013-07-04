using JetBrains.Application.Communication;
using JetBrains.Application.Settings;

namespace ReTrack
{
    [SettingsKey(typeof (InternetSettings), "ReTrack Settings")]
    public class ReTrackSettingsReSharper : ReTrackSettings
    {
        [SettingsEntry("", "YouTrack Username")]
        public new string YouTrackUsername
        {
            get { return base.YouTrackUsername; }
            set { base.YouTrackUsername = value; }
        }

        [SettingsEntry("", "YouTrack Password")]
        public new string YouTrackPassword
        {
            get { return base.YouTrackPassword; }
            set { base.YouTrackPassword = value; }
        }

        [SettingsEntry("", "YouTrack server URL")]
        public new string YouTrackUrl
        {
            get { return base.YouTrackUrl; }
            set { base.YouTrackUrl = value; }
        }
    }
}