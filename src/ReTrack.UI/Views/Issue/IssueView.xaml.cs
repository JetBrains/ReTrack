using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReTrack.UI.Infrastructure;

namespace ReTrack.UI.Views.Issue
{
    public partial class IssueView
        : UserControl, IViewModelBoundElement<IssueViewModel>
    {
        public IssueView()
        {
            InitializeComponent();
        }

        public IssueViewModel ViewModel
        {
            get { return DataContext as IssueViewModel; }
            private set { DataContext = value; }
        }
    }
}
