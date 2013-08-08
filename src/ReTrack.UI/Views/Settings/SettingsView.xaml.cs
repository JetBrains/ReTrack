using System.Windows.Controls;
using ReTrack.UI.Infrastructure;

namespace ReTrack.UI.Views.Settings
{
    public partial class SettingsPage
        : UserControl, IViewModelBoundElement<SettingsViewModel>
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        public SettingsViewModel ViewModel
        {
            get { return DataContext as SettingsViewModel; }
            private set { DataContext = value; }
        }
    }
}