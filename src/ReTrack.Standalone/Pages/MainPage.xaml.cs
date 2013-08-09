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
using ReTrack.UI.Views.Settings;

namespace ReTrack.Standalone.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage 
        : Page, IViewModelBoundElement<MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();

            ViewModel.IssueBrowserViewModel = IssueBrowser.ViewModel;

            var retrackApp = (App)Application.Current;
            ViewModel.InitializeViewFromSettings(retrackApp.Settings);
        }

        public MainPageViewModel ViewModel
        {
            get { return DataContext as MainPageViewModel; }
            private set { DataContext = value; }
        }
    }
}
