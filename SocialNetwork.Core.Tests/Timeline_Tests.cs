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
        public void GetTimeline_returns_formatted_collection_of_posts()
        {
            string testMessage  = "Unable to ascertain the awesomeness of everything";
            string testMessage2 = "Inquest finds not all things are awesome";

            Timeline testTimeline = new Timeline();

            testTimeline.Posts.Add(new Post(testMessage));
            testTimeline.Posts.Add(
                new PostWithFixedDateTime(testMessage2, DateTime.UtcNow.AddMinutes(-10)));

            IEnumerable<string> formattedPosts = testTimeline.GetTimeline();

            Assert.AreEqual(
                $"{testMessage} (0 minutes ago){Environment.NewLine}" +
                $"{testMessage2} (10 minutes ago)",
                string.Join(Environment.NewLine, formattedPosts));
        }
    }
}
