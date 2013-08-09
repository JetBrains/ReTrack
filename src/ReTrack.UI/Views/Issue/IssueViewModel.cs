using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using ReTrack.Engine;
using ReTrack.Engine.Models;

namespace ReTrack.UI.Views.Issue
{
    public class IssueViewModel
        : ViewModelBase
    {
        private string _summary;
        private string _state;
        private string _type;
        private string _id;

        public YouTrackProxy Proxy { get; set; }

        public ObservableCollection<ShortComment> Comments { get; set; }

        public string Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Type
        {
            get { return _type; }
            set
            {
                if (value == _type) return;
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        public string State
        {
            get { return _state; }
            set
            {
                if (value == _state) return;
                _state = value;
                OnPropertyChanged("State");
            }
        }

        public string Summary
        {
            get { return _summary; }
            set
            {
                if (value == _summary) return;
                _summary = value;
                OnPropertyChanged("Summary");
            }
        }

        public IssueViewModel()
        {
            Comments = new ObservableCollection<ShortComment>();
        }

        public void Initialize(YouTrackProxy proxy, ShortIssue issue)
        {
            Proxy = proxy;

            Id = issue.ID;
            Type = issue.Type;
            State = issue.State;
            Summary = issue.Summary;

            QueryForComments(issue.ID);
        }

        protected void QueryForComments(string issueId)
        {
            Task.Factory.StartNew(() => Proxy.QueryComments(issueId)).ContinueWith(
                r =>
                {
                    Application.Current.Dispatcher.Invoke(new Action(() => Comments.Clear()));
                    foreach (var comment in r.Result)
                    {
                        Application.Current.Dispatcher.Invoke(new Action(() => Comments.Add(comment)));
                    }
                });
        }
    }
}