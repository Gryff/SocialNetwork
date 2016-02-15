using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork.Core.Tests.Helpers;

namespace SocialNetwork.Core.Tests
{
    [TestClass]
    public class Timeline_Tests
    {
        [TestMethod]
        public void FormatPost_returns_output_friendly_Post()
        {
            string testMessage = "Everything is awesome";

            Post testPost = new Post(testMessage);

            string formattedPost = new Timeline().FormatPost(testPost);

            Assert.AreEqual($"{testMessage} (0 minutes ago)", formattedPost);
        }

        [TestMethod]
        public void FormatPost_returns_correct_grammar_for_post_one_minute_ago()
        {
            string testMessage = "Everything may or may not be awesome";

            PostWithFixedDateTime postOneMinuteAgo = new PostWithFixedDateTime(
                testMessage, 
                DateTime.UtcNow.AddMinutes(-1));

            string formattedPost = new Timeline().FormatPost(postOneMinuteAgo);

            Assert.AreEqual($"{testMessage} (1 minute ago)", formattedPost);
        }

        [TestMethod]
        public void GetTimeline_returns_formatted_collection_of_posts()
        {
            string testMessage  = "Unable to ascertain the awesomeness of everything";
            string testMessage2 = "Inquest finds not all things are awesome";

            Timeline testTimeline = new Timeline();

            testTimeline.AddPost(new Post(testMessage));
            testTimeline.AddPost(
                new PostWithFixedDateTime(testMessage2, DateTime.UtcNow.AddMinutes(-10)));

            IEnumerable<string> formattedPosts = testTimeline.GetTimeline();

            Assert.AreEqual(
                $"{testMessage} (0 minutes ago){Environment.NewLine}" +
                $"{testMessage2} (10 minutes ago)",
                string.Join(Environment.NewLine, formattedPosts));
        }
    }
}
