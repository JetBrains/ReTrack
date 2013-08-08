using System;
using System.Net;
using System.Security.Authentication;
using System.Windows;
using System.Windows.Controls;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Features.Common.Options;
using JetBrains.UI.CrossFramework;
using JetBrains.UI.Options;
using JetBrains.UI.Settings;
using JetBrains.Application.Settings;
using ReTrack.Engine;

namespace ReTrack
{
    [OptionsPage(PID, "ReTrack",
        typeof (IconsForDefaultSettingsStorages),
        ParentId = ToolsPage.PID)]
    public partial class ReTrackOptionsPage : UserControl, IOptionsPage
    {
        private const string PID = "ReTrack.OptionsPage";

        public ReTrackOptionsPage(Lifetime lifetime, OptionsSettingsSmartContext ctx)
        {
            InitializeComponent();

            ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.Username, UsernameBox, TextBox.TextProperty);
            ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.YouTrackPassword, PasswordBox, TextBox.TextProperty);
            ctx.SetBinding(lifetime, (ReTrackSettingsReSharper s) => s.YouTrackUrl, UrlBox, TextBox.TextProperty);
        }

        public bool OnOk()
        {
            return true;
        }

        public bool ValidatePage()
        {
            // todo: attempt to connect to source (if specified)
            return true;
        }

        public EitherControl Control
        {
            get { return this; }
        }

        public string Id
        {
            get { return PID; }
        }

        private void UrlBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (UrlBox.Text.ToLowerInvariant().Contains(".myjetbrains.com") &&
                !(UrlBox.Text.ToLowerInvariant().EndsWith("/youtrack") ||
                  UrlBox.Text.ToLowerInvariant().EndsWith("/youtrack/")))
            {
                UrlBox.Text += "/youtrack";
            }
        }

        private void TestConnection_Click(object sender, RoutedEventArgs e)
        {
            var validator = new YouTrackConnectionValidator();
            var errorMessage = "";
            if (!validator.TryValidateConnection(new ReTrackSettings
            {
                Username = UsernameBox.Text,
                Password = PasswordBox.Password,
                Url = UrlBox.Text
            }, out errorMessage))
            {
                MessageBox.Show(
                    errorMessage,
                    "Test Connection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(
                    string.Format("Successfully connected to {0}.", UrlBox.Text),
                    "Test Connection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}