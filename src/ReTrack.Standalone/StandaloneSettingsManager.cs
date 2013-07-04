using System;
using System.Configuration;

namespace ReTrack.Standalone
{
    public class StandaloneSettingsManager
    {
        private readonly string _configurationFilePath;

        public StandaloneSettingsManager(string configurationFilePath)
        {
            _configurationFilePath = configurationFilePath;
        }

        public ReTrackSettings PopulateSettingsFromAppConfig()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(_configurationFilePath);

            return new ReTrackSettings
            {
                YouTrackUsername = GetAppSettingsValueOrDefault(configuration, "ReTrackSettings.YouTrackUsername", ""),
                YouTrackPassword = GetAppSettingsValueOrDefault(configuration, "ReTrackSettings.YouTrackPassword", ""),
                YouTrackUrl = GetAppSettingsValueOrDefault(configuration, "ReTrackSettings.YouTrackUrl", "")
            };
        }

        public void StoreSettingsInAppConfig(ReTrackSettings settings)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(_configurationFilePath);

            SetAppSettingsValue(configuration, "ReTrackSettings.YouTrackUsername", settings.YouTrackUsername);
            SetAppSettingsValue(configuration, "ReTrackSettings.YouTrackPassword", settings.YouTrackPassword);
            SetAppSettingsValue(configuration, "ReTrackSettings.YouTrackUrl", settings.YouTrackUrl);

            configuration.Save(ConfigurationSaveMode.Modified);
        }

        public string GetAppSettingsValueOrDefault(Configuration configuration, string key, string defaultValue)
        {
            if (configuration.AppSettings.Settings[key] == null)
            {
                return defaultValue;
            }
            return configuration.AppSettings.Settings[key].Value;
        }

        public void SetAppSettingsValue(Configuration configuration, string key, string value)
        {
            if (configuration.AppSettings.Settings[key] == null)
            {
                configuration.AppSettings.Settings.Add(key, value);
            }
            configuration.AppSettings.Settings[key].Value = value;
        }
    }
}