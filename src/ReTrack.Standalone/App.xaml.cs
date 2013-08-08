using System;
using System.Configuration;
using System.Windows;
using ReTrack.Engine;

namespace ReTrack.Standalone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly StandaloneSettingsManager _standaloneSettingsManager;

        public App()
        {
            _standaloneSettingsManager = new StandaloneSettingsManager(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }

        public ReTrackSettings Settings { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Settings = _standaloneSettingsManager.PopulateSettingsFromAppConfig();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _standaloneSettingsManager.StoreSettingsInAppConfig(Settings);

            base.OnExit(e);
        }
    }
}