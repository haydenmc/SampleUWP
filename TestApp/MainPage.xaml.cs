using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TestApp.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ViewModel ViewModel
        {
            get
            {
                return ((App)Application.Current).ViewModel;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
        }

        private void SubredditPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ChangeSubreddit("/r/all");
        }

        private void SubredditList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ChangeSubreddit((string)SubredditList.SelectedItem);
        }

        private async void PostList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostList.SelectedItem == null)
            {
                return;
            }
            var uri = new Uri(((Post)PostList.SelectedItem).Url);
            PostList.SelectedItem = null;
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
