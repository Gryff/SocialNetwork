using System;

namespace SocialNetwork.Core.Tests.Helpers
{
    public class PostWithFixedDateTime : Post
    {
        public PostWithFixedDateTime(string content, DateTime postedDateTime)
        {
            this.Content = content;
            this.PostedDateTime = postedDateTime;
        }
    }
}
