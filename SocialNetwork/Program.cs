using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SocialNetwork.Core;

namespace SocialNetwork
{
    class Program
    {
        private static readonly List<User> Users = new List<User>();

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my social network");
            Console.WriteLine("To post a message follow the format {name} -> {message}");

            while (true)
            {
                string userInput = Console.ReadLine();

                if (userInput.Contains("->"))
                    PostMessage(userInput);

                else if (userInput.Contains("follows"))
                    AddUserToFollowingForUser(userInput);

                else if (userInput.Contains("wall"))
                    DisplayWall(userInput);

                else if (!userInput.Contains(" "))
                    DisplayTimeline(userInput);
            }

        }
        
        private static void PostMessage(string userInput)
        {
            string[] parsedInput = Regex.Split(userInput, @" -> ");

            User user = Users.FirstOrDefault(u => u.Name == parsedInput[0]);

            if (user != null)
            {
                user.Timeline.Posts.Add(new Post(parsedInput[1]));
            }
            else
            {
                User newUser = new User(parsedInput[0]);

                Users.Add(newUser);

                newUser.Timeline.Posts.Add(new Post(parsedInput[1]));
            }
        }

        private static void DisplayTimeline(string userInput)
        {
            User user = Users.FirstOrDefault(u => u.Name == userInput);

            if (user == null) return;

            IEnumerable<string> userTimeline = user.Timeline.GetTimeline().Reverse();

            Console.WriteLine(string.Join(Environment.NewLine, userTimeline));
        }


        private static void AddUserToFollowingForUser(string userInput)
        {
            string[] parsedInput = Regex.Split(userInput, " follows ");

            User user = Users.FirstOrDefault(u => u.Name == parsedInput[0]);

            User userToFollow = Users.FirstOrDefault(u => u.Name == parsedInput[1]);

            if (userToFollow != null)
            {
                user?.Following.Add(userToFollow);
            }
                
        }

        private static void DisplayWall(string userInput)
        {
            string[] parsedInput = Regex.Split(userInput, " wall");

            User user = Users.FirstOrDefault(u => u.Name == parsedInput[0]);

            if (user == null) return;

            List<Tuple<string, Post>> wall = new List<Tuple<string, Post>>();

            wall.AddRange(user.Timeline.Posts.Select(
                post => new Tuple<string, Post>(user.Name, post)));

            foreach (User u in user.Following)
            {
                wall.AddRange(
                    u.Timeline.Posts.Select(
                        post => new Tuple<string, Post>(u.Name, post)));
            }
            
            foreach (Tuple<string, Post> p in wall.OrderByDescending(t => t.Item2.PostedDateTime))
            {
                Console.WriteLine($"{p.Item1} - {p.Item2.ToOutputFormat()}");
            }
        }
    }
}
