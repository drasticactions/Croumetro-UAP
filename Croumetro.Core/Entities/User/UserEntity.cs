using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite.Net.Attributes;

namespace Croumetro.Core.Entities.User
{
    public class UserEntity
    {
        [JsonProperty("cover_image_url_https")]
        public string CoverImage { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("favorites_count")]
        public long FavoritesCount { get; set; }

        [JsonProperty("follow_request_sent")]
        public bool IsFollowRequest { get; set; }

        [JsonProperty("followers_count")]
        public long FollowersCount { get; set; }

        [JsonProperty("following")]
        public bool IsFollowing { get; set; }

        [JsonProperty("friends_count")]
        public long FriendsCount { get; set; }

        [PrimaryKey]
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImage { get; set; }

        [JsonProperty("protected")]
        public bool IsProtected { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("statuses_count")]
        public long StatusCount { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
