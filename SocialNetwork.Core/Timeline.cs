using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Core
{
    public class Timeline
    {
        private readonly List<Post> _posts = new List<Post>();

        public void AddPost(Post post) => this._posts.Add(post);

        public string FormatPost(Post post)
        {
            int minutesAgo = (DateTime.UtcNow - post.PostedDateTime).Minutes;

            string plural = minutesAgo == 1 ? string.Empty : "s";

            return $"{post.Content} ({minutesAgo} minute{plural} ago)";
        }

        public IEnumerable<string> GetTimeline() => this._posts.Select(this.FormatPost);
    }
}
