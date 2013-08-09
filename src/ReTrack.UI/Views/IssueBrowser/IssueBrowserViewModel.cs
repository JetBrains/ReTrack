using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ReTrack.Engine;
using ReTrack.Engine.Models;
using ReTrack.UI.Infrastructure;
using ReTrack.UI.Views.Issue;

namespace ReTrack.UI.Views.IssueBrowser
{
    public class IssueBrowserViewModel
        : ViewModelBase
    {
        private string _currentProjectShortName;
        private string _currentQuery;

        public YouTrackProxy Proxy { get; set; }

        public ICommand DisplayIssueCommand { get; set; }

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

        public IssueBrowserViewModel()
        {
            Projects = new ObservableCollection<ShortProject>();
            Issues = new ObservableCollection<ShortIssue>();

            DisplayIssueCommand = new DelegateCommand<string>(id =>
            {
                var page = new IssueView();
                page.ViewModel.Initialize(Proxy, Issues.FirstOrDefault(i => i.ID == id));

                var window = new Window();
                window.Title = string.Format("View Issue {0}", id);
                window.Content = page;
                window.Width = 500;
                window.Height = 500;
                window.Show();
            });

            // Refresh issue list when project changed   
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentProjectShortName" || args.PropertyName == "CurrentQuery")
                {
                    QueryForIssues();
                }
            };
        }

        public void Initialize(YouTrackProxy proxy)
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
            Task.Factory.StartNew(() => Proxy.Query(CurrentProjectShortName, CurrentQuery)).ContinueWith(
                r =>
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => Issues.Clear()));
                    foreach (var issue in r.Result)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() => Issues.Add(issue)));
                    }
                });
        }
    }
}