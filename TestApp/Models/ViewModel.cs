using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class ViewModel : INotifyPropertyChanged
    {
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

        public readonly ObservableCollection<Post> Posts = new ObservableCollection<Post>();

        public ViewModel()
        {
            Posts.Add(new Post()
            {
                Title = "Test Post"
            });
        }
    }
}
