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

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my social network");
            Console.WriteLine("To post a message follow the format {name} -> {message}");

            while (true)
            {
                string userInput = Console.ReadLine();

                string[] parsedInput = Regex.Split(userInput, @" -> ");

                User user = Users.FirstOrDefault(u => u.Name == parsedInput[0]);

                if (user != null)
                {
                    user.Timeline.AddPost(new Post(parsedInput[1]));
                }
                else
                {
                    User newUser = new User(parsedInput[0]);

                    Users.Add(newUser);

                    newUser.Timeline.AddPost(new Post(parsedInput[1]));
                }
            }
        }
    }
}
