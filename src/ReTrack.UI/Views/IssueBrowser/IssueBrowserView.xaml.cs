using System.Windows.Controls;
using ReTrack.UI.Infrastructure;

namespace ReTrack.UI.Views.IssueBrowser
{
    public partial class IssueBrowserView 
        : UserControl, IViewModelBoundElement<IssueBrowserViewModel>
    {
        public IssueBrowserView()
        {
            InitializeComponent();
        }

        public IssueBrowserViewModel ViewModel
        {
            get { return DataContext as IssueBrowserViewModel; }
            private set { DataContext = value; }
        }
    }
}