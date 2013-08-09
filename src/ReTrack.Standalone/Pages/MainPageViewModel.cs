using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ReTrack.Engine;
using ReTrack.UI;
using ReTrack.UI.Infrastructure;
using ReTrack.UI.Views.IssueBrowser;
using ReTrack.UI.Views.Settings;

namespace ReTrack.Standalone.Pages
{
    public class MainPageViewModel
        : ViewModelBase
    {
        public ICommand OpenSettingsCommand { get; set; }
        public IssueBrowserViewModel IssueBrowserViewModel { get; set; }

        public MainPageViewModel()
        {
            OpenSettingsCommand = new DelegateCommand(OpenSettings);
        }

        private void OpenSettings()
        {
            var retrackApp = (App)Application.Current;

            var page = new SettingsPage();
            page.ViewModel.Username = retrackApp.Settings.Username;
            page.ViewModel.Password = retrackApp.Settings.Password;
            page.ViewModel.Url = retrackApp.Settings.Url;

            var window = new Window();
            window.Title = "ReTrack Settings";
            window.Content = page;
            window.Width = 300;
            window.Height = 150;
            window.ShowDialog();

            retrackApp.Settings.Username = page.ViewModel.Username;
            retrackApp.Settings.Password = page.ViewModel.Password;
            retrackApp.Settings.Url = page.ViewModel.Url;

            InitializeViewFromSettings(retrackApp.Settings);
        }

        public void InitializeViewFromSettings(ReTrackSettings settings)
        {
            if (!string.IsNullOrEmpty(settings.Url))
            {
                var proxy = new YouTrackProxy(settings);

                IssueBrowserViewModel.Initialize(proxy);
            }
        }
    }
}
