using System;

namespace SocialNetwork.Core
{
    public class Post
    {
        public string Content { get; set; }
        public DateTime PostedDateTime { get; set; }

        public Post() { }

        public Post(string content)
        {
            this.Content = content;
            this.PostedDateTime = DateTime.UtcNow;
        }

        public string ToOutputFormat()
        {
            int minutesAgo = (DateTime.UtcNow - this.PostedDateTime).Minutes;

            string plural = minutesAgo == 1 ? string.Empty : "s";

            return $"{this.Content} ({minutesAgo} minute{plural} ago)";
        }
    }
}
