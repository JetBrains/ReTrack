using System.Windows;
using System.Windows.Input;
using ReTrack.Engine;
using ReTrack.UI.Infrastructure;

namespace ReTrack.UI.Views.Settings
{
    public class SettingsViewModel
        : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _url;

        public string Username
        {
            get { return _username; }
            set
            {
                if (value == _username) return;
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public string Url
        {
            get { return _url; }
            set
            {
                if (value == _url) return;
                _url = value;
                OnPropertyChanged("Url");
            }
        }

        public ICommand TestConnectionCommand { get; set; }
        public ICommand SaveSettingsCommand { get; set; }

        public SettingsViewModel()
        {
            TestConnectionCommand = new DelegateCommand(TestConnection);

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Url")
                {
                    if (Url.ToLowerInvariant().Contains(".myjetbrains.com") &&
                        !(Url.ToLowerInvariant().EndsWith("/youtrack") ||
                          Url.ToLowerInvariant().EndsWith("/youtrack/")))
                    {
                        Url = Url.TrimEnd('/') + "/youtrack";
                    }
                }
            };
        }

        protected void TestConnection()
        {
            var validator = new YouTrackConnectionValidator();
            var errorMessage = "";
            if (!validator.TryValidateConnection(new ReTrackSettings
            {
                Username = Username,
                Password = Password,
                Url = Url
            }, out errorMessage))
            {
                MessageBox.Show(
                    errorMessage,
                    "Test Connection", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(
                    string.Format("Successfully connected to {0}.", Url),
                    "Test Connection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}