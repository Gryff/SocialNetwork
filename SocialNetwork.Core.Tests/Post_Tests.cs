using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork.Core.Tests.Helpers;

namespace SocialNetwork.Core.Tests
{
    [TestClass]
    public class Post_Tests
    {
        [TestMethod]
        public void Instantiated_Post_contains_content_and_time_of_post()
        {
            string testMessage = "Good morning everyone!";
            DateTime mockPostedDateTime = DateTime.UtcNow;

            PostWithFixedDateTime mockPost = new PostWithFixedDateTime(testMessage, mockPostedDateTime);

            Assert.AreEqual(testMessage, mockPost.Content);
            Assert.AreEqual(mockPostedDateTime, mockPost.PostedDateTime);
        }

        [TestMethod]
        public void ToOutputFormat_returns_output_friendly_Post()
        {
            string testMessage = "Everything is awesome";

            Post testPost = new Post(testMessage);
            
            Assert.AreEqual($"{testMessage} (0 minutes ago)", testPost.ToOutputFormat());
        }

        [TestMethod]
        public void ToOutputFormat_returns_correct_grammar_for_post_one_minute_ago()
        {
            string testMessage = "Everything may or may not be awesome";

            PostWithFixedDateTime postOneMinuteAgo = new PostWithFixedDateTime(
                testMessage,
                DateTime.UtcNow.AddMinutes(-1));
            
            Assert.AreEqual($"{testMessage} (1 minute ago)", postOneMinuteAgo.ToOutputFormat());
        }
    }
}
