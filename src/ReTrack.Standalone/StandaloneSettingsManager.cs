using System;
using System.Configuration;
using ReTrack.Engine;

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
                Username = GetAppSettingsValueOrDefault(configuration, "ReTrackSettings.Username", ""),
                Password = GetAppSettingsValueOrDefault(configuration, "ReTrackSettings.Password", ""),
                Url = GetAppSettingsValueOrDefault(configuration, "ReTrackSettings.Url", "")
            };
        }

        public void StoreSettingsInAppConfig(ReTrackSettings settings)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(_configurationFilePath);

            SetAppSettingsValue(configuration, "ReTrackSettings.Username", settings.Username);
            SetAppSettingsValue(configuration, "ReTrackSettings.Password", settings.Password);
            SetAppSettingsValue(configuration, "ReTrackSettings.Url", settings.Url);

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