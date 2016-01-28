using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace TestApp.Models
{
    public class ViewModel : INotifyPropertyChanged
    {
        public const string BaseApiUrl = "https://api.reddit.com";
        public const string UserAgent = "Windows:TestApp:1.0 (by /u/hmcafee)";
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string subredditName;
        public string SubredditName
        {
            get
            {
                return subredditName;
            }
            set
            {
                if (value != subredditName)
                {
                    subredditName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isLoading = false;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                if (value != isLoading)
                {
                    isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public readonly ObservableCollection<Post> Posts = new ObservableCollection<Post>();
        public readonly ObservableCollection<string> Subreddits = new ObservableCollection<string>();

        public ViewModel()
        {
            Subreddits.Add("/r/all");
            Subreddits.Add("/r/aww");
            Subreddits.Add("/r/askreddit");
            Subreddits.Add("/r/microsoft");
            Subreddits.Add("/r/windowsphone");
        }

        public async void ChangeSubreddit(string subreddit)
        {
            // TODO: Verify the format of the incoming string
            SubredditName = subreddit;
            Posts.Clear();

            IsLoading = true;
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, BaseApiUrl + SubredditName);
                request.Headers.Add("User-Agent", Uri.EscapeDataString(UserAgent));
                var result = await httpClient.SendAsync(request);

                if (result.IsSuccessStatusCode)
                {
                    var listing = await result.Content.ReadAsAsync<RedditObject<SubredditListing>>();
                    var listingPosts = listing.Data.Children.Select(c => c.Data);
                    foreach (var post in listingPosts)
                    {
                        Posts.Add(post);
                    }
                    IsLoading = false;
                }
                else
                {
                    await new MessageDialog("We couldn't load this subreddit.", "Oops!").ShowAsync();
                    IsLoading = false;
                }
            }

            
        }
    }
}
