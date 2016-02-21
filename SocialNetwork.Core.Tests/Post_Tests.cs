using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            Post mockPost = new Post
            {
                Content = testMessage,
                PostedDateTime = mockPostedDateTime
            };

            Assert.AreEqual(testMessage, mockPost.Content);
            Assert.AreEqual(mockPostedDateTime, mockPost.PostedDateTime);
        }

        [TestMethod]
        public void ToOutputFormat_returns_output_friendly_Post()
        {
            string testMessage = "Everything is awesome";

            Post testPost = new Post(testMessage);
            
            Assert.AreEqual($"{testMessage} (0 seconds ago)", testPost.ToOutputFormat());
        }

        [TestMethod]
        public void ToOutputFormat_returns_correct_grammar_for_post_one_minute_ago()
        {
            string testMessage = "Everything may or may not be awesome";

            Post postOneMinuteAgo = new Post
            {
                Content = testMessage,
                PostedDateTime = DateTime.UtcNow.AddMinutes(-1)
            };
            
            Assert.AreEqual($"{testMessage} (1 minute ago)", postOneMinuteAgo.ToOutputFormat());
        }

        [TestMethod]
        public void ToOutPutFormat_returns_appropriate_time_units()
        {
            string testMessageSeconds = "Good morning world";
            string testMessageMinutes = "Good night world";
            string testMessageHours   = "Good afternoon world";
            string testMessageDays    = "Good evening world";

            Post testPostSeconds = new Post
            {
                Content = testMessageSeconds,
                PostedDateTime = DateTime.UtcNow.AddSeconds(-5)
            };

            Assert.AreEqual($"{testMessageSeconds} (5 seconds ago)", testPostSeconds.ToOutputFormat());

            Post testPostMinutes = new Post
            {
                Content = testMessageMinutes,
                PostedDateTime = DateTime.UtcNow.AddMinutes(-7)
            };

            Assert.AreEqual($"{testMessageMinutes} (7 minutes ago)", testPostMinutes.ToOutputFormat());

            Post testPostHours = new Post
            {
                Content = testMessageHours,
                PostedDateTime = DateTime.UtcNow.AddHours(-1)
            };

            Assert.AreEqual($"{testMessageHours} (1 hour ago)", testPostHours.ToOutputFormat());

            Post testPostDays = new Post
            {
                Content = testMessageDays,
                PostedDateTime = DateTime.UtcNow.AddDays(-9)
            };

            Assert.AreEqual($"{testMessageDays} (9 days ago)", testPostDays.ToOutputFormat());
        }
    }
}
