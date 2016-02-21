using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SocialNetwork.Core.Tests
{
    [TestClass]
    public class Commands_Tests
    {
        [TestMethod]
        public void PostMessage_adds_Post_to_Users_Timeline_when_User_already_exists()
        {
            string testUserInput = "Alice -> hello London";

            List<User> testUsers = new List<User>
            {
                new User("Alice")
            };

            Assert.AreEqual(0, testUsers[0].Timeline.Posts.Count);

            Commands.PostMessage(testUserInput, testUsers);

            Assert.AreEqual(1, testUsers[0].Timeline.Posts.Count);

            Assert.AreEqual("hello London", testUsers[0].Timeline.Posts[0].Content);
        }

        [TestMethod]
        public void PostMessage_creates_User_if_it_doesnt_exist_and_adds_Post()
        {
            string testUserInput = "Alice -> goodnight London";

            List<User> testUsers = new List<User>();

            Assert.AreEqual(0, testUsers.Count);

            Commands.PostMessage(testUserInput, testUsers);

            Assert.AreEqual(1, testUsers.Count);

            Assert.AreEqual(1, testUsers[0].Timeline.Posts.Count);

            Assert.AreEqual("goodnight London", testUsers[0].Timeline.Posts[0].Content);
        }

        [TestMethod]
        public void GetFormattedTimeline_returns_expected_timeline_format()
        {
            List<User> testUsers = new List<User>();

            User bob = new User("Bob");

            bob.Timeline.Posts.AddRange(new []
            {
                new Post
                {
                    Content = "good night last night",
                    PostedDateTime = DateTime.UtcNow.AddMinutes(-10)
                },
                new Post("not feeling so great today"), 
            });

            testUsers.Add(bob);

            string testUserInput = "Bob";

            string result = Commands.GetFormattedTimeline(testUserInput, testUsers);

            Assert.AreEqual(
                $"not feeling so great today (0 seconds ago){Environment.NewLine}" +
                    "good night last night (10 minutes ago)", 
                result);
        }

        [TestMethod]
        public void AddUserToFollowingForUser_adds_User_to_Following_for_user()
        {
            List<User> testUsers = new List<User>
            {
                new User("Bob"),
                new User("Alice")
            };

            string testUserInput = "Bob follows Alice";

            Assert.AreEqual(0, testUsers[0].Following.Count);

            Commands.AddUserToFollowingForUser(testUserInput, testUsers);

            Assert.AreEqual(1, testUsers[0].Following.Count);

            Assert.AreEqual("Alice", testUsers[0].Following[0].Name);
        }

        [TestMethod]
        public async Task GetWall_returns_expected_wall_format()
        {
            List<User> testUsers = new List<User>();

            User bob = new User("Bob");

            bob.Timeline.Posts.Add(new Post
            {
                Content = "how is everyone?",
                PostedDateTime = DateTime.UtcNow.AddMinutes(-10)
            });

            bob.Timeline.Posts.Add(new Post("I'm feeling rough"));

            User alice = new User("Alice");

            alice.Timeline.Posts.Add(new Post
            {
                Content = "up bright and early today",
                PostedDateTime = DateTime.UtcNow.AddMinutes(-5)
            });

            await Task.Delay(50);

            alice.Timeline.Posts.Add(new Post("need coffee"));

            bob.Following.Add(alice);

            testUsers.AddRange(new [] { bob, alice });

            string testUserIput = "Bob wall";

            string wall = Commands.GetWall(testUserIput, testUsers);

            Assert.AreEqual(
                $"Alice - need coffee (0 seconds ago){Environment.NewLine}" +
                $"Bob - I'm feeling rough (0 seconds ago){Environment.NewLine}" +
                $"Alice - up bright and early today (5 minutes ago){Environment.NewLine}" +
                "Bob - how is everyone? (10 minutes ago)",
                wall);
        }

        [TestMethod]
        public void DetermineCommandType_returns_expected_CommandTypes()
        {
            Assert.AreEqual(
                CommandType.Post, 
                Commands.DetermineCommandType("Bob -> need a new book"));

            Assert.AreEqual(
                CommandType.Follow, 
                Commands.DetermineCommandType("Bob follows Rupert"));

            Assert.AreEqual(
                CommandType.Timeline,
                Commands.DetermineCommandType("Rupert"));

            Assert.AreEqual(
                CommandType.Wall,
                Commands.DetermineCommandType("Alice wall"));
        }
    }
}
