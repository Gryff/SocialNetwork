using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SocialNetwork.Core
{
    public static class Commands
    {
        public static void PostMessage(string userInput, List<User> users)
        {
            string[] parsedInput = Regex.Split(userInput, @" -> ");

            User user = users.FirstOrDefault(u => u.Name == parsedInput[0]);

            if (user != null)
            {
                user.Timeline.Posts.Add(new Post(parsedInput[1]));
            }
            else
            {
                User newUser = new User(parsedInput[0]);

                users.Add(newUser);

                newUser.Timeline.Posts.Add(new Post(parsedInput[1]));
            }
        }

        public static string GetFormattedTimeline(string userInput, List<User> users)
        {
            User user = users.FirstOrDefault(u => u.Name == userInput);

            if (user == null) return string.Empty;

            IEnumerable<string> userTimeline = user.Timeline.GetTimeline().Reverse();

            return string.Join(Environment.NewLine, userTimeline);
        }


        public static void AddUserToFollowingForUser(string userInput, List<User> users)
        {
            string[] parsedInput = Regex.Split(userInput, " follows ");

            User user = users.FirstOrDefault(u => u.Name == parsedInput[0]);

            User userToFollow = users.FirstOrDefault(u => u.Name == parsedInput[1]);

            if (userToFollow != null)
            {
                user?.Following.Add(userToFollow);
            }

        }

        public static string GetWall(string userInput, List<User> users)
        {
            string[] parsedInput = Regex.Split(userInput, " wall");

            User user = users.FirstOrDefault(u => u.Name == parsedInput[0]);

            if (user == null) return "";

            List<Tuple<string, Post>> wall = new List<Tuple<string, Post>>();

            wall.AddRange(user.Timeline.Posts.Select(
                post => new Tuple<string, Post>(user.Name, post)));

            foreach (User u in user.Following)
            {
                wall.AddRange(
                    u.Timeline.Posts.Select(
                        post => new Tuple<string, Post>(u.Name, post)));
            }

            IEnumerable<string> result = wall
                .OrderByDescending(t => t.Item2.PostedDateTime)
                .Select(p => $"{p.Item1} - {p.Item2.ToOutputFormat()}");

            return string.Join(Environment.NewLine, result);
        }

        public static CommandType DetermineCommandType(string userInput)
        {
            if (userInput.Contains("->"))
                return CommandType.Post;

            if (userInput.Contains("follows"))
                return CommandType.Follow;

            if (userInput.Contains("wall"))
                return CommandType.Wall;

            return CommandType.Timeline;
        }
    }
}
