using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    class SubredditListing
    {
        [JsonProperty(PropertyName = "modhash")]
        public string ModHash { get; set; }

        [JsonProperty(PropertyName = "children")]
        public List<RedditObject<Post>> Children { get; set; }
    }
}
