﻿using System;
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

            List<dynamic> wallPosts = new List<dynamic>();

            wallPosts.AddRange(user.Timeline.Posts.Select( post =>
                new
                {
                    user.Name,
                    Post = post
                }));

            foreach (User u in user.Following)
            {
                wallPosts.AddRange(u.Timeline.Posts.Select(post =>
                    new
                    {
                        u.Name,
                        Post = post
                    }));
            }

            IEnumerable<string> result = wallPosts
                .OrderByDescending(p => p.Post.PostedDateTime)
                .Select(p => $"{p.Name} - {p.Post.ToOutputFormat()}");

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
