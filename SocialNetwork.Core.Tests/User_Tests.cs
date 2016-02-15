using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SocialNetwork.Core.Tests
{
    [TestClass]
    public class User_Tests
    {
        [TestMethod]
        public void Instantiated_User_contains_correct_metadata()
        {
            string testName = "Bob";

            User user = new User(testName);

            Assert.AreEqual(testName, user.Name);
        }
    }
}
