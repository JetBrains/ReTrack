using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ReTrack.Annotations;

namespace ReTrack
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for YouTrackExplorerWindow.xaml
    /// </summary>
    public partial class YouTrackExplorerWindow : UserControl, INotifyPropertyChanged
    {
        private string _currentProjectShortName;
        private string _currentQuery;

        public YouTrackProxy Proxy { get; set; }

        public ObservableCollection<ShortProject> Projects { get; set; }
        public ObservableCollection<ShortIssue> Issues { get; set; }

        public string CurrentProjectShortName
        {
            get { return _currentProjectShortName; }
            set
            {
                if (value == _currentProjectShortName) return;
                _currentProjectShortName = value;
                OnPropertyChanged("CurrentProjectShortName");
            }
        }

        public string CurrentQuery
        {
            get { return _currentQuery; }
            set
            {
                if (value == _currentQuery) return;
                _currentQuery = value;
                OnPropertyChanged("CurrentQuery");
            }
        }

        public YouTrackExplorerWindow()
        {
            Projects = new ObservableCollection<ShortProject>();
            Issues = new ObservableCollection<ShortIssue>();

            InitializeComponent();

            // Refresh issue list when project changed
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentProjectShortName")
                {
                    QueryForIssues();
                }
            };
        }

        public void InitializeViewModel(YouTrackProxy proxy)
        {
            Proxy = proxy;
            Projects.Clear();
            Projects.Add(new ShortProject("", "Everything"));
            foreach (var project in Proxy.Projects("")) // TODO we can filter projects
            {
                Projects.Add(project);
            }
            CurrentProjectShortName = "";
        }

        protected void QueryForIssues()
        {
            Task.Factory.StartNew(() => Proxy.Query(CurrentProjectShortName, CurrentQuery))
                .ContinueWith(r =>
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => Issues.Clear()));

                    foreach (var issue in r.Result)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() => Issues.Add(issue)));
                    }
                });
        }

        private void TbInput_OnPopulating(object sender, PopulatingEventArgs e)
        {
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void OnInputBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var owner = (AutoCompleteBox) sender;
                var s = owner.Text;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AutoCompleteBox_LostFocus(object sender, RoutedEventArgs e)
        {
            QueryForIssues();
        }
    }
}