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
    }
}
